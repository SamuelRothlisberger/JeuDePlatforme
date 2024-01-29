using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject john;

    void Update()
    {
        if (john != null)
        {
            Vector3 position = transform.position;
            position.x = john.transform.position.x;
            position.y = john.transform.position.y+0.25f;
            transform.position = position;
        }
    }
}
