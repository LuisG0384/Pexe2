using UnityEngine;

public class RadarScript : MonoBehaviour
{
    private InimigueMove InimigueMove;
    GameObject som;
    AudioScript controle;

    void Start()
    {
        InimigueMove = GetComponentInParent<InimigueMove>();
        som = GameObject.Find("AudioManager");
        controle = som.GetComponent<AudioScript>();


        if (InimigueMove == null)
        {
            Debug.LogError("O script RadarDetector n�o conseguiu encontrar FishMove no objeto pai. A comunica��o falhar�.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InimigueMove.Target = other.transform;
            //Debug.Log(other.transform.position.x);
            InimigueMove.isTargetDetected = true;
            InimigueMove.isTargetDetected = controle.inimigoDetectado = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InimigueMove.isTargetDetected = false;
            InimigueMove.isTargetDetected = controle.inimigoDetectado = false;
        }
}
}
