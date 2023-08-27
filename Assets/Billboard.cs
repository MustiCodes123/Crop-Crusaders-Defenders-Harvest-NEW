using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam; 

    void Update()
    {
        if (cam != null)
        {
            transform.LookAt(cam.position + cam.forward);
        }
    }
}
