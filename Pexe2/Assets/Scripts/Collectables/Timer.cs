using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Timer : MonoBehaviour
{
    [Header("Configurações")]
    public float tempoEmMinutos = 2f; 
    public TMP_Text textoDoTimer;     

    [Header("Visual")]
    public Color corNormal = Color.white;
    public Color corCrítica = Color.red; 

    
    private float tempoRestante;
    private bool estaContando = true;

    void Start()
    {
        
        tempoRestante = tempoEmMinutos * 60;
        textoDoTimer.color = corNormal;
    }

    void Update()
    {
        if (estaContando)
        {
            if (tempoRestante > 0)
            {
                
                tempoRestante -= Time.deltaTime;

                AtualizarDisplay();
            }
            else
            {
                // O tempo acabou!
                tempoRestante = 0;
                estaContando = false;
                TempoEsgotado();
            }
        }
    }

    void AtualizarDisplay()
    {
        
        float minutos = Mathf.FloorToInt(tempoRestante / 60);
        float segundos = Mathf.FloorToInt(tempoRestante % 60);

        
        textoDoTimer.text = string.Format("{0:00}:{1:00}", minutos, segundos);

        // --- EFEITO DE TENSÃO ---
        // Se faltar menos de 10 segundos, muda a cor para Vermelho
        if (tempoRestante <= 10f)
        {
            textoDoTimer.color = corCrítica;
        }
    }

    void TempoEsgotado()
    {
        
        textoDoTimer.text = "00:00";

        Debug.Log("GAME OVER! O tempo acabou.");

        // AQUI VOCÊ COLOCA A LÓGICA DE PERDER O JOGO
        // Exemplo:
        // FindObjectOfType<GameManager>().GameOver();
    }
}