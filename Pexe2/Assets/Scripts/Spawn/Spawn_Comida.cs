using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGlobal_Comida : MonoBehaviour
{
    [Header("Configuração da Comida")]
    [SerializeField] GameObject[] listaDeComidas; 
    [SerializeField] private float tempoSpawn = 3f;

    [Header("Área Global (Arraste o Box Collider)")]
    public BoxCollider areaDeSpawn; 

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

        
        if (areaDeSpawn == null)
            areaDeSpawn = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        if (listaDeComidas.Length > 0 && areaDeSpawn != null)
        {
            StartCoroutine(SpawnRotina());
        }
        else
        {
            Debug.LogError("ERRO: Verifique se a Lista de Comidas tem itens E se o BoxCollider foi arrastado!");
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
                
                int indexAleatorio = Random.Range(0, listaDeComidas.Length);
                GameObject prefabSorteado = listaDeComidas[indexAleatorio];

                
                Bounds limites = areaDeSpawn.bounds;

                float x = Random.Range(limites.min.x, limites.max.x);
                float y = Random.Range(limites.min.y, limites.max.y); 
                float z = Random.Range(limites.min.z, limites.max.z);

                Vector3 posicaoAleatoria = new Vector3(x, y, z);

                
                Quaternion rotacaoAleatoria = Quaternion.Euler(0, Random.Range(0, 360), 0);

                Instantiate(prefabSorteado, posicaoAleatoria, rotacaoAleatoria);

                manager.Aumenta();
            }
        }
    }
}