using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    private CapsuleCollider _capsuleCollider;
    private Vector3 _direction;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity;
    [SerializeField] private int _coinsCount;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private Text _coinsText;
    [SerializeField] private Score _scoreScript;
    private int _lineToMove = 1;
    public float LineDistance = 4;
    private const float _maxSpeed = 110;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        Time.timeScale = 1;
        _coinsCount = PlayerPrefs.GetInt("_coinsCount");
        _coinsText.text = _coinsCount.ToString();
        StartCoroutine(SpeedIncrease());
    }

    private void Update()
    {
        if (SwipeController.SwipeRight)
        {
            if (_lineToMove < 2)
                _lineToMove++;
        }

        if (SwipeController.SwipeLeft)
        {
            if (_lineToMove > 0)
                _lineToMove--;
        }

        if (SwipeController.SwipeUp)
        {
            if(_controller.isGrounded)
                Jump();
        }

        if (SwipeController.SwipeDown)
        {
            StartCoroutine(Slide());
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (_lineToMove == 0)
            targetPosition += Vector3.left * LineDistance;
        else if (_lineToMove == 2)
            targetPosition += Vector3.right * LineDistance;

        if (transform.position == targetPosition)
            return;

        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            _controller.Move(moveDir);
        else
            _controller.Move(diff);
    }

    private void Jump()
    {
        _direction.y = _jumpForce;
    }

    void FixedUpdate()
    {
        _direction.z = _speed;
        _direction.y += _gravity * Time.fixedDeltaTime;
        _controller.Move(_direction * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "obstacle")
        {
            _losePanel.SetActive(true);
            int lastRunScore = int.Parse(_scoreScript.ScoreText.text.ToString());
            PlayerPrefs.SetInt("lastRunScore", lastRunScore);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            _coinsCount++;
            PlayerPrefs.SetInt("_coinsCount", _coinsCount);
            _coinsText.text = _coinsCount.ToString();
            Destroy(other.gameObject);
        }
    }

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(1);
        if(_speed < _maxSpeed)
        {
            _speed += 1;
            StartCoroutine(SpeedIncrease());
        }
    }

    private IEnumerator Slide()
    {
        _capsuleCollider.center = new Vector3(0, -0.8f, 0);
        _capsuleCollider.height = 1;

        yield return new WaitForSeconds(1);

        _capsuleCollider.center = new Vector3(0, 0, 0);
        _capsuleCollider.height = 2;
    }
}
