using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida : MonoBehaviour
{
    CollectedText indice;

    private void Start()
    {
        indice = FindAnyObjectByType<CollectedText>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            indice.OnCollectedFoodText();  
            Destroy(gameObject);
        }
    }
}
