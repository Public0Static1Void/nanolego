using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramManager : MonoBehaviour
{
    [SerializeField] private GameObject ob_ToShow;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float turn_speed;

    void Update()
    {
        if (ob_ToShow == null) return;

        ob_ToShow.transform.Rotate(transform.up, turn_speed * Time.deltaTime);
        ob_ToShow.transform.Rotate(transform.right, turn_speed * Time.deltaTime);
    }

    public void ChangeObject(GameObject ob)
    {
        ob_ToShow = ob;
        ob_ToShow.transform.position = transform.position + offset;
    }
}