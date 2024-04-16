using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectScript : MonoBehaviour
{
    private bool objectSelected = false;
    private Transform ob;

    [SerializeField] private LayerMask pickableMask;

    public Transform ray_origin; // Reference to the XRController

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(ray_origin.position, ray_origin.forward, out hit, 10f))
            Debug.DrawLine(ray_origin.position, ray_origin.forward, Color.green);
        else
            Debug.DrawLine(ray_origin.position, ray_origin.forward, Color.blue);


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

            RaycastHit hit;

            if (Physics.Raycast(ray_origin.position, ray_origin.forward, out hit, 10f))
            {
                Debug.DrawLine(transform.position, hit.point, Color.green);

                Debug.Log(hit.transform.gameObject.name);

                if (hit.transform.gameObject.layer != 7)
                    return;

                ob = hit.transform;
                objectSelected = true;
            }

        }
    }
}