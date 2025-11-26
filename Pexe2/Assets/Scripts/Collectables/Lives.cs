using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections; 

public class Lives : MonoBehaviour
{
    [Header("UI da Vida")]
    public Slider barraDeVida;

    [Header("Tela de Game Over")]
    public GameObject painelGameOver;
    public float tempoDoFade = 2.0f; 

    [Header("Configuração")]
    public int maxLives = 100;
    private int currentLives;

    private void Start()
    {
        currentLives = maxLives;
        Time.timeScale = 1f; 

        if (painelGameOver != null)
        {
            painelGameOver.SetActive(false);
        }

        if (barraDeVida != null)
        {
            barraDeVida.maxValue = maxLives;
            barraDeVida.value = currentLives;
        }
    }

    public void OnLifeRestore()
    {
        if (currentLives < maxLives)
        {
            currentLives += 10;
            if (currentLives > maxLives) currentLives = maxLives;
            UpdateVisuals();
        }
    }

    public void OnHitTaken()
    {
        if (currentLives > 0)
        {
            currentLives -= 10;
            if (currentLives < 0) currentLives = 0;
            UpdateVisuals();

            if (currentLives <= 0)
            {
                
                StartCoroutine(AnimacaoGameOver());
            }
        }
    }

    void UpdateVisuals()
    {
        if (barraDeVida != null) barraDeVida.value = currentLives;
    }

    IEnumerator AnimacaoGameOver()
    {
        Debug.Log("Iniciando Fade de Game Over...");

        
        Timer timer = FindAnyObjectByType<Timer>();
        if (timer != null) timer.enabled = false;

        
        if (painelGameOver != null)
        {
            painelGameOver.SetActive(true);

           
            CanvasGroup cg = painelGameOver.GetComponent<CanvasGroup>();
            if (cg == null) cg = painelGameOver.AddComponent<CanvasGroup>();

            cg.alpha = 0; 

            
            float tempoPassado = 0f;

            while (tempoPassado < tempoDoFade)
            {
                
                tempoPassado += Time.unscaledDeltaTime;

                
                cg.alpha = tempoPassado / tempoDoFade;

                
                yield return null;
            }

            
            cg.alpha = 1;
        }

        // 4. SÓ AGORA Pausa o jogo e solta o mouse
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReiniciarFase()
    {
        // Importante: Voltar o tempo ao normal antes de recarregar
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}