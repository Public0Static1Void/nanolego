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

    // Puerta
    private bool door = false;
    private Transform door_parent;

    private void Start()
    {
        objectsOnZone = new List<Transform>();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (Input.GetKeyDown(KeyCode.F) && door)
            {
                door = false;
            }


            if (hit.transform.tag != "Player")
                mirilla.position = hit.point;
            if (hit.transform.tag == "Door")
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    door_parent = hit.transform.parent;
                    door = true;
                }
            }
        }

        if (!objectSelected && !door)
            return;

        if (objectSelected && ob != null)
        {
            ob.position = Vector3.Lerp(ob.position, transform.position, Time.deltaTime);
            ob.rotation = transform.rotation;
        }
        else if (door)
        {
            Vector3 directionToPlayer = transform.position - door_parent.transform.position;
            directionToPlayer.y = 0f; // Ignore vertical component

            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            Quaternion yRotation = Quaternion.Euler(door_parent.transform.rotation.eulerAngles.x, targetRotation.eulerAngles.y / 2 - 10f, door_parent.transform.rotation.eulerAngles.z);

            if (Quaternion.Angle(door_parent.transform.rotation, yRotation) > 0.1f)
            {
                door_parent.transform.rotation = Quaternion.RotateTowards(door_parent.transform.rotation, yRotation, Time.deltaTime * 50);
            }
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
                objectSelected = false;
                return;
            }
            if (door)
            {
                door = false;
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

                if ((hit.transform.tag != pickableTag && hit.transform.tag != "Door") || Vector3.Distance(transform.position, hit.transform.position) > range)
                    return;

                name_text.text = hit.transform.name;

                if (hit.transform.tag == pickableTag)
                {
                    hit.transform.GetComponent<Collider>().enabled = false;
                    ob = hit.transform;
                }
                else if (hit.transform.tag == "Door")
                {
                    door_parent = hit.transform.parent;
                    door = true;
                }

                objectSelected = true;
            }
        }
    }

    /*private void OnDrawGizmos()
    {
        if (Selection.activeObject == this.gameObject)
            Gizmos.DrawWireSphere(transform.position, range);
    }*/
}