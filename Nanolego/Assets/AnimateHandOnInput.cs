using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);

        if (triggerValue > 0.9f && !activated)
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
        }

        Vector3.Lerp(transform.position, positions[curr_pos].position, 5);
    }

    private IEnumerator Wait()
    {
        activated = true;
        yield return new WaitForSeconds(0.2f);
        activated = false;
    }
}