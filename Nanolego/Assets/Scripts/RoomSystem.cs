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

    [SerializeField] private Transform player;

    private bool move = false;

    private void Start()
    {
        current_room = 0;
    }
    private void Update()
    {
        Debug.Log("Move: " + move + ", room: " + current_room);
        Debug.Log(rooms_transform[current_room]);
        if (Input.GetKeyDown(KeyCode.E))
            move = !move;


        if (move)
        {
            float distance = Vector3.Distance(transform.position, rooms_transform[current_room].position);

            if (rooms_transform[current_room].position.y > transform.position.y && distance > 0.1f)
                transform.Translate(transform.up * 5 * Time.deltaTime);
            else if (distance > 0.1f)
                transform.Translate(transform.up * -5 * Time.deltaTime);
            else if (distance < 0.1f)
                move = false;
        }
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
        //transform.position = rooms_transform[current_room].position;
        /*while (transform.position != rooms_transform[current_room].position)
        {
            Vector3.Lerp(transform.position, rooms_transform[current_room].position, 5);
        }*/

        StartCoroutine(WaitToFalse());
    }

    private IEnumerator WaitToFalse()
    {
        move = true;
        yield return new WaitForSeconds(3);
        move = false;
    }
}