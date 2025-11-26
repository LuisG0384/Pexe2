using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarScript : MonoBehaviour
{
    private InimigueMove InimigueMove;
    GameObject som;
    AudioScript controle;

    float originalVolume;
    float fadeOutDuration = 0.5f;

    void Start()
    {
        InimigueMove = GetComponentInParent<InimigueMove>();
        som = GameObject.Find("AudioManager");
        controle = som.GetComponent<AudioScript>();


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
            InimigueMove.isTargetDetected = controle.inimigoDetectado = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InimigueMove.isTargetDetected = controle.inimigoDetectado = false;
        }
    }
}

