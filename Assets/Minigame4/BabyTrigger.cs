using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject speechBubble;
    public bool realParent;
    public GameObject star;
    void Start()
    {
        speechBubble.SetActive(false);
        star.GetComponent<SpriteRenderer>().enabled = false;
        star.GetComponent<BoxCollider2D>().enabled = false;
        star.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "BabyPenguin")
        {
            speechBubble.SetActive(true);
            if (realParent)
            {
                star.GetComponent<SpriteRenderer>().enabled = true;
                star.GetComponent<BoxCollider2D>().enabled = true;
                star.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        
    }
}
