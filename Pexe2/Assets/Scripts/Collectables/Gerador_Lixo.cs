using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerador_Lixo : MonoBehaviour
{
    [Header("O que spawnar")]
    public GameObject prefabLixo; 

    [Header("Configuração de Quantidade")]
    public int limiteMaximo = 10; 
    public float velocidadeSpawn = 2f;

    [Header("Área de Spawn")]
    public float alturaSpawn = 15f;
    public float tamanhoAreaX = 10f;
    public float tamanhoAreaZ = 10f;

    private void Start()
    {
        if (prefabLixo != null)
        {
            StartCoroutine(RotinaDeSpawn());
        }
        else
        {
            Debug.LogError("ERRO: Arraste o Prefab do Lixo para o script GeradorLixo!");
        }
    }

    IEnumerator RotinaDeSpawn()
    {
        while (true) 
        {
            
            yield return new WaitForSeconds(velocidadeSpawn);

            
            int lixosNaCena = GameObject.FindGameObjectsWithTag("Lixo").Length;

            
            if (lixosNaCena < limiteMaximo)
            {
                SpawnarLixo();
            }
        }
    }

    void SpawnarLixo()
    {
     
        float xAleatorio = Random.Range(-tamanhoAreaX, tamanhoAreaX);
        float zAleatorio = Random.Range(-tamanhoAreaZ, tamanhoAreaZ);

        Vector3 posicaoFinal = new Vector3(
            transform.position.x + xAleatorio,
            alturaSpawn,
            transform.position.z + zAleatorio
        );

        
        Instantiate(prefabLixo, posicaoFinal, Random.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 centro = new Vector3(transform.position.x, alturaSpawn, transform.position.z);
        Vector3 tamanho = new Vector3(tamanhoAreaX * 2, 1, tamanhoAreaZ * 2);
        Gizmos.DrawWireCube(centro, tamanho);
    }
}