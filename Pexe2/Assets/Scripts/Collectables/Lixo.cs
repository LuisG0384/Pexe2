using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lixo : MonoBehaviour
{
    [Header("Configurações de Queda")]
    public float velocidadeAfundar = 2f;
    public float tempoNoChao = 3f; 
    public float velocidadeRotacao = 50f; 

    private bool tocouNoChao = false;
    CollectedText indice;

    private void Start()
    {
        indice = FindAnyObjectByType<CollectedText>();
    }

    private void Update()
    {
        
        if (!tocouNoChao)
        {
            
            transform.Translate(Vector3.down * velocidadeAfundar * Time.deltaTime, Space.World);

           
            transform.Rotate(Vector3.up * velocidadeRotacao * Time.deltaTime);
            transform.Rotate(Vector3.right * (velocidadeRotacao / 2) * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (indice != null) indice.OnCollectedTrashText();
            Destroy(gameObject);
        }

        if (other.CompareTag("Chão"))
        {
            if (!tocouNoChao) 
            {
                tocouNoChao = true;
                StartCoroutine(EsperarECuspirDeNovo());
            }
        }
    }

    IEnumerator EsperarECuspirDeNovo()
    {
        
        yield return new WaitForSeconds(tempoNoChao);

        
        Destroy(gameObject);
    }
}