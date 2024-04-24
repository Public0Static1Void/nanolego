using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectScript : MonoBehaviour
{
    private bool objectSelected = false;
    private Transform ob;

    [SerializeField] private string pickableTag;

    [SerializeField] private TMP_Text name_text;

    [SerializeField] private float range;

    [SerializeField] private Transform mirilla;

    private List<Transform> objectsOnZone;

    private void Start()
    {
        objectsOnZone = new List<Transform>();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform.tag != "Player")
                mirilla.position = hit.point;
            if (hit.transform.tag == pickableTag)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ob = hit.transform;
                    objectSelected = true;
                }
            }
        }

        if (!objectSelected)
            return;

        if (Input.GetKeyDown(KeyCode.F))
            objectSelected = false;

        ob.position = Vector3.Lerp(ob.position, transform.position, Time.deltaTime);
        ob.rotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == pickableTag)
        {
            objectsOnZone.Add(other.transform);
            name_text.text = other.name;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == pickableTag)
        {
            objectsOnZone.Remove(other.transform);
        }
    }
    public void GetSelectedObject(InputAction.CallbackContext con)
    {
        if (con.performed)
        {
            if (objectSelected)
            {
                objectSelected = false;
                return;
            }
            /*
            foreach (Transform t in objectsOnZone)
            {
                ob = t;
                if (Vector3.Distance(transform.position, ob.position) > Vector3.Distance(transform.position, t.position))
                {
                    ob = t;
                }
            }

            if (ob != null)
            {
                objectSelected = true;
                name_text.text = ob.name;
            }
            */

            name_text.text = "si";

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                name_text.text = hit.transform.name + ", " + hit.transform.tag;

                if (hit.transform.tag != pickableTag || Vector3.Distance(transform.position, hit.transform.position) > range)
                    return;

                name_text.text = hit.transform.name;

                objectSelected = true;

                ob = hit.transform;
            }
        }
    }

    /*private void OnDrawGizmos()
    {
        if (Selection.activeObject == this.gameObject)
            Gizmos.DrawWireSphere(transform.position, range);
    }*/
}