using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Text _scoreText;
  
    void Start()
    {
        
    }

    void Update()
    {
        _scoreText.text = ((int)(_player.position.z / 2)).ToString();
    }
}
