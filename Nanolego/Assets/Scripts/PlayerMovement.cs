using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    public bool canMove;

    [SerializeField] private float speed;
    private CharacterController chController;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        chController = GetComponent<CharacterController>();

        canMove = true;
    }

    private void Update()
    {
        if (!canMove)
            return;

        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed * Input.GetAxis("Horizontal") * speed * transform.forward.x, 0, Input.GetAxis("Vertical") * speed * transform.forward.z));

        Debug.Log("Horizontal: " + Input.GetAxis("Horizontal"));
        Debug.Log("Vertical: " + Input.GetAxis("Vertical"));
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