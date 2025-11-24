using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Inimigo : MonoBehaviour
{
    [Header("Configuração do Inimigo")]
    [SerializeField] GameObject inimigo; 
    [SerializeField] private float inimigoTimer = 5f; 
    [SerializeField] private int maximoInimigos = 3;  

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
        
        if (inimigo != null)
        {
            StartCoroutine(SpawnInimigosRoutine());
        }
        else
        {
            Debug.LogError("ERRO: Você esqueceu de arrastar o Inimigo para o Inspector!");
        }
    }

    private IEnumerator SpawnInimigosRoutine()
    {
        while (true)
        {
          
            float tempoAleatorio = Random.Range(inimigoTimer - 1f, inimigoTimer + 1f);
            if (tempoAleatorio < 1f) tempoAleatorio = 1f;

            yield return new WaitForSeconds(tempoAleatorio);

            
            int contagemAtual = GameObject.FindGameObjectsWithTag("Inimigue").Length;

            
            if (manager != null && manager.pode && contagemAtual < maximoInimigos)
            {
                Instantiate(inimigo, transform.position, Quaternion.identity);
                manager.Aumenta();
            }
        }
    }
}