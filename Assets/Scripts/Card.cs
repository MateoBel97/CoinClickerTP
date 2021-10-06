using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] GameObject[] cardSlots;
    [SerializeField] Sprite[] cardSprites;
    [SerializeField] float speed;

    private Transform targetTransform;
    private Vector3 movement;
    private CoinGameManager coinGameManager;

    void Awake()
    {
        coinGameManager = GameObject.Find("Game").GetComponent<CoinGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSprite(int cardNumber)
    {
        GetComponent<Image>().sprite = cardSprites[cardNumber - 1];
    }
    public void StartCardAnimation(int cardNumber, Transform newTransform)
    {
        targetTransform = cardSlots[cardNumber - 1].transform;
        movement = new Vector3(targetTransform.position.x - transform.position.x,
                                targetTransform.position.y - transform.position.y,
                                0).normalized;
        StartCoroutine(SpawnCard(newTransform));
        
    }

    

    public IEnumerator TranslateCard(GameObject cardCopy)
    {
        while(cardCopy.transform.position.x < targetTransform.position.x)
        {
            cardCopy.transform.Translate(movement * speed * Time.deltaTime);
            yield return null;
        }
        cardCopy.transform.position = targetTransform.position;
        if(coinGameManager.cardsCounter == 3)
        {
            coinGameManager.ShowEndGameWindow();
        }
    }

    private IEnumerator SpawnCard(Transform newTransform)
    {
        var cardCopy = Instantiate(transform.gameObject);
        //var cardCopy = cardTemplate;
        cardCopy.transform.localScale = new Vector3(0, 0, 0);
        cardCopy.transform.SetParent(transform.parent);
        cardCopy.transform.position = newTransform.position;
        cardCopy.GetComponent<Card>().SetSprite(coinGameManager.cardsCounter);
        AudioSource cardAudioSource = cardCopy.GetComponent<AudioSource>();
        cardAudioSource.Play();
        Animator cardAnimator = cardCopy.GetComponent<Animator>();
        cardAnimator.SetInteger("CardsFound", coinGameManager.cardsCounter);
        cardAnimator.SetTrigger("Spawn");
        yield return new WaitForSeconds(2f);
        StartCoroutine(TranslateCard(cardCopy));
    }
}
