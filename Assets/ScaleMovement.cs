using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScaleMovement : MonoBehaviour
{
    [SerializeField] private Transform originalObject;
    [SerializeField] private float scaleFactor;
    [SerializeField] private Vector3 translation = new Vector3(0f,0f,0f);
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = originalObject.localPosition * scaleFactor + translation;
        transform.localRotation = originalObject.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = originalObject.localPosition * scaleFactor + translation;
        transform.localRotation = originalObject.localRotation;
    }


}
