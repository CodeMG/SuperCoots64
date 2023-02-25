using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairGenerator : MonoBehaviour
{
    public GameObject step;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 50;i++)
        {
            GameObject ob = Instantiate(step, step.transform.position + new Vector3(1*i,0.221f*i,0),Quaternion.identity);
            ob.transform.SetParent(transform);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
