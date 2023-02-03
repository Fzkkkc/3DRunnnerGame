using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text _coinsText;
    private void Start()
    {
        int _coinsCount = PlayerPrefs.GetInt("_coinsCount");
        _coinsText.text = _coinsCount.ToString();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(0);
    }
}
