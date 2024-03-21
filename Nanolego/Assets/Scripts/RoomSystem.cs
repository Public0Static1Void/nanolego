using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomSystem : MonoBehaviour
{
    [SerializeField] private InputActionProperty rightHandButtons;
    [SerializeField] private TMP_Text room_text;
    public List<Transform> rooms_transform;
    public int current_room = 0;

    [SerializeField] private Transform player;
    void Update()
    {
        room_text.text = "Room: " + current_room.ToString();
        float btn_pressed = rightHandButtons.action.ReadValue<float>();
    }

    public void IncreaseRoom(InputAction.CallbackContext con)
    {
        if (con.performed)
        {
            if (current_room < rooms_transform.Count)
                current_room++;

            transform.position = rooms_transform[current_room].position;
        }
    }
    public void DecreaseRoom(InputAction.CallbackContext con)
    {
        if (con.performed)
        {
            if (current_room > 0)
                current_room--;

            transform.position = rooms_transform[current_room].position;
        }
    }
}