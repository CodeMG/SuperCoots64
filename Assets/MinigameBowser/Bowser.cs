using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowser : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject targetPos;
    public bool grabbed = true;
    float speed = 1;
    public int dir = 1;
    public int explodeCounter = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = (player.transform.position - transform.position).magnitude;
        float scale = (speed/300)*Mathf.Sin(dist * Mathf.PI / 15) + 1;
        transform.GetChild(0).localScale = new Vector3(scale,scale*dir,1);
        if (grabbed)
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }
        if (dist > 15.0f)
        {
            Vector3 target = targetPos.transform.position - transform.position;
            target.Normalize();
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(target.x, target.y) * 10f;
            dir = -1;
        }
    }

    public void explode()
    {
        transform.GetComponent<Rigidbody2D>().velocity -= 2 * new Vector2(-transform.up.x, -transform.up.y) * 10f;
        explodeCounter++;
        if (explodeCounter >= 3)
        {
            transform.gameObject.SetActive(false);
        }
    }

    public void thrown(float sp)
    {
        if (!grabbed)
        {
            return;
        }
        transform.SetParent(null);
        transform.GetComponent<Rigidbody2D>().velocity += new Vector2(-transform.up.x, -transform.up.y) *10f;
        speed = sp;
        grabbed = false;
    }
}
