using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject chestShopWindow;

    private CoinGameManager coinGameManager;
    private Animator animator;
    private bool isAvailable;
    // Start is called before the first frame update
    void Awake()
    {
        isAvailable = false;
        chestShopWindow.SetActive(false);
        coinGameManager = GameObject.Find("Game").GetComponent<CoinGameManager>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if((coinGameManager.coinCounter >= 5) && (!isAvailable))
        {
            isAvailable = true;
            animator.SetTrigger("Available");
            
        }
        else if((coinGameManager.coinCounter < 5)&&(isAvailable))
        {
            isAvailable = false;
            animator.SetTrigger("Unavailable");
        }
        GetComponent<Button>().enabled = isAvailable;
    }

    public void ShowChestShopWindow()
    {
        GetComponent<AudioSource>().Play();
        chestShopWindow.SetActive(true);
        chestShopWindow.GetComponent<ChestShopWindow>().OpenWindow();
    }
}
