using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class karakterKontrol : MonoBehaviour
{
    public Sprite[] beklemeAnim;
    public Sprite[] ziplamaAnim;
    public Sprite[] yurumeAnim;

    public Text canText;
    public Text coinText;
    public Image arkaGecis;
    SpriteRenderer spriteRenderer;
    Rigidbody2D fizik;
    GameObject kamera;

    Vector3 vec;
    Vector3 kameraSonPos;
    Vector3 kamerailkPos;

    int beklemeAnimSayac = 0;
    int yurumeAnimSayac = 0;
    int can = 100;
    
    float horizontal = 0;
    float beklemeAnimZaman = 0;
    float yurumeAnimZaman = 0;
    float gecisSayaci = 0;
    float anaMenuyeDonZaman = 0;

    bool birKereZipla = true;
    int altinSayaci = 0;

    void Start()
    {
        Time.timeScale = 1;
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        kamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (SceneManager.GetActiveScene().buildIndex>PlayerPrefs.GetInt("kacincilevel"))
        {
            PlayerPrefs.SetInt("kacincilevel", SceneManager.GetActiveScene().buildIndex);
        }
        PlayerPrefs.SetInt("kacincilevel", SceneManager.GetActiveScene().buildIndex);
        kamerailkPos = kamera.transform.position - transform.position;
        canText.text = "CAN  " + can;
        coinText.text = "30 - " + altinSayaci;


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (birKereZipla)
            {
                fizik.AddForce(new Vector2(0,500));
                birKereZipla = false;
            } 
        }
    }

    void FixedUpdate()
    {
        karakterHareket();
        Animasyon();
        if (can<=0)
        {
            Time.timeScale = 0.4f;
            canText.enabled = false;
            gecisSayaci += 0.03f;
            arkaGecis.color = new Color(0, 0, 0, gecisSayaci);
            anaMenuyeDonZaman += Time.deltaTime;
            if (anaMenuyeDonZaman>1)
            {
                SceneManager.LoadScene("mainMenu");
            }
        }
    }
    void LateUpdate()
    {
        kameraKontrol();
    }
    void karakterHareket()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        //horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        vec = new Vector3(horizontal * 10, fizik.velocity.y, 0);
        fizik.velocity = vec;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        birKereZipla = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="kursun")
        {
            can--;
            canText.text = "CAN  " + can;
        }
        if (collision.gameObject.tag == "dusman")
        {
            can-=10;
            canText.text = "CAN  " + can;
        }
        if (collision.gameObject.tag == "testere")
        {
            can -= 10;
            canText.text = "CAN  " + can;
        }
        if (collision.gameObject.tag == "levelsonu")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (collision.gameObject.tag == "canver")
        {
            can += 10;
            if (can>=100)
            {
                can = 100;
            }
            canText.text = "CAN  " + can;
            collision.GetComponent<BoxCollider2D>().enabled = false;
            collision.GetComponent<canver>().enabled = true;
            Destroy(collision.gameObject,3);
        }
        if (collision.gameObject.tag == "coin")
        {
            altinSayaci++;
            coinText.text = "30 - " + altinSayaci;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "su")
        {
            can = 0;
        }
        if (collision.gameObject.tag == "cainmace")
        {
            can -= 10;
            canText.text = "CAN  " + can;
        }

    }
    void Animasyon()
    {

        if (birKereZipla)
        {
            if (horizontal == 0)
            {
                beklemeAnimZaman += Time.deltaTime;
                if (beklemeAnimZaman > 0.05f)
                {
                    spriteRenderer.sprite = beklemeAnim[beklemeAnimSayac++];
                    if (beklemeAnimSayac == beklemeAnim.Length)
                    {
                        beklemeAnimSayac = 0;
                    }
                    beklemeAnimZaman = 0;
                }

            }
            else if (horizontal > 0)
            {
                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.01f)
                {
                    spriteRenderer.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    yurumeAnimZaman = 0;
                }
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0)
            {
                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.01f)
                {
                    spriteRenderer.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    yurumeAnimZaman = 0;
                }
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            if (fizik.velocity.y>0)
            {
                spriteRenderer.sprite = ziplamaAnim[0];
            }
            else
            {
                spriteRenderer.sprite = ziplamaAnim[1];
            }
            if (horizontal>0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal<0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        
    }

    void kameraKontrol()
    {
        kameraSonPos = kamerailkPos + transform.position;
        kamera.transform.position = Vector3.Lerp(kamera.transform.position, kameraSonPos,0.08f);
    }
}
