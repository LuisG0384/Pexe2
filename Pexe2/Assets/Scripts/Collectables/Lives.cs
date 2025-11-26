using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necessário para o Slider

public class Lives : MonoBehaviour
{
    [Header("UI da Vida")]
    public Slider barraDeVida; // Arraste seu Slider aqui no Inspector

    [Header("Configuração")]
    public int maxLives = 100; // Vida máxima (ex: 100%)
    private int currentLives;

    private void Start()
    {
        currentLives = maxLives;

        // Configura a barra logo no início
        if (barraDeVida != null)
        {
            barraDeVida.maxValue = maxLives;
            barraDeVida.value = currentLives;
        }
        else
        {
            Debug.LogError("ERRO: Você esqueceu de arrastar o Slider para o script Lives!");
        }
    }

    public void OnLifeRestore()
    {
        // Se a vida não estiver cheia, cura
        if (currentLives < maxLives)
        {
            currentLives += 10; // Cura 10 pontos (ajuste como quiser)

            // Não deixa passar do máximo
            if (currentLives > maxLives) currentLives = maxLives;

            UpdateVisuals();
        }
    }

    public void OnHitTaken()
    {
        if (currentLives > 0)
        {
            currentLives -= 10; // Perde 10 pontos de dano (ajuste como quiser)

            // Garante que não fique negativo
            if (currentLives < 0) currentLives = 0;

            UpdateVisuals();

            // Lógica de Morte (Vida zerou)
            if (currentLives <= 0)
            {
                PararTimer();
                // Aqui você pode chamar o Game Over
                Debug.Log("Morreu!");
            }
        }
    }

    void UpdateVisuals()
    {
        if (barraDeVida != null)
        {
            // Atualiza o slider visualmente
            barraDeVida.value = currentLives;
        }
    }

    void PararTimer()
    {
        // Tenta achar o Timer (verifique se o nome do seu script é Timer ou TimerRegressivo)
        Timer timer = FindAnyObjectByType<Timer>();
        if (timer != null)
        {
            timer.enabled = false;
        }
        else
        {
            // Caso esteja usando o script que fizemos antes
            Timer timerR = FindAnyObjectByType<Timer>();
            if (timerR != null) timerR.enabled = false;
        }
    }
}