using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graminha : MonoBehaviour
{
    CollectedText indice;

    private void Start()
    {
        indice = FindAnyObjectByType<CollectedText>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (indice != null) indice.OnCollectedFoodText();

            
            Destroy(gameObject);
        }
    }
}