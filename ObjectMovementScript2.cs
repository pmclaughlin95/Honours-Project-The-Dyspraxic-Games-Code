using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovementScript2 : MonoBehaviour
{
    float speed = 0;
 
    void Update()
    {
        //Starts Movement
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        //Checks for edges
        if (transform.position.x >= -6)
        {
            speed = 2;
        }
        else if (transform.position.x <= -11.5)
        {
            speed = -2;
        }
    }
}

