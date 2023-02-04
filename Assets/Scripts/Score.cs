using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] public Text ScoreText;
    private int _totalScore;
    public int ScoreMultiplier;

    void Update()
    {
        _totalScore += ScoreMultiplier;
        ScoreText.text = _totalScore.ToString();
    }
}
