using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public Transform targetTransformWhenClicked;
    
    [SerializeField] float speedToReachTarget;
    [SerializeField] AudioClip coinClickedAudioClip;

    private Vector2 movement;
    private AudioSource audioSource;
    private CoinGameManager coinGameManager;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        coinGameManager = GameObject.Find("Game").GetComponent<CoinGameManager>();
    }

    
    void Update()
    {
        
    }

    public void CoinClicked()
    {
        audioSource.clip = coinClickedAudioClip;
        audioSource.Play();
        var particles = transform.GetChild(0);
        particles.transform.SetParent(transform.parent);
        StartCoroutine(PlayParticles(particles));
        movement = new Vector2(targetTransformWhenClicked.position.x - transform.position.x,
            targetTransformWhenClicked.position.y - transform.position.y).normalized;
        StartCoroutine(MoveToTargerAnimation());
    }

    private IEnumerator PlayParticles(Transform particles)
    {
        particles.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.5f);
        particles.GetComponent<ParticleSystem>().Stop();
    }

    private IEnumerator MoveToTargerAnimation()
    { 
        coinGameManager.coinCounter++;
        while (transform.position.x < targetTransformWhenClicked.position.x)
        {
            transform.Translate(movement * speedToReachTarget * Time.deltaTime);
            yield return null;
        }
        GameObject.Destroy(transform.GetComponent<Image>());
        yield return new WaitForSeconds(1f);
        GameObject.Destroy(transform.gameObject);

    }
}
