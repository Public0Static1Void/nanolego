using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RoomSystem : MonoBehaviour
{
    [SerializeField] private InputActionProperty rightHandButtons;
    [SerializeField] private TMP_Text room_text;
    public List<Transform> rooms_transform;
    public int current_room = 0;

    // Speed
    [Header("Speed")]
    [SerializeField] private float speed;

    private bool move = false;

    private void Start()
    {
        current_room = 0;
    }
    private void Update()
    {
        /*
        if (!move)
            return;

        if (Vector3.Distance(transform.position, rooms_transform[current_room].position) < 0.05f)
        {
            PlayerMovement.Instance.canMove = true;
            move = true;
        }

        transform.position = Vector3.Lerp(transform.position, rooms_transform[current_room].position, Time.deltaTime);*/
    }
    public void IncreaseRoom(InputAction.CallbackContext con)
    {
        if (con.performed)
        {
            if (current_room < rooms_transform.Count - 1)
                current_room++;

            Debug.Log("Room: " + current_room.ToString());

            MovePlayer();
        }
    }
    public void DecreaseRoom(InputAction.CallbackContext con)
    {
        if (con.performed)
        {
            if (current_room > 0)
                current_room--;

            Debug.Log("Room: " + current_room.ToString());

            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        transform.position = rooms_transform[current_room].position;
        //move = false;
        //PlayerMovement.Instance.canMove = false;
    }
}