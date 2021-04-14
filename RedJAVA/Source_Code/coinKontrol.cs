using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinKontrol : MonoBehaviour
{
    public Sprite[] coins;
    SpriteRenderer spriteRenderer;
    float zaman = 0;
    int animasyonSayac = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        zaman += Time.deltaTime;
        if (zaman > 0.03f)
        {
            spriteRenderer.sprite = coins[animasyonSayac++];
            if (animasyonSayac == coins.Length)
            {
                animasyonSayac = 0;
            }
            zaman = 0;
        }
    }
}
