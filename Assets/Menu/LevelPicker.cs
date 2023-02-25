using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelPicker : MonoBehaviour
{
    public GameObject bombclipStar;
    public GameObject penguinStar;
    public GameObject LavafallStar;

    public EventSystem events;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anyButtonSelected();
    }

    public void bombclipStarTriggered()
    {
        SceneManager.LoadScene("Bombclip");
    }

    public void penguinStarTriggered()
    {
        SceneManager.LoadScene("Penguin");
    }

    public void lavafallStarTriggered()
    {
        SceneManager.LoadScene("Lavafall");
    }

    public void bljStarTriggered()
    {
        SceneManager.LoadScene("BLJ");
    }

    public void bowserStarTriggered()
    {
        SceneManager.LoadScene("Bowser");
    }
    private void anyButtonSelected()
    {
        GameObject ob = events.currentSelectedGameObject;
        if (ob == null)
        {
            events.SetSelectedGameObject(bombclipStar);
        }
    }
}
