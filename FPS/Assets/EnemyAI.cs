using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 1;
    public float speedF = 0.02f;
    private float RealSpeed;
    private bool change = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RealSpeed = speed * speedF;
        if (change)
        {
            transform.position += new Vector3(RealSpeed, 0, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            transform.position -= new Vector3(RealSpeed, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemeyBarrier")
        {
            change = !change;
            Debug.Log("ENEMY BARRIER");
        }
    }
}
