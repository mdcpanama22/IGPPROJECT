﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Vector3 Direction;
    public float speed = 1.0f;
    private Rigidbody2D rb2;
    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();

        if (transform.tag == "bullet")
            Destroy(gameObject, 3);
        else if (transform.tag == "ebullet")
            Destroy(gameObject, 8);
    }

    // Update is called once per frame
    void Update()
    {

        rb2.MovePosition(transform.position + Direction * Time.deltaTime * speed);
        rb2.MoveRotation(Vector3.Angle(transform.position, Direction));


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.tag == "bullet")
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies") || collision.transform.tag == "Aster")
            {
               // GetComponent<AudioSource>().Play();
              //  GM.instance.DestroyEnemy(collision.gameObject);
                
            }
        }
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.tag == "bullet")
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies") || collision.gameObject.layer == LayerMask.NameToLayer("floor"))
            {
                // GetComponent<AudioSource>().Play();
                if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
                {
                    if(collision.tag == "Crab")
                    {
                        GM.instance.ChangeScore(200);
                    }else if(collision.tag == "Heads")
                    {
                        GM.instance.ChangeScore(100);
                    }
                    Destroy(collision.gameObject);
                }
                Destroy(this.gameObject);
            }
        }
        
    }
}