using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ExperimentManager : MonoBehaviour
{
    [SerializeField] GameObject _XROriginGameObject;
    private XROrigin _XROrigin;
    private float _cameraHeight;
    [Range(1, 10)]
    [SerializeField] int _scaleFactor = 1;

    [SerializeField] private bool _scaledIPD = true;
    [SerializeField] private bool _visibleHands = true;



    [Header("Unscaled IPD hands")]
    [SerializeField] GameObject _normalIPDRight;
    [SerializeField] GameObject _normalIPDLeft;

    [Header("Scaled IPD hands")]
    [SerializeField] GameObject _scaledIPDRight;
    [SerializeField] GameObject _scaledIPDLeft;

    public void Start()
    {
        _XROrigin = _XROriginGameObject.GetComponent<XROrigin>();
        _cameraHeight = _XROrigin.CameraYOffset;
        //_XROrigin.CurrentTrackingOriginMode = UnityEngine.XR.TrackingOriginModeFlags.Device;

        if (!_scaledIPD)
        {
            _XROriginGameObject.transform.localScale = new Vector3(1, 1, 1);
            Debug.Log(_XROriginGameObject.transform.localScale);
            _XROrigin.CameraYOffset = _cameraHeight / (float)_scaleFactor;

            SetHandsScale(_scaledIPD);
        }
        else
        {
            _XROriginGameObject.transform.localScale = new Vector3(1 / (float)_scaleFactor, 1 / (float)_scaleFactor, 1 / (float)_scaleFactor);
            Debug.Log(_XROriginGameObject.transform.localScale);
            _XROrigin.CameraYOffset = _cameraHeight;

            SetHandsScale(_scaledIPD);
        }

        _normalIPDRight.GetComponent<ScaleMovement>().ScaleFactor = _scaleFactor;
        _normalIPDLeft.GetComponent<ScaleMovement>().ScaleFactor = _scaleFactor;
    }
    public void ChangeIPD(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        Debug.Log("change IPD settings");

        if (_XROriginGameObject.transform.localScale.x == 1f)
        {
            _XROriginGameObject.transform.localScale = new Vector3(1 / (float)_scaleFactor, 1 / (float)_scaleFactor, 1 / (float)_scaleFactor);
            Debug.Log(_XROriginGameObject.transform.localScale);
            _XROrigin.CameraYOffset = _cameraHeight;
            SetHandsScale(false);
        }
        else
        {
            _XROriginGameObject.transform.localScale = new Vector3(1, 1, 1 );
            Debug.Log(_XROriginGameObject.transform.localScale);
            _XROrigin.CameraYOffset = _cameraHeight/(float)_scaleFactor;
            SetHandsScale(true) ;
        }

    }

    private void SetHandsScale(bool scaled)
    {
        _normalIPDLeft.SetActive(!scaled && _visibleHands);
        _normalIPDRight.SetActive(!scaled && _visibleHands);

        _scaledIPDLeft.SetActive(scaled && _visibleHands);
        _scaledIPDRight.SetActive(scaled && _visibleHands);

        _scaledIPD = scaled;
    }

    private void HandsVisibility(bool visible)
    {

        _normalIPDLeft.SetActive(!_scaledIPD && visible);
        _normalIPDRight.SetActive(!_scaledIPD && visible);

        _scaledIPDLeft.SetActive(_scaledIPD && visible);
        _scaledIPDRight.SetActive(_scaledIPD && visible);


    }
    public void ChangeHandsVisibility(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        _visibleHands = !_visibleHands;
        HandsVisibility(_visibleHands);
        

    }

}
