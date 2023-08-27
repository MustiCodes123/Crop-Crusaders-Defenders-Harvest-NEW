using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardAssigner : MonoBehaviour
{
    public Transform cam;

    private void Awake()
    {
        Billboard[] billboards = FindObjectsOfType<Billboard>();
        foreach (Billboard billboard in billboards)
        {
            billboard.cam = cam;
        }
    }
}
