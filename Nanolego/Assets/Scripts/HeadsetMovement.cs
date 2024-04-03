using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeadsetMovement : MonoBehaviour
{
    [SerializeField] private InputActionProperty headRotation;

    void Update()
    {
        transform.rotation = headRotation.action.ReadValue<Quaternion>();
        Debug.Log("Head: " + headRotation.action.ReadValue<Quaternion>());
    }
}