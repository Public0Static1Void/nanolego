using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palillo : MonoBehaviour
{
    [SerializeField] private GameObject primerPlatillo;
    [SerializeField] private GameObject segundoPlatillo;

    private bool hasPolen;

    void Start()
    {
        segundoPlatillo.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Polen")
        {
            other.gameObject.SetActive(false);
            hasPolen = true;
        }
        if (other.tag == "Platillo" && hasPolen)
        {
            segundoPlatillo.transform.position = primerPlatillo.transform.position;
            segundoPlatillo.transform.rotation = primerPlatillo.transform.rotation;

            primerPlatillo.SetActive(false);
            segundoPlatillo.SetActive(true);
            hasPolen = false;
        }
    }
}