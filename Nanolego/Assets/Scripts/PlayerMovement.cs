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

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (xInput == 0 && yInput == 0)
        {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
        }

        Vector3 moveDirection = Vector3.ProjectOnPlane(main_camera.transform.forward, Vector3.up).normalized;
        Vector3 moveVector = moveDirection * yInput * speed * Time.deltaTime;
        rb.MovePosition(transform.position + moveVector);

        //Camera.main.transform.Rotate(Vector3.up * xInput * turn_speed);
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