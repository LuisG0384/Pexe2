using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarScript : MonoBehaviour
{
    private InimigueMove InimigueMove;

    void Start()
    {
        InimigueMove = GetComponentInParent<InimigueMove>();

        if (InimigueMove == null)
        {
            Debug.LogError("O script RadarDetector não conseguiu encontrar FishMove no objeto pai. A comunicação falhará.");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            InimigueMove.Target = other.transform;
            //Debug.Log(other.transform.position.x);
            InimigueMove.isTargetDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InimigueMove.isTargetDetected = false;
        }
    }
}
