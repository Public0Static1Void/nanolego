using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    public bool canMove;

    [Header("Movimiento")]
    [SerializeField] private float speed;
    [SerializeField] private float turn_speed;
    private Rigidbody rb;

    [SerializeField] private Transform main_camera;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        canMove = true;
    }

    private void Update()
    {
        if (!canMove)
            return;

        Vector3 moveDirection = Vector3.ProjectOnPlane(main_camera.transform.forward, Vector3.up).normalized;
        Vector3 moveVector = moveDirection * Input.GetAxis("Vertical") * speed;
        rb.MovePosition(transform.position + moveVector);

        transform.Rotate(transform.up * Input.GetAxis("Horizontal") * turn_speed);
    }
    public void Move(InputAction.CallbackContext con)
    {
        if (con.performed)
        {
            Vector2 dir = con.action.ReadValue<Vector2>();
            Debug.Log(dir);
        }
    }
}