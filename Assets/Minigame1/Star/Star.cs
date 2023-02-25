using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Star : MonoBehaviour
{
    public string levelpref;
    public SpriteMask mask;
    public Animator animator;

    private string currentState;
    private bool countdownStart;
    private float current = 0;
    public  float countdown = 3f;
    private float currentTime = 0.0f;
    private float currentTotalTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        mask = GetComponentInChildren<SpriteMask>();
        animator = GetComponent<Animator>();
        currentState = "Base Layer.Idle";
        current = 0;
        currentTime = PlayerPrefs.GetFloat(levelpref+"Current");
        currentTotalTime = PlayerPrefs.GetFloat("Timer");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (countdownStart)
        {
            current += Time.deltaTime;
            if (current >= countdown)
            {
                if (PlayerPrefs.GetInt("Quickmode") == 0)
                {
                    SceneManager.LoadScene("LevelPicker");
                }
                else
                {

                    if (levelpref == "bombclip")
                    {
                        
                        SceneManager.LoadScene("Penguin");
                    }
                    else if (levelpref == "penguin")
                    {
                        SceneManager.LoadScene("Lavafall");
                    }
                    else if (levelpref == "lavafall")
                    {
                        SceneManager.LoadScene("BLJ");
                    }
                    else if (levelpref == "blj")
                    {
                        SceneManager.LoadScene("Bowser");
                    }
                    else
                    {
                        SceneManager.LoadScene("HighScore");
                    }
                }
                
            }
        }
        else
        {
            PlayerPrefs.SetFloat(levelpref+"Current",currentTime + Time.timeSinceLevelLoad);
            PlayerPrefs.SetFloat("Timer",currentTotalTime+Time.timeSinceLevelLoad);
            //PlayerPrefs.SetFloat(levelpref+"Current",);
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Play animation
        PB1 player = collision.gameObject.GetComponent<PB1>();
        PBTopDown pBTopDown = collision.gameObject.GetComponent<PBTopDown>();
        if ((player != null||pBTopDown != null) && !countdownStart)
        {
            if (player != null)
            {
                player.gotStar();
            }
            //PlayerPrefs.SetFloat(levelpref+"Current",PlayerPrefs.GetFloat(levelpref+"Current")+Time.timeSinceLevelLoad);
            PlayerPrefs.SetInt(levelpref,1);
            changeAnimationState("Base Layer.CloseLevel");
            countdownStart = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<AudioSource>().Play();
        }
        //Load new scene
    }
}
