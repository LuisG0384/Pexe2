using UnityEngine;
using UnityEngine.UI;

public class DetectorPerigo : MonoBehaviour
{
    [Header("Configuração Visual")]
    public Image telaDePerigo;

    [Header("Ajuste Fino")]
    [Range(0f, 1f)] public float limiteMaximoAlpha = 0.5f; 
    [Range(1f, 5f)] public float velocidadeDoSumico = 3f; 

    [Header("Configuração de Distância")]
    public string tagDoInimigo = "Inimigue";
    public float distanciaParaComecar = 40f;

    void Update()
    {
        float menorDistancia = EncontrarInimigoMaisProximo();

        if (menorDistancia < distanciaParaComecar)
        {
            float intensidadeLinear = 1 - (menorDistancia / distanciaParaComecar);

            float intensidadeCurva = Mathf.Pow(intensidadeLinear, velocidadeDoSumico);

            float alphaFinal = intensidadeCurva * limiteMaximoAlpha;

            Color corAtual = telaDePerigo.color;
            corAtual.a = alphaFinal;
            telaDePerigo.color = corAtual;
        }
        else
        {
            Color corAtual = telaDePerigo.color;
            corAtual.a = 0f;
            telaDePerigo.color = corAtual;
        }
    }

    float EncontrarInimigoMaisProximo()
    {
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag(tagDoInimigo);
        float distanciaMaisCurta = Mathf.Infinity;
        Vector3 minhaPosicao = transform.position;

        foreach (GameObject inimigo in inimigos)
        {
            float dist = Vector3.Distance(minhaPosicao, inimigo.transform.position);
            if (dist < distanciaMaisCurta)
            {
                distanciaMaisCurta = dist;
            }
        }
        return distanciaMaisCurta;
    }
}