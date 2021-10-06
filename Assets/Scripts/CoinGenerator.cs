using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{

    [SerializeField] int numRows;
    [SerializeField] int numColumns;
    [SerializeField] Transform firstCoinTransform;
    [SerializeField] Transform lastCoinTransform;
    [SerializeField] GameObject coinTemplate;
    [SerializeField] Transform coinTargetTransform;

    void Awake()
    {
        SpawnCoins();
    }

    void Update()
    {
        
    }

    private void SpawnCoins()
    {
        float xDistance = (lastCoinTransform.position.x - firstCoinTransform.position.x) / (numColumns - 1);
        float yDistance = (lastCoinTransform.position.y - firstCoinTransform.position.y) / (numRows - 1);
        int count = 0;
        for(int c = 0; c < numColumns; c++)
        {
            for(int r = 0; r < numRows; r++)
            {
                ++count;
                var copy = Instantiate(coinTemplate);
                copy.transform.SetParent(transform);
                copy.name = "Coin " + count.ToString();
                Vector3 offset = new Vector3(xDistance * c, yDistance * r, 0);
                copy.transform.position = firstCoinTransform.position + offset;
                copy.transform.localScale = new Vector3(1, 1, 1);
                //Debug.Log(coinTargetTransform.position.x + " ; " + coinTargetTransform.position.y);
                copy.GetComponent<Coin>().targetTransformWhenClicked = coinTargetTransform;
                copy.gameObject.SetActive(true);
            }
        }
    }
}
