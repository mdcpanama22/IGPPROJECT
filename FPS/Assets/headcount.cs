using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headcount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Base")
        {
            GM.instance.ChangeScore((int)(transform.localScale.x * 100));
            collision.GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
        }
    }
}
