using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PB1 : MonoBehaviour
{

    private Rigidbody2D body;
    private float speed = 50;
    private Vector2 inputAcc;
    private int dir = 1;

    public bool isGrabbing;
    public GameObject grabbed;
    private Animator animator;
    private string currentState;
    private float xScale;
    public bool inputEnabled = true;
    private bool crouching;

    private float meowCooldown = 0.3f;
    private float meowCounter = 0.0f;

    public GameObject pauseMenu;
    public AudioClip jump;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        inputAcc = new Vector2(0,0);
        isGrabbing = false;
        animator = GetComponent<Animator>();
        xScale = transform.localScale.x;
        inputEnabled = true;
        crouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused) {
            return;
        }
        meowCounter += Time.deltaTime;
        if (!crouching)
        {
            transform.localScale = new Vector3(xScale * dir * -1, transform.localScale.y, transform.localScale.z);
        }
        
        if (inputAcc.magnitude > 0.1 && !isGrabbing && !crouching)
        {
            changeAnimationState("Base Layer.Walking");
        }
        else if (inputAcc.magnitude <= 0.1 && !isGrabbing && currentState != "Base Layer.GotStar" && !crouching)
        {
            changeAnimationState("Base Layer.Idle");
        }
        else if (inputAcc.magnitude <= 0.1 && isGrabbing  && !crouching)
        {
            changeAnimationState("Base Layer.GrabIdle");
        }
        else if (crouching)
        {
            changeAnimationState("Base Layer.Crouching");
        }
        Debug.DrawRay(transform.position + (Vector3.right * 0.6f * dir), transform.TransformDirection(Vector2.right*dir*2), Color.red, 0.1f);
        //body.velocity = new Vector2(inputVelocity.x * speed, body.velocity.y);
        body.velocity += inputAcc * Time.deltaTime;
        if (isGrabbing && grabbed.GetComponent<Bob>() != null && grabbed.GetComponent<Bob>().currentState == Bob.State.GrabbedBig)
        {
            body.velocity = new Vector2(-5 * dir, body.velocity.y);
        }
        if (isGrabbing && (grabbed.GetComponent<Bob>() != null || grabbed.GetComponent<BabyPenguin>() != null))
        {
            grabbed.transform.position = new Vector3(transform.position.x + dir * 0.7f, transform.position.y + 1f, transform.position.z);
        }
        //Damp
        //body.velocity = new Vector2(body.velocity.x *0.99f,body.velocity.y);
        if (crouching)
        {
            body.velocity = new Vector2(body.velocity.x - (body.velocity.x * Time.deltaTime), body.velocity.y - (body.velocity.y * Time.deltaTime));
        }
        else {
            body.velocity = new Vector2(body.velocity.x - (body.velocity.x * 2 * Time.deltaTime), body.velocity.y);
        }
    }

    public void gotStar()
    {
        changeAnimationState("Base Layer.GotStar");
        inputEnabled = false;
        inputAcc = new Vector2(0,0);
    }

    public void changeAnimationState(string newState)
    {
        if (currentState == newState)
        {
            return;
        }
        animator.Play(newState);
        currentState = newState;
    }
    public bool grounded()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position - (Vector3.up * 1.1f), transform.TransformDirection(Vector2.down),1f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && !hit.collider.isTrigger&&hit.transform.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public bool groundedBLJ(out Collider2D col)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position - (Vector3.up * 1.1f), transform.TransformDirection(Vector2.down), 1f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.transform.gameObject != gameObject && hit.transform.tag == "BLJ")
            {
                col = hit.collider;
                return true;
            }
        }
        col = null;
        return false;
    }

    public void grab(InputAction.CallbackContext context)
    {
        if (!inputEnabled || PauseMenu.isPaused)
        {
            return;
        }
        if (!context.performed)
        {
            return;
        }
        if (isGrabbing)
        {
            
            grabbed.transform.SetParent(null);
            isGrabbing = false;
            if (grabbed.GetComponent<Bob>() != null && grabbed.GetComponent<Bob>().currentState == Bob.State.GrabbedBig)
            {
                transform.position = transform.position + new Vector3(-2,0,0)*dir;
                grabbed.GetComponent<Bob>().thrown(new Vector3(dir, 1, 0).normalized);
                grabbed = null;
            }
            else if (grabbed.GetComponent<Bob>() != null && grabbed.GetComponent<Bob>().currentState == Bob.State.GrabbedSmall)
            {
                grabbed.GetComponent<Bob>().thrown(new Vector3(dir, 1, 0).normalized);
                grabbed = null;
            }
            else if (grabbed.GetComponent<BabyPenguin>() != null)
            {
                grabbed.GetComponent<BabyPenguin>().thrown(new Vector3(dir, 1, 0).normalized);
                grabbed = null;
            }
            return;
        }
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + (Vector3.right * 0.6f*dir) + Vector3.down*0.5f, transform.TransformDirection(Vector2.right*dir), 2f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.transform.gameObject != gameObject)
            {
                Bob b = hit.transform.GetComponent<Bob>();
                if (b != null)
                {
                    hit.transform.position = new Vector3(transform.position.x + dir * 0.5f, hit.transform.position.y + 1f, hit.transform.position.z);
                    b.grabbed();
                    isGrabbing = true;
                    grabbed = hit.transform.gameObject;
                }
                BabyPenguin penguin = hit.transform.GetComponent<BabyPenguin>();
                if (penguin != null)
                {
                    hit.transform.position = new Vector3(transform.position.x + dir * 0.5f, hit.transform.position.y + 1f, hit.transform.position.z);
                    penguin.grabbed();
                    isGrabbing = true;
                    grabbed = hit.transform.gameObject;
                }
            }
        }
    }

    public void moveHorizontal(InputAction.CallbackContext context)
    {
        
        if (!inputEnabled || PauseMenu.isPaused)
        {
            return;
        }
        Vector2 inputVec = context.ReadValue<Vector2>();
        if (inputVec.x < 0.2 && inputVec.x > -0.2)
        {
            inputVec.x = 0;
        }
        if (inputVec.y < 0.2 && inputVec.y > -0.2)
        {
            inputVec.y = 0;
        }
        
        if (inputVec.x > 0)
        {
            dir = 1;
        }
        else if (inputVec.x < 0)
        {
            dir = -1;
        }
        if (crouching)
        {
            return;
        }
        inputAcc = new Vector2(inputVec.x * speed, 0);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!inputEnabled || PauseMenu.isPaused)
        {
            return;
        }
        Collider2D col;
        if (crouching && groundedBLJ(out col) && Mathf.Sign(transform.localScale.x) == Mathf.Sign(dir) && dir == 1)
        {
            Vector3 localForward = col.transform.localRotation * new Vector3(1 * Mathf.Sign(col.transform.localScale.x), 0, 0);
            Vector2 bv = body.velocity;
            Debug.Log("BV: " + bv);
            body.velocity += new Vector2(localForward.x, localForward.y) * Time.deltaTime * (Mathf.Pow(Mathf.Abs((body.velocity.magnitude / 5) + 1), 3)) + new Vector2(1f,1f); ;
            //body.velocity = new Vector2(Mathf.Pow((body.velocity.x + localForward.x * Time.deltaTime)*0.2f,3), Mathf.Pow((body.velocity.y + localForward.y * Time.deltaTime)*0.2f,3));
            //Cap the velocity or else it quickly approaches infinity
            bv = body.velocity;
            Debug.Log("BV after: " + bv);
            Debug.Log("LF: " + localForward);
            if (meowCounter > meowCooldown)
            {
                GetComponent<AudioSource>().PlayOneShot(jump);
                meowCounter = 0;
            }
            
            if (body.velocity.magnitude > 210.0f)
            {
                
                body.velocity = body.velocity.normalized * 210.0f ;
            }
            return;
        }
        if (grounded() && (!crouching || (crouching && body.velocity.magnitude > 0.5f))) {
            Debug.Log("JUMPING HAPPENING");
            body.velocity = new Vector2(body.velocity.x, 20);
            GetComponent<AudioSource>().PlayOneShot(jump);
            if (crouching)
            {
                body.velocity += new Vector2(dir * 5,0);
            }
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (!inputEnabled || PauseMenu.isPaused)
        {
            return;
        }
        if (context.performed )
        {
            crouching = true;
            inputAcc = new Vector2(0,0);

        }
        else if(context.canceled)
        {
            crouching = false;
        }
        
        
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (!inputEnabled)
        {
            return;
        }
        if (context.performed && !PauseMenu.isPaused)
        {
            pauseMenu.SetActive(true);
            PauseMenu.isPaused = true;
            Time.timeScale = 0;
        }
        else if (context.performed && PauseMenu.isPaused)
        {
            pauseMenu.SetActive(false);
            PauseMenu.isPaused = false;
            Time.timeScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PauseMenu.isPaused)
        {
            return;
        }
        if (collision.gameObject.tag == "Lava")
        {
            //Burn butt
            body.velocity = new Vector2(4, 20);
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().time = 0.25f;
        }else if (collision.gameObject.tag == "Ice")
        {
            Vector3 localForward = collision.transform.localRotation * new Vector3(1 * Mathf.Sign(collision.transform.localScale.x),0,0);
            body.velocity += new Vector2(localForward.x, localForward.y) * Time.deltaTime*1000;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (PauseMenu.isPaused)
        {
            return;
        }
        if (collision.gameObject.tag == "Ice")
        {
            Vector3 localForward = collision.transform.localRotation * new Vector3(1 * Mathf.Sign(collision.transform.localScale.x), 0, 0);
            body.velocity += new Vector2(localForward.x, localForward.y) * Time.deltaTime * 500;
        }
    }
}
