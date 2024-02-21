using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class SceneSettingsUI : MonoBehaviour
{

    [SerializeField] private Toggle _visibleHands;
    [SerializeField] private Toggle _scaledIPD;

    private bool _visibleHandsEnabled;
    private bool _scaledIPDEnabled;

    public bool VisibleHandsEnabled { get => _visibleHandsEnabled;  }
    public bool ScaledIPDEnabled { get => _scaledIPDEnabled; }

    private void Start()
    {

        _visibleHandsEnabled = _visibleHands.isOn;
        _scaledIPDEnabled = _scaledIPD.isOn;


        _visibleHands.onValueChanged.AddListener(delegate 
        {
            ChangeHandsVisibility(_visibleHands.isOn); 
        }) ;

        _scaledIPD.onValueChanged.AddListener(delegate
        {
            ChangeScaledIPD(_scaledIPD.isOn);
        });

    }


    private void ChangeHandsVisibility(bool handVisibility)
    {

        _visibleHandsEnabled = handVisibility;
        Debug.Log("hands" + VisibleHandsEnabled);
    }
        
    private void ChangeScaledIPD(bool scaledIPD)
    {

        _scaledIPDEnabled = scaledIPD;
        Debug.Log("scaled IPD" + ScaledIPDEnabled);
    }
}
