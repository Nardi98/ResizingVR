using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class ExperimentManager : MonoBehaviour
{
    [SerializeField] ScenesInfo scenesInfo;

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


    [Header("visible hands objects")]
    [SerializeField] GameObject[] _visibleWithHands;

    [Header("not visible hands objects")]
    [SerializeField] GameObject[] _visibleWithNoHands;

    private XRManualControl _xRControls = new XRManualControl();

    [SerializeField] private bool _measurmentScene = false;

    private void Awake()
    {
        if (!XRSettings.enabled )
        {
            StartCoroutine(_xRControls.StartXRCoroutine());
        }
        else
        {
            Debug.Log("XR already running");
        }
    }
    public void Start()
    {
        _XROrigin = _XROriginGameObject.GetComponent<XROrigin>();
        _cameraHeight = _XROrigin.CameraYOffset;
        //_XROrigin.CurrentTrackingOriginMode = UnityEngine.XR.TrackingOriginModeFlags.Device;

        if(scenesInfo != null)
        {
            Debug.LogWarning("scene info present");
            if (scenesInfo.currentScene < scenesInfo.info.Length)
            {
                Debug.LogWarning("scenes numbers is correct");
                _scaledIPD = scenesInfo.info[scenesInfo.currentScene].scaledIPD;
                _visibleHands = scenesInfo.info[scenesInfo.currentScene].visibleHands;
                _scaleFactor = scenesInfo.scaleFactor;
                scenesInfo.currentScene += 1;
            }
        }

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

        ShowCorrectObjects();
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

        ShowCorrectObjects();


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

    private void ShowCorrectObjects()
    {

        for(int i = 0; i<_visibleWithHands.Length; i++)
        {
            Debug.Log("hada visible objects");
            _visibleWithHands[i].SetActive(_visibleHands );
        }

        for(int i=0; i<_visibleWithNoHands.Length; i++)
        {
            Debug.Log("hands invisible objects");
            _visibleWithNoHands[i].SetActive(!_visibleHands);
        }
        
    }

    public void NextScene()
    {Debug.Log("loading scene");
        if (_measurmentScene && !_visibleHands)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MeasurmentScene", LoadSceneMode.Single);
        }
        else
        {
            
            UnityEngine.SceneManagement.SceneManager.LoadScene("ExperimentScene", LoadSceneMode.Single);
        }
    }
}
