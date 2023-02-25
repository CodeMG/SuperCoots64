using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public static int hitCounter = 0;
    public GameObject star;
    // Start is called before the first frame update
    void Start()
    {
        hitCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitCounter >= 3 && star.GetComponent<SpriteRenderer>().enabled)
        {
            star.transform.Translate(new Vector3(0,-15/(3*2) * Time.deltaTime,0)) ;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Bowser>().explode();
        hitCounter++;
        transform.parent.GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);
        
    }
}
