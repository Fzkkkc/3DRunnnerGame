using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] public Text ScoreText;

    void Update()
    {
        ScoreText.text = ((int)(_player.position.z / 2)).ToString();
    }
}
