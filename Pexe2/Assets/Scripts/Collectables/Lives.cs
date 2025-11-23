using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necessário para mexer com Imagens

public class Lives : MonoBehaviour
{
    [Header("Configuração")]
    public GameObject heartPrefab; // Arraste seu Prefab de coração aqui
    public int maxLives = 3;

    private int currentLives;
    private List<GameObject> hearts = new List<GameObject>();

    private void Start()
    {
        currentLives = maxLives;
        DrawHearts();
    }

    void DrawHearts()
    {
        // Limpa corações antigos (se houver)
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        hearts.Clear();

        // Cria novos corações baseados na vida máxima
        for (int i = 0; i < maxLives; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, transform);
            hearts.Add(newHeart);
        }
    }

    public void OnLifeRestore()
    {
        if (currentLives < maxLives)
        {
            currentLives++;
            UpdateVisuals();
        }
    }

    public void OnHitTaken()
    {
        if (currentLives > 0)
        {
            currentLives--;
            UpdateVisuals();
        }
    }

    void UpdateVisuals()
    {
        // Loop para ligar ou desligar os corações
        for (int i = 0; i < hearts.Count; i++)
        {
            // Se o índice for menor que a vida atual, mostra o coração. Senão, esconde.
            if (i < currentLives)
            {
                hearts[i].SetActive(true);
                // Dica: Em vez de SetActive, você pode trocar o sprite para um "coração vazio"
                // hearts[i].GetComponent<Image>().sprite = fullHeart;
            }
            else
            {
                hearts[i].SetActive(false);
                // hearts[i].GetComponent<Image>().sprite = emptyHeart;
            }
        }
    }
}