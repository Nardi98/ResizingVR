using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private XRManualControl _xrControl = new XRManualControl();
    [SerializeField] private ScenesInfo _sceneInfo;
    
    [SerializeField] private SceneSettingsUI[] _sceneSettingsUI;

    [SerializeField] private TMP_Text _scaleFactorUI;
    private int _scaleFactor = 1;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("entering scene the manager");

        //StartCoroutine(_xrControl.StartXRCoroutine());

    }

    // Update is called once per frame
    void Update()
    {
 

      
        
    }

    public void StartExperiment()
    {
        Debug.Log("setting up experiment...");
        _sceneInfo.info = new ScenesInfo.SceneInformations[_sceneSettingsUI.Length];

        for(int i = 0;  i < _sceneSettingsUI.Length; i++)
        {
            _sceneInfo.info[i].scaledIPD = _sceneSettingsUI[i].ScaledIPDEnabled;
            _sceneInfo.info[i].visibleHands = _sceneSettingsUI[i].VisibleHandsEnabled; 
            Debug.Log("Scene number " + i);
            Debug.Log("IPD " + _sceneSettingsUI[i].ScaledIPDEnabled + " " + "hands " + _sceneSettingsUI[i].VisibleHandsEnabled);
            //Debug.Log("hands " + _sceneSettingsUI[i].ScaledIPDEnabled);

        }

        _sceneInfo.currentScene = 0;
        _sceneInfo.scaleFactor = _scaleFactor;

        //CheckSettings();
       
        UnityEngine.SceneManagement.SceneManager.LoadScene("ExperimentScene", LoadSceneMode.Single);
    }

    public void ChangeScaleFactorUI(float scaleFactor)
    {
        _scaleFactorUI.text = scaleFactor.ToString();
        _scaleFactor = (int)scaleFactor;
    }
    private void CheckSettings()
    {

        Debug.Log("CheckSettings");
        foreach(ScenesInfo.SceneInformations scene in _sceneInfo.info)
        {
            Debug.Log("Scene");
            Debug.Log(scene.scaledIPD + " " + scene.visibleHands);
        }
        
    }
}
