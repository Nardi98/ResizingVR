using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class HandAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Start()
    {
        //_animator = GetComponent<Animator>();
    }
    public void ChangeTrigger(InputAction.CallbackContext context)
    {
        
        _animator.SetFloat("Trigger", context.ReadValue<float>());
        Debug.Log("trigger value" + context.ReadValue<float>());
    }

    public void ChangeGrip(InputAction.CallbackContext context)
    {
        _animator.SetFloat("Grip", context.ReadValue<float>());
        Debug.Log("grip value" + context.ReadValue<float>());
    }
}
