using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Comida : MonoBehaviour
{
    [Header("Configuração da Comida")]
    [SerializeField] GameObject comidaPrefab; 
    [SerializeField] private float tempoSpawn = 3f; 

    [Header("Limites de População")]
    [SerializeField] private int maximoComidas = 10; 
    [SerializeField] private int minimoParaReiniciar = 5; 

    private bool spawnAtivo = true; 
    SpawnManagerScript manager;

    private void Awake()
    {
        
        if (transform.parent != null)
        {
            manager = transform.parent.GetComponent<SpawnManagerScript>();
        }
    }

    private void Start()
    {
        if (comidaPrefab != null)
        {
            StartCoroutine(SpawnRotina());
        }
        else
        {
            Debug.LogError("ERRO: Arraste o prefab da Comida para o script Spawn_Comida!");
        }
    }

    private IEnumerator SpawnRotina()
    {
        while (true) 
        {
            
            float tempoRandom = Random.Range(tempoSpawn - 1f, tempoSpawn + 1f);
            if (tempoRandom < 0.5f) tempoRandom = 0.5f;

            yield return new WaitForSeconds(tempoRandom);

           
            int contagemAtual = GameObject.FindGameObjectsWithTag("ComidaDiva").Length;

            
            if (contagemAtual >= maximoComidas)
            {
                
                spawnAtivo = false;
            }
            else if (contagemAtual <= minimoParaReiniciar)
            {
                
                spawnAtivo = true;
            }


            if (spawnAtivo && manager != null && manager.pode && contagemAtual < maximoComidas)
            {
                Instantiate(comidaPrefab, transform.position, Quaternion.identity);
                manager.Aumenta();
            }
        }
    }
}