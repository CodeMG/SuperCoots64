using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PB3 : MonoBehaviour
{

    private Rigidbody2D body;
    private float speed = 50;
    private Vector2 inputAcc;
    private int dir = 1;

    private bool isGrabbing;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        inputAcc = new Vector2(0, 0);
        isGrabbing = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position + (Vector3.right * 0.6f * dir), transform.TransformDirection(Vector2.right * dir * 2), Color.red, 0.1f);
        //body.velocity = new Vector2(inputVelocity.x * speed, body.velocity.y);
        body.velocity += inputAcc * Time.deltaTime;
        if (body.velocity.x > 10)
        {
            body.velocity = new Vector2(10, body.velocity.y);
        }
        if (body.velocity.x < -10)
        {
            body.velocity = new Vector2(-10, body.velocity.y);
        }
        //Damp
        //body.velocity = new Vector2(body.velocity.x *0.99f,body.velocity.y);
        body.velocity = new Vector2(body.velocity.x - (body.velocity.x * 2 * Time.deltaTime), body.velocity.y);
    }

    public bool grounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position - (Vector3.up * 1.1f), transform.TransformDirection(Vector2.down), 0.05f);

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    public void moveHorizontal(InputAction.CallbackContext context)
    {

        Debug.Log("Move Detected");
        Vector2 inputVec = context.ReadValue<Vector2>();
        if (inputVec.x < 0.2 && inputVec.x > -0.2)
        {
            inputVec.x = 0;
        }
        if (inputVec.y < 0.2 && inputVec.y > -0.2)
        {
            inputVec.y = 0;
        }
        inputAcc = new Vector2(inputVec.x * speed, 0);
        if (inputVec.x > 0)
        {
            dir = 1;
        }
        else if (inputVec.x < 0)
        {
            dir = -1;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (grounded())
        {
            Debug.Log("Jump");
            body.velocity = new Vector2(body.velocity.x, 15);
        }
    }
}
