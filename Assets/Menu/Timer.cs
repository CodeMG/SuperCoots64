using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject text;
    void Start()
    {
        if (PlayerPrefs.GetInt("Quickmode") == 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.GetComponent<TextMeshProUGUI>().SetText((PlayerPrefs.GetFloat("Timer"))+" Seconds");
    }
}
