using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject staticCameraOne;
    public GameObject staticCameraTwo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        staticCameraOne.SetActive(false);
        staticCameraTwo.SetActive(true);
    }
}
