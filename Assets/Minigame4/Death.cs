using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    private float countdown = 1f;
    private float counter = 0.0f;
    private bool triggered = false;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!triggered)
        {
            return;
        }
        counter += Time.deltaTime;

        if (counter >= countdown)
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggered = true;
        canvas.SetActive(true);
        //PlayerPrefs.SetFloat("PenguinCurrent", Time.timeSinceLevelLoad);
    }
}
