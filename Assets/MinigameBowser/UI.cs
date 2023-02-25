using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

    //Player
    public GameObject[] playerHeart;
    private int playerHealth;

    //Bowser
    public GameObject[] bowserHeart;
    private int bowserHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 3;
        bowserHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i<  bowserHeart.Length;i++)
        {
            if (i >= Bomb.hitCounter)
            {
                bowserHeart[i].SetActive(true);
            }
            else{
                bowserHeart[i].SetActive(false);
            }
        }
    }

    public bool hitPlayer()
    {
        playerHealth--;
        playerHeart[playerHealth].SetActive(false);
        return playerHealth <= 0;
    }
}
