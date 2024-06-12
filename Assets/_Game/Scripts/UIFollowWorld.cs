using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowWorld : MonoBehaviour
{
    [SerializeField] Camera cam;
    
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
}
