using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform follow;
    public bool followXDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float deltaYPos = (follow.position.y+2) - transform.position.y;
        transform.Translate(new Vector3(0,deltaYPos*deltaYPos*Mathf.Sign(deltaYPos),0)*Time.deltaTime*2);
        if (followXDir)
        {
            float deltaXPos = (follow.position.x ) - transform.position.x;
            transform.Translate(new Vector3(deltaXPos * deltaXPos * Mathf.Sign(deltaXPos), 0,0) * Time.deltaTime * 2);
        }
    }
}
