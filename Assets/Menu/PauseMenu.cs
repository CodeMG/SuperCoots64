using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public EventSystem events;
    public GameObject defaultButton;
    public string levelpref;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        anyButtonSelected();
    }
    private void anyButtonSelected()
    {
        GameObject ob = events.currentSelectedGameObject;
        if (ob == null)
        {
            events.SetSelectedGameObject(defaultButton);
        }
    }

    public void levelButton()
    {
        
        SceneManager.LoadScene("LevelPicker");
        resumeButton();
    }

    public void menuButton()
    {
        SceneManager.LoadScene("MainMenu");
        resumeButton();
    }

    public void restartButton()
    {
        //PlayerPrefs.SetFloat(levelpref + "Current", Time.timeSinceLevelLoad);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        resumeButton();
    }

    public void resumeButton()
    {
        gameObject.SetActive(false);
        PauseMenu.isPaused = false;
        Time.timeScale = 1;
    }

}
