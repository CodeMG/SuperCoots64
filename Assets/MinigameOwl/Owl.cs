using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : MonoBehaviour
{
    float counter = 0;
    float cooldown = 4f;
    int dir = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1 * dir * -1,transform.localScale.y,transform.localScale.z);
        counter += Time.deltaTime;
        if (counter > cooldown)
        {
            dir *= -1;
            counter = 0;
        }
        transform.Translate(new Vector3(4*Time.deltaTime*dir,0,0));
    }
}
