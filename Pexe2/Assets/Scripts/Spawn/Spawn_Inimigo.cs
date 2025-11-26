using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Inimigo : MonoBehaviour
{
    [Header("Configuração dos Inimigos")]
    [SerializeField] GameObject[] listaDeInimigos; 
    [SerializeField] private float inimigoTimer = 5f;
    private int maximoTuba = 7;
    private int maximoPira = 20;
    [SerializeField] CharacterController controller;

    public BoxCollider areaDeSpawn; 

    SpawnManagerScript manager;

    private void Awake()
    {
        if (transform.parent != null)
        {
            manager = transform.parent.GetComponent<SpawnManagerScript>();
        }

        if (controller == null)
        {
            controller = GetComponent<CharacterController>();
        }

        if (areaDeSpawn == null)
            areaDeSpawn = GetComponent<BoxCollider>();
    }



    private void Start()
    {
        if (listaDeInimigos.Length > 0 && areaDeSpawn != null)
        {
            StartCoroutine(SpawnInimigosRoutine());
        }
        else
        {
            Debug.LogError("ERRO: Verifique se a Lista de Inimigos tem itens E se o BoxCollider existe!");
        }
    }

    private IEnumerator SpawnInimigosRoutine()
    {
        while (true)
        {
            
            float tempoAleatorio = Random.Range(inimigoTimer - 1f, inimigoTimer + 1f);
            if (tempoAleatorio < 1f) tempoAleatorio = 1f;

            yield return new WaitForSeconds(tempoAleatorio);

            
            int contagemT = GameObject.FindGameObjectsWithTag("Inimigue").Length;
            int contagemP = GameObject.FindGameObjectsWithTag("Inimigue2").Length;


            if (manager != null && manager.pode && contagemT < maximoTuba && contagemP < maximoPira)
            {
                
                int indexAleatorio = Random.Range(0, listaDeInimigos.Length);
                GameObject prefabSorteado = listaDeInimigos[indexAleatorio];

                
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