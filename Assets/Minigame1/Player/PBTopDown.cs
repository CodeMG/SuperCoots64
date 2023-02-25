using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PBTopDown : MonoBehaviour
{
    private float rotationSpeed;
    int currentCheckpoint;
    public GameObject bowser;
    private bool isGrabbing;
    public GameObject pauseMenu;
    public GameObject UI;
    public GameObject deadScreen;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 10;
        currentCheckpoint = 0;
        isGrabbing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbing)
        {
            transform.Rotate(new Vector3(0, 0, -1 * rotationSpeed * Time.deltaTime));
        }
        rotationSpeed = rotationSpeed * 0.980f;
        if (currentCheckpoint > 1000)
        {
            currentCheckpoint = 0;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool dead = false;
        Bowser b = collision.gameObject.GetComponent<Bowser>();
        if (b.dir == -1)
        {
            GetComponent<AudioSource>().Play();
            dead = UI.GetComponent<UI>().hitPlayer();
            if (dead)
            {
                //stuff
                //PlayerPrefs.SetFloat("bowserCurrent",Time.);
                deadScreen.SetActive(true);

            }
        }
        if (dead)
        {
            return;
        }
        b.grabbed = true;
        isGrabbing = true;
        bowser.transform.SetParent(transform);
        b.dir = 1;
        
    }

    public void Pause(InputAction.CallbackContext context)
    {
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

    public void throwBowser(InputAction.CallbackContext context)
    {
        bowser.GetComponent<Bowser>().thrown(rotationSpeed);
        isGrabbing = false;
        rotationSpeed = 0;
    }

    public void moveHorizontal(InputAction.CallbackContext context)
    {

        Vector2 inputVec = context.ReadValue<Vector2>();
        if (inputVec.x < 0.2 && inputVec.x > -0.2)
        {
            inputVec.x = 0;
        }
        if (inputVec.y < 0.2 && inputVec.y > -0.2)
        {
            inputVec.y = 0;
        }

        inputVec.Normalize();
        Vector2 right = new Vector2(1,0);
        float dot = Vector2.Dot(inputVec,right);
        Vector2 currentVec = checkpointMap(currentCheckpoint);
        Debug.Log(dot);
        if (dot >= currentVec.x && dot <= currentVec.y)
        {
            rotationSpeed += 60.0f;
            currentCheckpoint++;
            Debug.Log(currentCheckpoint);
        }
    }

    public Vector2 checkpointMap(int counter)
    {
        counter = counter % 4;
        if (counter == 0)
        {
            return new Vector2(0.8f,1.1f);
        }
        else if (counter == 1)
        {
            return new Vector2(-1.1f,-0.8f);
        }
        else if (counter == 2)
        {
            return new Vector2(-0.8f,-0.4f);
        }
        else
        {
            return new Vector2(0.4f,0.8f);
        }
    }
}
