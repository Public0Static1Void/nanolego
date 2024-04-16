using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectScript : MonoBehaviour
{
    private bool objectSelected = false;
    private Transform ob;

    [SerializeField] private LayerMask pickableMask;

    private void FixedUpdate()
    {
        if (!objectSelected)
            return;

        ob.position = Vector3.Lerp(ob.position, transform.position, Time.deltaTime);
        ob.rotation = transform.rotation;
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
            Ray ray = new Ray(transform.position, transform.forward);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(transform.position, hit.point, Color.green);
                if (hit.transform.gameObject.layer != pickableMask)
                    return;
                Debug.Log(hit.transform.gameObject.name);
                ob = hit.transform;
                objectSelected = true;
            }
            else
                Debug.DrawLine(transform.position, transform.forward, Color.blue);

        }
    }
}