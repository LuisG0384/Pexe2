using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida : MonoBehaviour
{
    CollectedText indice;
    Lives Vidas;

    private void Start()
    {
        indice = FindAnyObjectByType<CollectedText>();
        Vidas = FindAnyObjectByType<Lives>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //indice.OnCollectedFoodText();
            Vidas.OnLifeRestore();
            Destroy(gameObject);
        }
    }
}
