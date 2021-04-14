using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
# endif

public class mace : MonoBehaviour
{
    Vector3 aradakiMesafe;
    GameObject[] gidilecekNoktolar;

    bool aradakiMesafeyiBirKereAl = true;
    bool ileriMiGeriMi = true;

    int aradakiMesafeSayaci = 0;
    void Start()
    {
        gidilecekNoktolar = new GameObject[transform.childCount];
        for (int i = 0; i < gidilecekNoktolar.Length; i++)
        {
            gidilecekNoktolar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktolar[i].transform.SetParent(transform.parent);
        }
    }


    void FixedUpdate()
    {
        noktalaraGit();
    }
    void noktalaraGit()
    {
        if (aradakiMesafeyiBirKereAl)
        {
            aradakiMesafe = (gidilecekNoktolar[aradakiMesafeSayaci].transform.position - transform.position).normalized;
            aradakiMesafeyiBirKereAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktolar[aradakiMesafeSayaci].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * 10;
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
[CustomEditor(typeof(mace))]
[System.Serializable]

class maceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        mace script = (mace)target;
        if (GUILayout.Button("ÜRET", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject yeniObjem = new GameObject();
            yeniObjem.transform.parent = script.transform;
            yeniObjem.transform.position = script.transform.position;
            yeniObjem.name = script.transform.childCount.ToString();
        }
    }
}
# endif
