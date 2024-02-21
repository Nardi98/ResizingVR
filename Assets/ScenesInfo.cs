using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScenesInfo : ScriptableObject
{
    [System.Serializable]
    public struct SceneInformations
    {
        public bool visibleHands;
        public bool scaledIPD;
    }
    [SerializeField]
    public SceneInformations[] info;

    [Range(1, 10)]
    public int scaleFactor;

    public int currentScene = 0;
}
