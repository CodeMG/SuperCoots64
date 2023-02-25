using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyPenguin : MonoBehaviour
{
    public enum State { Idle, GrabbedSmall};

    private Rigidbody2D body;
    public State currentState;
    float timeCounter = 0;
    float explodeCooldown = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        currentState = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position - (Vector3.up), transform.TransformDirection(Vector2.down * 0.1f), Color.red, 0.1f);
        if (currentState == State.Idle)
        {
            body.simulated = true;
        }
        else if (currentState == State.GrabbedSmall)
        {
            body.simulated = false;
        }
    }

    public void thrown(Vector3 dir)
    {
        currentState = State.Idle;
        body.simulated = true;
        body.velocity += new Vector2(dir.x * 5.0f, dir.y * 5.0f);
    }

    public void grabbed()
    {

        currentState = State.GrabbedSmall;
        
    }

}
