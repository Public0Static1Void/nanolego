using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class PhysicalButton : MonoBehaviour
{
    public float max_y, min_y;

    private Rigidbody rb;

    private bool touchingSomething = false;

    [SerializeField] private UnityEvent programable_event;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        max_y = transform.position.y;
        min_y = transform.position.y - transform.localScale.y / 2;
    }

    void FixedUpdate()
    {
        if (!touchingSomething && transform.position.y < max_y)
        {
            rb.AddForce(transform.up * 5, ForceMode.Force);
        }
        else if (transform.position.y > max_y)
            transform.position = new Vector3(transform.position.x, max_y, transform.position.z);
        else if (transform.position.y < min_y)
        {
            Debug.Log("A");
            programable_event.Invoke();
            transform.position = new Vector3(transform.position.x, min_y, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        touchingSomething = true;
    }
    private void OnCollisionExit(Collision coll)
    {
        touchingSomething = false;
    }
}