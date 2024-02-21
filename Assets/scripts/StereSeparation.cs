using System;
using System.Security.Principal;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private Camera XRcamera;
    public void Start()
    {

        Debug.Log(XRcamera.stereoSeparation);
        
    }
    private void Update()
    {
        XRcamera.stereoSeparation = 10f;//XRcamera.stereoSeparation + Time.deltaTime * 1f;
        Debug.Log("stereo separation");
        Debug.Log(XRcamera.stereoSeparation);
    }
}
