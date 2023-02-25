using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarButton : MonoBehaviour
{
    public string levelpref;
    public GameObject unlockedStar;
    public GameObject lockedStar;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(levelpref))
        {
            unlockedStar.SetActive(true);
            lockedStar.SetActive(false);
            GetComponent<Button>().targetGraphic = unlockedStar.GetComponent<Image>();
        }
        else
        {
            unlockedStar.SetActive(false);
            lockedStar.SetActive(true);
            GetComponent<Button>().targetGraphic = lockedStar.GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
