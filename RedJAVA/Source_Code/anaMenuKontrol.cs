using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class anaMenuKontrol : MonoBehaviour
{
    GameObject level1, level2, level3;
    GameObject leveller;
    void Start()
    {
        level1 = GameObject.Find("level 1");
        level2 = GameObject.Find("level 2");
        level3 = GameObject.Find("level 3");

        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);

        leveller = GameObject.Find("leveller");
        

        for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)
        {
            leveller.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }
    public void butonSec(int gelenButon)
    {
        if (gelenButon == 1)
        {
            SceneManager.LoadScene(1);
        }
        else if (gelenButon == 2)
        {
            for (int i = 0; i < leveller.transform.childCount; i++)
            {
                leveller.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else if (gelenButon == 3)
        {
            Application.Quit();
        }
    }
    
  public void levellerButon(int gelenLevel)
    {
        SceneManager.LoadScene(gelenLevel);
    }
}
