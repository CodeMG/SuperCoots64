using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Highscore : MonoBehaviour
{
    public bool menuHighscore;
    public GameObject bombclip;
    public GameObject penguin;
    public GameObject lavafall;
    public GameObject blj;
    public GameObject bowser;

    public GameObject total;

    public GameObject button;
    public EventSystem events;
    // Start is called before the first frame update
    void Start()
    {
        if (menuHighscore)
        {
            

            bombclip.GetComponent<TextMeshProUGUI>().SetText("Clip through the gate:\t" +PlayerPrefs.GetFloat("bombclipBest") + " Seconds");
            penguin.GetComponent<TextMeshProUGUI>().SetText("Penguin on the run:\t" + PlayerPrefs.GetFloat("penguinBest") + " Seconds");
            lavafall.GetComponent<TextMeshProUGUI>().SetText("Ride the Lavafall:\t\t" + PlayerPrefs.GetFloat("lavafallBest") + " Seconds");
            blj.GetComponent<TextMeshProUGUI>().SetText("BLJ into the Pipe:\t\t" + PlayerPrefs.GetFloat("bljBest") + " Seconds");
            bowser.GetComponent<TextMeshProUGUI>().SetText("Battle in the Sky:\t\t" + PlayerPrefs.GetFloat("bowserBest") + " Seconds");
            total.GetComponent<TextMeshProUGUI>().SetText("Total:\t" +PlayerPrefs.GetFloat("TimerBest"));

        }
        else
        {
            bombclip.GetComponent<TextMeshProUGUI>().SetText("Clip through the gate:\t" + PlayerPrefs.GetFloat("bombclipCurrent") + "\t" + "Best time: " + PlayerPrefs.GetFloat("bombclipBest"));
            penguin.GetComponent<TextMeshProUGUI>().SetText("Penguin on the run:\t" + PlayerPrefs.GetFloat("penguinCurrent") + "\t" + "Best time: " + PlayerPrefs.GetFloat("penguinBest"));
            lavafall.GetComponent<TextMeshProUGUI>().SetText("Ride the Lavafall:\t\t" + PlayerPrefs.GetFloat("lavafallCurrent") + "\t" + "Best time: " + PlayerPrefs.GetFloat("lavafallBest"));
            blj.GetComponent<TextMeshProUGUI>().SetText("BLJ into the Pipe:\t\t" + PlayerPrefs.GetFloat("bljCurrent") + "\t" + "Best time: " + PlayerPrefs.GetFloat("bljBest"));
            bowser.GetComponent<TextMeshProUGUI>().SetText("Battle in the Sky:\t\t" + PlayerPrefs.GetFloat("bowserCurrent") + "\t" + "Best time: " + PlayerPrefs.GetFloat("bowserBest"));
            total.GetComponent<TextMeshProUGUI>().SetText("Total:\t" + PlayerPrefs.GetFloat("Timer") + "\t" + "Best time: " + PlayerPrefs.GetFloat("TimerBest"));
            if (!PlayerPrefs.HasKey("TimerBest") || PlayerPrefs.GetFloat("Timer") < PlayerPrefs.GetFloat("TimerBest"))
            {
                PlayerPrefs.SetFloat("TimerBest", PlayerPrefs.GetFloat("Timer"));
            }
            string levelpref = "bombclip";
            if (PlayerPrefs.GetFloat(levelpref + "Current") < PlayerPrefs.GetFloat(levelpref + "Best") || !PlayerPrefs.HasKey(levelpref + "Best"))
            {
                PlayerPrefs.SetFloat(levelpref + "Best", PlayerPrefs.GetFloat(levelpref + "Current"));
            }
            levelpref = "penguin";
            if (PlayerPrefs.GetFloat(levelpref + "Current") < PlayerPrefs.GetFloat(levelpref + "Best") || !PlayerPrefs.HasKey(levelpref + "Best"))
            {
                PlayerPrefs.SetFloat(levelpref + "Best", PlayerPrefs.GetFloat(levelpref + "Current"));
            }
            levelpref = "bowser";
            if (PlayerPrefs.GetFloat(levelpref + "Current") < PlayerPrefs.GetFloat(levelpref + "Best") || !PlayerPrefs.HasKey(levelpref + "Best"))
            {
                PlayerPrefs.SetFloat(levelpref + "Best", PlayerPrefs.GetFloat(levelpref + "Current"));
            }
            levelpref = "blj";
            if (PlayerPrefs.GetFloat(levelpref + "Current") < PlayerPrefs.GetFloat(levelpref + "Best") || !PlayerPrefs.HasKey(levelpref + "Best"))
            {
                PlayerPrefs.SetFloat(levelpref + "Best", PlayerPrefs.GetFloat(levelpref + "Current"));
            }
            levelpref = "lavafall";
            if (PlayerPrefs.GetFloat(levelpref + "Current") < PlayerPrefs.GetFloat(levelpref + "Best") || !PlayerPrefs.HasKey(levelpref + "Best"))
            {
                PlayerPrefs.SetFloat(levelpref + "Best", PlayerPrefs.GetFloat(levelpref + "Current"));
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        anyButtonSelected();
    }

    public void finish()
    {

        SceneManager.LoadScene("MainMenu");
    }

    private void anyButtonSelected()
    {
        GameObject ob = events.currentSelectedGameObject;
        if (ob == null)
        {
            events.SetSelectedGameObject(button);
        }
    }
}
