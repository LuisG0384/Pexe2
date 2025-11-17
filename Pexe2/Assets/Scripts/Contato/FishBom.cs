using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBom : MonoBehaviour
{ 
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CorpoDoinimigue"))
        {
            Debug.Log("Comeu");
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}