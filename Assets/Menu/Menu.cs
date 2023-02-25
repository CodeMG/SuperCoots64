using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject creditsPanel;
    public GameObject creditsBackButton;
    public GameObject controlsPanel;
    public GameObject controlsBackButton;
    public GameObject highscorePanel;
    public GameObject highscoreBackButton;

    public GameObject tutorialButton;
    public GameObject quickmodeButton;
    public GameObject creditsButton;
    public GameObject exitButton;
    public EventSystem events;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("bombclip") && PlayerPrefs.HasKey("penguin") && PlayerPrefs.HasKey("lavafall") && PlayerPrefs.HasKey("bowser") && PlayerPrefs.HasKey("blj"))
        {
            quickmodeButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            quickmodeButton.GetComponent<Button>().interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        anyButtonSelected();
    }

    public void creditsBackButtonTriggered()
    {
        mainPanel.SetActive(true);
        creditsPanel.SetActive(false);
        events.SetSelectedGameObject(null);
    }

    public void controlsBackButtonTriggered()
    {
        mainPanel.SetActive(true);
        controlsPanel.SetActive(false);
        events.SetSelectedGameObject(null);
    }

    public void highscoreBackButtonTriggered()
    {
        mainPanel.SetActive(true);
        highscorePanel.SetActive(false);
        events.SetSelectedGameObject(null);
    }

    public void highscoreButtonTriggered()
    {
        mainPanel.SetActive(false);
        highscorePanel.SetActive(true);
        events.SetSelectedGameObject(null);
    }

    public void controlsButtonTriggered()
    {
        mainPanel.SetActive(false);
        controlsPanel.SetActive(true);
        events.SetSelectedGameObject(null);
    }

    public void creditsButtonTriggered()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
        events.SetSelectedGameObject(null);
    }

    public void tutorialButtonTriggered()
    {
        SceneManager.LoadScene("LevelPicker");
        PlayerPrefs.SetInt("Quickmode",0);
    }

    public void quickmodeButtonTriggered()
    {
        PlayerPrefs.SetInt("Quickmode", 1);
        PlayerPrefs.SetFloat("Timer",0f);
        SceneManager.LoadScene("Bombclip");
        PlayerPrefs.SetFloat("bombclipCurrent", 0f);
        PlayerPrefs.SetFloat("penguinCurrent", 0f);
        PlayerPrefs.SetFloat("lavafallCurrent", 0f);
        PlayerPrefs.SetFloat("bljCurrent", 0f);
        PlayerPrefs.SetFloat("bowserCurrent", 0f);

    }

    public void exitButtonTriggered()
    {
        Application.Quit();
    }

    private void anyButtonSelected()
    {
        GameObject ob = events.currentSelectedGameObject;
        if (ob == null)
        {
            if (creditsPanel.active)
            {
                events.SetSelectedGameObject(creditsBackButton);
                return;
            }
            else if(controlsPanel.active){
                events.SetSelectedGameObject(controlsBackButton);
                return;
            }else if (highscorePanel.active)
            {
                events.SetSelectedGameObject(highscoreBackButton);
                return;
            }
            events.SetSelectedGameObject(tutorialButton);
        }
    }
}
