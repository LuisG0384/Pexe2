using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LixoPeixe : MonoBehaviour
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
            if (indice != null) indice.OnCollectedTrashText();
            Destroy(gameObject);
        }
    }
}