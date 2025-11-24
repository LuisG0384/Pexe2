using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTerreno_Graminha : MonoBehaviour
{
    [Header("Configuração da Grama")]
    [SerializeField] GameObject[] listaDeGramas;
    [SerializeField] private float tempoSpawn = 2f;

    [Header("Referência do Terreno")]
    public Terrain terreno;

    [Header("Limites")]
    [SerializeField] private int maximoGramas = 50;

    private void Start()
    {
        if (terreno == null) terreno = Terrain.activeTerrain;
        if (terreno == null) Debug.LogError(" ERRO: Não achei nenhum Terreno na cena!");

        StartCoroutine(SpawnRotina());
    }

    private IEnumerator SpawnRotina()
    {
        while (true)
        {
            yield return new WaitForSeconds(tempoSpawn);

            // Tenta spawnar
            SpawnarNoTerreno();
        }
    }

    void SpawnarNoTerreno()
    {
        if (terreno == null) return;

        TerrainData dados = terreno.terrainData;
        Vector3 posicaoTerreno = terreno.transform.position;

        // Escolhe posição
        float x = posicaoTerreno.x + Random.Range(0, dados.size.x);
        float z = posicaoTerreno.z + Random.Range(0, dados.size.z);

        // Começa bem alto (200 metros acima do terreno)
        float yAlto = posicaoTerreno.y + dados.size.y + 200f;

        Vector3 origemDoRaio = new Vector3(x, yAlto, z);

        // --- DEBUG VISUAL ---
        // Desenha uma linha vermelha na Scene (vai aparecer por 2 segundos)
        Debug.DrawRay(origemDoRaio, Vector3.down * 1000f, Color.red, 2f);

        RaycastHit hit;
        if (Physics.Raycast(origemDoRaio, Vector3.down, out hit, 1000f))
        {
            // O raio bateu em alguma coisa! Vamos ver o que é.
            Debug.Log($" Raio bateu em: {hit.collider.gameObject.name} | Tag: {hit.collider.tag}");

            if (hit.collider.CompareTag("Chão") || hit.collider.CompareTag("Terrain"))
            {
                if (listaDeGramas.Length > 0)
                {
                    GameObject prefab = listaDeGramas[Random.Range(0, listaDeGramas.Length)];
                    Instantiate(prefab, hit.point, Quaternion.identity);
                    Debug.Log(" Grama plantada com sucesso!");
                }
            }
            else
            {
                Debug.LogWarning($" Achei chão, mas a Tag está errada! A Tag é '{hit.collider.tag}', mas preciso de 'Chao'.");
            }
        }
        else
        {
            Debug.LogError(" O raio não bateu em NADA! Talvez esteja começando baixo demais ou fora do mapa.");
        }
    }
}