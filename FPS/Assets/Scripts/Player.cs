using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator playerAnim;
    public float speed = 1;
    public float speedF = 0.02f;
    public float speedJ = 0.4f;

    public AudioClip[] audioFiles;

    private bool inAir = false;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float RealSpeed = speed * speedF;
        float RealJump = speed * speedJ;
        Vector3 newTransform = Vector3.zero;
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
        {
            playerAnim.SetBool("isRunning", true);
            GetComponent<SpriteRenderer>().flipX = true;
            newTransform += new Vector3(-RealSpeed, 0, 0);
        }

        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            playerAnim.SetBool("isRunning", false);
            GetComponent<SpriteRenderer>().flipX = false;
            
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            playerAnim.SetBool("isRunning", true);
            newTransform += new Vector3(RealSpeed, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            transform.Translate( RealSpeed, 0, 0);
            playerAnim.SetBool("isRunning", false);
        }
        if((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))){
            playerAnim.SetBool("isRunning", false);
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if((Input.GetKeyDown(KeyCode.Space)) && !inAir)
        {
            playerAnim.SetBool("isJumping", true);
            GetComponent<Rigidbody2D>().velocity += RealJump * Vector2.up;
            inAir = true;
           
        }

        if (Input.GetMouseButtonDown(0))
        {
            Bullet();
        }

        transform.position += newTransform;
        
    }
    public void Bullet()
    {
        GetComponent<AudioSource>().Play();
        GameObject Bullet = Instantiate(GM.instance.bullet[0]);
        Bullet.transform.position = transform.position;
        Bullet.transform.rotation = transform.rotation;

        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        Bullet.GetComponent<bullet>().Direction = p - transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            inAir = false;
            playerAnim.SetBool("isJumping", false);
        }
      /*  if (collision.gameObject.tag == "bottom_left_barrier")
        {
            if (collision.gameObject.name == "01")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 1, 0));
                collision.transform.parent.Find("02").gameObject.SetActive(true);
            }
            if (collision.gameObject.name == "02")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 0, 0));
                collision.transform.parent.Find("01").gameObject.SetActive(true);
            }
            inAir = false;
            playerAnim.SetBool("isJumping", false);
        }
        if (collision.gameObject.tag == "bottom_right_barrier") { 
                if (collision.gameObject.name == "03")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 2, 0));
                collision.transform.parent.Find("04").gameObject.SetActive(true);
            }
            if (collision.gameObject.name == "04")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 1, 0));
                collision.transform.parent.Find("03").gameObject.SetActive(true);
            }
            inAir = false;
            playerAnim.SetBool("isJumping", false);
        }
        if (collision.gameObject.tag == "mid_left_barrier")
        {
            if (collision.gameObject.name == "01")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 0, 1));
                collision.transform.parent.Find("10").gameObject.SetActive(true);
            }
            if (collision.gameObject.name == "10")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 0, 0));
                collision.transform.parent.Find("01").gameObject.SetActive(true);
            }
            inAir = false;
            playerAnim.SetBool("isJumping", false);
        }
        if (collision.gameObject.tag == "mid_top")
        {
            if (collision.gameObject.name == "10")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 0, 2));
                collision.transform.parent.Find("20").gameObject.SetActive(true);
            }
            if (collision.gameObject.name == "20")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 0, 1));
                collision.transform.parent.Find("10").gameObject.SetActive(true);
            }
            inAir = false;
            playerAnim.SetBool("isJumping", false);
        }
        if (collision.gameObject.tag == "mid_mid")
        {
            if (collision.gameObject.name == "02")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 1, 1));
                collision.transform.parent.Find("12").gameObject.SetActive(true);
            }
            if (collision.gameObject.name == "12")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 1, 0));
                collision.transform.parent.Find("02").gameObject.SetActive(true);
            }
            inAir = false;
            playerAnim.SetBool("isJumping", false);
        }
        if (collision.gameObject.tag == "mid_mid_mid")
        {
            if (collision.gameObject.name == "12")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 2, 1));
                collision.transform.parent.Find("13").gameObject.SetActive(true);
            }
            if (collision.gameObject.name == "13")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 1, 1));
                collision.transform.parent.Find("12").gameObject.SetActive(true);
            }
            inAir = false;
            playerAnim.SetBool("isJumping", false);
        }
        if (collision.gameObject.tag == "mid_mid_top")
        {
            if (collision.gameObject.name == "12")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 1, 2));
                collision.transform.parent.Find("22").gameObject.SetActive(true);
            }
            if (collision.gameObject.name == "22")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 1, 1));
                collision.transform.parent.Find("12").gameObject.SetActive(true);
            }
            inAir = false;
            playerAnim.SetBool("isJumping", false);
        }
        if (collision.gameObject.tag == "mid_right")
        {
            if (collision.gameObject.name == "10")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 1, 1));
                collision.transform.parent.Find("11").gameObject.SetActive(true);
            }
            if (collision.gameObject.name == "11")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 0, 1));
                collision.transform.parent.Find("10").gameObject.SetActive(true);
            }
            inAir = false;
            playerAnim.SetBool("isJumping", false);
        }
        if (collision.gameObject.tag == "right_mid")
        {
            if (collision.gameObject.name == "03")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 2, 1));
                collision.transform.parent.Find("13").gameObject.SetActive(true);
            }
            if (collision.gameObject.name == "13")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 2, 0));
                collision.transform.parent.Find("03").gameObject.SetActive(true);
            }
            inAir = false;
            playerAnim.SetBool("isJumping", false);
        }
        if (collision.gameObject.tag == "right_top")
        {
            if (collision.gameObject.name == "13")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 2, 2));
                collision.transform.parent.Find("23").gameObject.SetActive(true);
            }
            if (collision.gameObject.name == "23")
            {
                collision.gameObject.SetActive(false);
                GM.instance.StartCoroutine(GM.instance.moveBottomLeft(Camera.main.transform.position, 2, 1));
                collision.transform.parent.Find("13").gameObject.SetActive(true);
            }
            inAir = false;
            playerAnim.SetBool("isJumping", false);
        }*/
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            GM.instance.LoseLife(this.gameObject);
        }
    }
}
