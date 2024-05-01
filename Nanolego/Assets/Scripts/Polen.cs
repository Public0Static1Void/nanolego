using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polen : MonoBehaviour
{

    public GameObject objetoACrear;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "pincel")
        {
            Instantiate(objetoACrear, transform.position, Quaternion.identity);
        }
    }
}
