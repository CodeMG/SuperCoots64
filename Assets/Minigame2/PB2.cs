using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PB2 : MonoBehaviour
{
    private Rigidbody2D body;
    float speed = 50;
    int dir = 1;
    Vector2 inputAcc;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity += inputAcc * Time.deltaTime;
        if (body.velocity.x > 10)
        {
            body.velocity = new Vector2(10, body.velocity.y);
        }
        if (body.velocity.x < -10)
        {
            body.velocity = new Vector2(-10, body.velocity.y);
        }
        body.velocity = new Vector2(body.velocity.x - (body.velocity.x * 2 * Time.deltaTime), body.velocity.y);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            //Burn butt
            Debug.Log("Burn baby burn");
            body.velocity = new Vector2(4,20);
        }
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
}
