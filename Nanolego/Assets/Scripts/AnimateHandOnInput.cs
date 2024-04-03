using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;

    public Animator handAnimator;

    [SerializeField] private TMP_Text trigger_text;
    [SerializeField] private TMP_Text grip_text;
    [SerializeField] private TMP_Text room_text;

    public float triggerValue;
    public float gripValue;


    // Update is called once per frame
    void Update()
    {
        triggerValue = pinchAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);
        trigger_text.text = "Trigger:" + triggerValue.ToString();

        gripValue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
        grip_text.text = "Grip: " + gripValue.ToString();
    }
}