using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private CharacterController chController;

    private void Start()
    {
        chController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        chController.Move(new Vector3 (Input.GetAxis("Horizontal") * speed * transform.forward.x, 0, 
                                        Input.GetAxis("Vertical") * speed * transform.forward.y));
        //Debug.Log("Horizontal: " + Input.GetAxis("Horizontal"));
        //Debug.Log("Vertical: " + Input.GetAxis("Vertical"));
    }
    public void Move(InputAction.CallbackContext con)
    {
        if (con.performed)
        {
            Vector2 dir = con.action.ReadValue<Vector2>();
            Debug.Log(dir);
            chController.Move(dir * speed);
        }
    }
}