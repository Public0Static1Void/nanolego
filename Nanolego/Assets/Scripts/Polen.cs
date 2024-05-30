using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polen : MonoBehaviour
{

    public GameObject objetoACrear;
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Pickable")
        {
            Debug.Log("a");
            Instantiate(objetoACrear);
        }
    }
}
