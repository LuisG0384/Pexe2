using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Graminha : MonoBehaviour
{
    [SerializeField] GameObject[] listaDeGramas;
    [SerializeField] private float tempoSpawn = 2f;

    public Terrain terreno; 

    [SerializeField] private int maximoGramas = 50;
    [SerializeField] private int minimoParaReiniciar = 20;

    private bool spawnAtivo = true;

    private void Start()
    {
        
        if (terreno == null)
            terreno = Terrain.activeTerrain;

        if (listaDeGramas.Length > 0 && terreno != null)
        {
            StartCoroutine(SpawnRotina());
        }
        else
        {
            Debug.LogError("ERRO: O Script precisa do TERRENO para calcular a altura!");
        }
    }

    private IEnumerator SpawnRotina()
    {
        while (true)
        {
            float tempoRandom = Random.Range(tempoSpawn - 0.5f, tempoSpawn + 0.5f);
            yield return new WaitForSeconds(tempoRandom);

            
            int contagemAtual = GameObject.FindGameObjectsWithTag("Graminha").Length;

            
            if (contagemAtual >= maximoGramas) spawnAtivo = false;
            else if (contagemAtual <= minimoParaReiniciar) spawnAtivo = true;

            
            if (spawnAtivo && contagemAtual < maximoGramas)
            {
                SpawnarMatematico();
            }
        }
    }

    void SpawnarMatematico()
    {
        
        Vector3 posicaoDoTerreno = terreno.transform.position;
        TerrainData dados = terreno.terrainData;

        
        float xAleatorio = posicaoDoTerreno.x + Random.Range(0, dados.size.x);
        float zAleatorio = posicaoDoTerreno.z + Random.Range(0, dados.size.z);

        
        float alturaY = terreno.SampleHeight(new Vector3(xAleatorio, 0, zAleatorio));

        
        float yFinal = posicaoDoTerreno.y + alturaY;

        
        Vector3 localDeNascimento = new Vector3(xAleatorio, yFinal, zAleatorio);

        
        GameObject prefabSorteado = listaDeGramas[Random.Range(0, listaDeGramas.Length)];
        Quaternion rotacaoAleatoria = Quaternion.Euler(0, Random.Range(0, 360), 0);

        Instantiate(prefabSorteado, localDeNascimento, rotacaoAleatoria);
    }
}