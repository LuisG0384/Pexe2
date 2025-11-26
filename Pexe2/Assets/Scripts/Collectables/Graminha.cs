using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graminha : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            
            //if (indice != null) indice.OnCollectedFoodText();
            Vidas.OnLifeRestore();
            Destroy(gameObject);
        }
    }
}