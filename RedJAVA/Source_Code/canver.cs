using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canver : MonoBehaviour
{
    public Sprite []animasyonKareleri;
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
        if (zaman>0.1f)
        {
            spriteRenderer.sprite = animasyonKareleri[animasyonSayac++];
            if (animasyonSayac == animasyonKareleri.Length)
            {
                animasyonSayac = animasyonKareleri.Length - 1;
            }
            zaman = 0;
        }
    }
}
