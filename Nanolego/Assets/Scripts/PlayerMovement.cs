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