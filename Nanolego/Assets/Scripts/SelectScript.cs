using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
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

    // Puerta
    private bool door = false;
    private Transform door_parent;

    private bool wait = false;

    private void Start()
    {
        objectsOnZone = new List<Transform>();
    }

    private void FixedUpdate()
    {
        if (objectSelected && Input.GetKeyDown(KeyCode.F))
        {
            ob.GetComponent<Collider>().enabled = true;
            if (ob.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.useGravity = true;
            }
            objectSelected = false;
            return;
        }
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (objectSelected && Input.GetKeyDown(KeyCode.F) && hit.transform.tag == "Pickable")
            {
                ob.GetComponent<Collider>().enabled = true;
                if (ob.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    rb.useGravity = true;
                }
                objectSelected = false;
                return;
            }
            else if (Input.GetKeyDown(KeyCode.F) && hit.transform.tag == "Pickable")
            {
                hit.transform.GetComponent<Collider>().enabled = false;
                ob = hit.transform;

                if (ob.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    rb.useGravity = false;
                }
                objectSelected = true;
            }


            if (hit.transform.tag != "Player")
                mirilla.position = hit.point;
            if (hit.transform.tag == "Button" && Input.GetKey(KeyCode.F))
            {
                Button bt = hit.transform.GetComponent<Button>();
                bt.onClick.Invoke();
            }

        }

        if (!objectSelected)
            return;

        if (ob != null)
        {
            ob.position = Vector3.Lerp(ob.position, transform.position + transform.forward, Time.deltaTime);
            ob.rotation = transform.rotation;
        }
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
                ob.GetComponent<Collider>().enabled = true;
                if (ob.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    rb.useGravity = true;
                }
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

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                name_text.text = hit.transform.name + ", " + hit.transform.tag;

                if (((hit.transform.tag != pickableTag || hit.transform.tag == "Platillo") && hit.transform.tag != "Button") || Vector3.Distance(transform.position, hit.transform.position) > range)
                    return;

                name_text.text = hit.transform.name;

                if (hit.transform.tag == pickableTag)
                {
                    ob = hit.transform;

                    hit.transform.GetComponent<Collider>().enabled = false;
                    if (ob.TryGetComponent<Rigidbody>(out Rigidbody rb))
                    {
                        rb.useGravity = false;
                    }
                }
                if (hit.transform.tag == "Button")
                {
                    Button bt = hit.transform.GetComponent<Button>();
                    bt.onClick.Invoke();
                }
                else
                    objectSelected = true;
            }
        }
    }

    private IEnumerator WaitABit()
    {
        wait = true;
        yield return new WaitForSeconds(0.2f);
        wait = false;
    }


    /*private void OnDrawGizmos()
    {
        if (Selection.activeObject == this.gameObject)
            Gizmos.DrawWireSphere(transform.position, range);
    }*/
}