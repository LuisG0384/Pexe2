using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StarWarsScroll : MonoBehaviour
{
    [Header("Configuração")]
    public float velocidadeDeScroll = 30f;
    [Range(0.1f, 2.0f)] public float fatorAceleracao = 0.3f;
    public float limiteSuperiorY = 1500f;

    [Header("Tela Preta")]
    public CanvasGroup painelPreto;
    public float velocidadeDoFade = 0.5f;
    public float tempoDeEsperaFinal = 3.0f;

    // Internas
    private RectTransform rectTransform;
    private float velocidadeAtual;
    private bool textoAcabou = false;
    private bool fadeAcabou = false;
    private float contadorEspera = 0f;

    // Trava para o texto não andar sozinho
    private bool podeMover = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // Garante que o painel preto comece invisível e ativado
        if (painelPreto != null)
        {
            painelPreto.alpha = 0f;
            painelPreto.gameObject.SetActive(true);
        }
    }

    public void Iniciar(float velocidade)
    {
        velocidadeAtual = velocidade; 
        podeMover = true;            
    }

    void Update()
    {
        if (!podeMover) return;

        if (!textoAcabou)
        {
            rectTransform.anchoredPosition += new Vector2(0, velocidadeAtual * Time.deltaTime);

            velocidadeAtual += (velocidadeAtual * fatorAceleracao) * Time.deltaTime;

            if (rectTransform.anchoredPosition.y > limiteSuperiorY)
            {
                textoAcabou = true;
            }
        }
        else if (!fadeAcabou)
        {
            if (painelPreto != null)
            {
                painelPreto.alpha += Time.deltaTime * velocidadeDoFade;
                if (painelPreto.alpha >= 1)
                {
                    painelPreto.alpha = 1;
                    fadeAcabou = true;
                }
            }
            else fadeAcabou = true;
        }
        else
        {
            contadorEspera += Time.deltaTime;
            if (contadorEspera >= tempoDeEsperaFinal)
            {
                SceneManager.LoadScene("MENU");
            }
        }
    }
}