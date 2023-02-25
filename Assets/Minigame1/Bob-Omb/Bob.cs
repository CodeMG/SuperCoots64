using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    public enum State { Idle, GrabbedSmall, Exploding, GrabbedBig,Dead};

    public static int explosionCount = 0;

    private Rigidbody2D body;
    public State currentState;
    float timeCounter = 0;
    float explodeCooldown = 1.0f;

    float idletimeCounter = 0;
    float idleExplodeCounter = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        currentState = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused)
        {
            return;
        }
        Debug.DrawRay(transform.position - (Vector3.up), transform.TransformDirection(Vector2.down*0.1f), Color.red,0.1f);
        if (currentState == State.Idle)
        {
            body.velocity = new Vector2(2,0);
            idletimeCounter += Time.deltaTime;
            if (idletimeCounter > idleExplodeCounter)
            {
                currentState = State.Exploding;
                idletimeCounter = 0;

            }
        }
        else if (currentState == State.GrabbedSmall)
        {
            body.simulated = false;
        }
        else if (currentState == State.Exploding)
        {
            
            body.simulated = true;
            if (grounded())
            {
                transform.localScale = new Vector3(transform.localScale.x + 1.0f * Time.deltaTime, transform.localScale.y + 1.0f * Time.deltaTime, transform.localScale.z);
                timeCounter += Time.deltaTime;
                if (timeCounter > explodeCooldown)
                {
                    currentState = State.Dead;
                    timeCounter = 0;
                    idletimeCounter = 0;
                    
                }
            }
        }
        else if (currentState == State.GrabbedBig)
        {
            body.simulated = false;
        }
        else if (currentState == State.Dead)
        {
            //Destroy(this.gameObject);
            transform.position = new Vector3(-14,-4,0);
            GetComponent<AudioSource>().Play();
            currentState = State.Idle;
            transform.localScale = new Vector3(1,1,1);
            explosionCount++;
        }
        Debug.Log(currentState);
    }
    public bool grounded()
    {
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position - (Vector3.up), transform.TransformDirection(Vector2.down), 0.1f);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.transform.gameObject != gameObject && hit.transform.tag != "Player")
            {
                Debug.Log("Grounded");
                return true;
            }
        }
        return false;
    }
    public void thrown(Vector3 dir)
    {
        currentState = State.Exploding;
        body.simulated = true;
        body.velocity += new Vector2(dir.x * 5.0f, dir.y * 5.0f);
    }

    public void grabbed()
    {
        if (currentState == State.Exploding)
        {
            currentState = State.GrabbedBig;
            transform.localScale = new Vector3(2,2,1);
            timeCounter = explodeCooldown+1;
        }
        else
        {
            currentState = State.GrabbedSmall;
        }
    }

}
