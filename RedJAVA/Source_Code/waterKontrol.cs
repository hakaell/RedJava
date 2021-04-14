using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterKontrol : MonoBehaviour
{
    public Sprite[] waters;
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
        if (zaman > 0.05f)
        {
            spriteRenderer.sprite = waters[animasyonSayac++];
            if (animasyonSayac == waters.Length)
            {
                animasyonSayac = 0;
            }
            zaman = 0;
        }
    }
}
