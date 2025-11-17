using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarBomScript1 : MonoBehaviour
{
    private ComidaMove ComidaMove;

    void Start()
    {
        ComidaMove = GetComponentInParent<ComidaMove>();

        if (ComidaMove == null)
        {
            Debug.LogError("O script RadarDetector não conseguiu encontrar FishMove no objeto pai. A comunicação falhará.");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ComidaMove.Target = other.transform;
            //Debug.Log(other.transform.position.x);
            ComidaMove.isTargetDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ComidaMove.isTargetDetected = false;
        }
    }
}
