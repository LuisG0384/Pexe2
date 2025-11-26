using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float tempoEmMinutos = 1f; 
    public TMP_Text textoDoTimer;     

    public Color corNormal = Color.white;
    public Color corCrítica = Color.red; 

    
    private float tempoRestante;
    private bool estaContando = true;

    [SerializeField] CollectedText points;

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

        if (tempoRestante <= 10f)
        {
            textoDoTimer.color = corCrítica;
        }
    }

    void TempoEsgotado()
    {
        
        textoDoTimer.text = "00:00";

        PontosScript.pontuacao = int.Parse(points.texto.text);
        SceneManager.LoadScene("Final");
    }
}