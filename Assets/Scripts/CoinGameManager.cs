using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinGameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinCounterText;
    [SerializeField] GameObject endGameWindow;
 
    public int coinCounter;
    public int cardsCounter;

    void Awake()
    {
        coinCounter = 0;
        cardsCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coinCounterText.text = coinCounter.ToString();
        //chestShop.SetActive(coinCounter >= 5);
    }

    public void ShowEndGameWindow()
    {
        endGameWindow.SetActive(true);
        endGameWindow.GetComponent<AudioSource>().Play();
    }

    public void RestartGame()
    {
        endGameWindow.GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
