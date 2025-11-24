using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBom : MonoBehaviour
{
    SpawnManagerScript manager;
    CollectedText indice;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("SpawnerManager").GetComponent<SpawnManagerScript>();
        indice = FindAnyObjectByType<CollectedText>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CorpoDoinimigue"))
        {
            Debug.Log("Comeu");
            Destroy(other.gameObject.transform.parent.gameObject);
            indice.OnCollectedFoodText();
            manager.Diminui();
        }
    }
}