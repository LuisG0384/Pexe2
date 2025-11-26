using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGlobal : MonoBehaviour
{
    public GameObject prefabLixo;

    public float intervaloSpawn = 2f; 
    public int limiteMaximo = 20;     

    public BoxCollider areaDeSpawn;

    private void Start()
    {
        
        if (areaDeSpawn == null)
            areaDeSpawn = GetComponent<BoxCollider>();

        StartCoroutine(RotinaSpawn());
    }

    IEnumerator RotinaSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloSpawn);

            
            int quantidadeAtual = GameObject.FindGameObjectsWithTag("Lixo").Length;

            if (quantidadeAtual < limiteMaximo)
            {
                SpawnarObjeto();
            }
        }
    }

    void SpawnarObjeto()
    {
       
        Bounds limites = areaDeSpawn.bounds;

       
        float xAleatorio = Random.Range(limites.min.x, limites.max.x);
        float zAleatorio = Random.Range(limites.min.z, limites.max.z);

        
        float alturaY = transform.position.y;

        Vector3 posicaoSorteada = new Vector3(xAleatorio, alturaY, zAleatorio);

       

        
        if (Physics.Raycast(posicaoSorteada, Vector3.down, 1000f))
        {
             Instantiate(prefabLixo, posicaoSorteada, Random.rotation);
        }
        

        Instantiate(prefabLixo, posicaoSorteada, Random.rotation);
    }
}