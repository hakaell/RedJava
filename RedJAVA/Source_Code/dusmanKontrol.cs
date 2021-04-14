using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
# endif

public class dusmanKontrol : MonoBehaviour
{
    Vector3 aradakiMesafe;
    GameObject[] gidilecekNoktolar;
    GameObject karakter;
    RaycastHit2D ray;

    bool aradakiMesafeyiBirKereAl = true;
    bool ileriMiGeriMi = true;

    int aradakiMesafeSayaci = 0;
    int hiz= 5;
    float atesZamani = 0;

    public Sprite onTaraf;
    public Sprite arkaTaraf;
    public GameObject kursun;
    public LayerMask layermask;

    SpriteRenderer spriteRenderer;
    void Start()
    {
        gidilecekNoktolar = new GameObject[transform.childCount];
        karakter = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < gidilecekNoktolar.Length; i++)
        {
            gidilecekNoktolar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktolar[i].transform.SetParent(transform.parent);
        }
    }
    void atesEt()
    {
        atesZamani += Time.deltaTime;
        if (atesZamani>Random.Range(0.5f,1))
        {
            Instantiate(kursun, transform.position, Quaternion.identity);
            atesZamani = 0;
        }
    }

    void FixedUpdate()
    {
        beniGordumu();
        if (ray.collider.tag=="Player")
        {
            hiz = 8;
            spriteRenderer.sprite = onTaraf;
            atesEt();
        }
        else
        {
            hiz = 4;
            spriteRenderer.sprite = arkaTaraf;
        }



        noktalaraGit();
    }
    void beniGordumu()
    {
        Vector3 rayYonum = karakter.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayYonum, 100,layermask);
        Debug.DrawLine(transform.position, ray.point, Color.magenta);
    }

    public Vector2 getYon()
    {
        return (karakter.transform.position - transform.position).normalized;
    }

    void noktalaraGit()
    {
        if (aradakiMesafeyiBirKereAl)
        {
            aradakiMesafe = (gidilecekNoktolar[aradakiMesafeSayaci].transform.position - transform.position).normalized;
            aradakiMesafeyiBirKereAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktolar[aradakiMesafeSayaci].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * hiz;
        if (mesafe < 0.5f)
        {
            aradakiMesafeyiBirKereAl = true;
            if (aradakiMesafeSayaci == gidilecekNoktolar.Length - 1)
            {
                ileriMiGeriMi = false;
            }
            else if (aradakiMesafeSayaci == 0)
            {
                ileriMiGeriMi = true;
            }
            if (ileriMiGeriMi)
            {
                aradakiMesafeSayaci++;
            }
            else
            {
                aradakiMesafeSayaci--;
            }
        }
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }

    }
# endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(dusmanKontrol))]
[System.Serializable]

class dusmanKontrolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        dusmanKontrol script = (dusmanKontrol)target;
        if (GUILayout.Button("ÜRET", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject yeniObjem = new GameObject();
            yeniObjem.transform.parent = script.transform;
            yeniObjem.transform.position = script.transform.position;
            yeniObjem.name = script.transform.childCount.ToString();
        }
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layermask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("arkaTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("kursun"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}
# endif
