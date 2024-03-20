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

    public List<Transform> positions;
    private int curr_pos = 0;
    private bool activated = false;

    [SerializeField] private TMP_Text trigger_text;
    [SerializeField] private TMP_Text grip_text;
    [SerializeField] private TMP_Text room_text;

    public float triggerValue;
    public float gripValue;

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        triggerValue = pinchAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);
        trigger_text.text = "Trigger:" + triggerValue.ToString();

        gripValue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
        grip_text.text = "Grip: " + gripValue.ToString();

        if (room_text != null )
            room_text.text = "Room: " + curr_pos.ToString();

        if (triggerValue == 1)
            RoomSystem.instance.current_room++;

        /*if (triggerValue > 0.9f && !activated)
        {
            if (curr_pos < positions.Count)
                curr_pos++;
            StartCoroutine(Wait());
        }
        if (gripValue > 0.9f && !activated)
        {
            if (curr_pos > 0)
                curr_pos--;
            StartCoroutine(Wait());
        }*/

        //Vector3.Lerp(transform.position, positions[curr_pos].position, 5);
    }

    private IEnumerator Wait()
    {
        activated = true;
        yield return new WaitForSeconds(0.2f);
        activated = false;
    }
}