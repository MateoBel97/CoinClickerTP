using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestShopWindow : MonoBehaviour
{
    [SerializeField] GameObject chest;
    [SerializeField] Sprite closedChestSprite;
    [SerializeField] Sprite openedChestSprite;

    [SerializeField] GameObject cardTemplate;

    private CoinGameManager coinGameManager;
    // Start is called before the first frame update
    void Awake()
    {
        coinGameManager = GameObject.Find("Game").GetComponent<CoinGameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenWindow()
    {
        //chest.GetComponent<Image>().sprite = closedChestSprite;
        //chest.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        chest.GetComponent<Animator>().SetTrigger("Closed");
        GameObject.Find("BuyButton").GetComponent<Button>().enabled = true;
        
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void BuyChest()
    {
        if(coinGameManager.coinCounter >= 5)
        {
            GameObject.Find("BuyButton").GetComponent<Button>().enabled = false;
            chest.GetComponent<Image>().sprite = openedChestSprite;
            coinGameManager.coinCounter -= 5;
            coinGameManager.cardsCounter++;
            chest.GetComponent<AudioSource>().Play();
            StartCoroutine(OpenChest());

            
            //transform.gameObject.SetActive(coinGameManager.coinCounter >= 5);
            
        }
        
    }
    public void CloseWindow()
    {
        transform.gameObject.SetActive(false);
    }

    private IEnumerator OpenChest()
    {
        chest.GetComponent<Animator>().SetTrigger("Bought");
        chest.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1.5f);
        chest.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        cardTemplate.GetComponent<Card>().StartCardAnimation(coinGameManager.cardsCounter, chest.transform);
        CloseWindow();
    }

}
