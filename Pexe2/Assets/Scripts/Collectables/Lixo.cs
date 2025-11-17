using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Lixo : MonoBehaviour
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
            indice.OnCollectedTrashText();
            Destroy(gameObject);
        }
    }
}
