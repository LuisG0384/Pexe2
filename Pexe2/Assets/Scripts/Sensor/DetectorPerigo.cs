using UnityEngine;
using UnityEngine.UI;

public class DetectorPerigo : MonoBehaviour
{
    [Header("Configuração Visual")]
    public Image telaDePerigo;

    [Header("Ajuste Fino")]
    [Range(0f, 1f)] public float limiteMaximoAlpha = 0.5f; // Máximo de 50% visível
    [Range(1f, 5f)] public float velocidadeDoSumico = 3f; // <--- NOVA VARIÁVEL (Quanto maior, mais rápido some)

    [Header("Configuração de Distância")]
    public string tagDoInimigo = "Inimigue";
    public float distanciaParaComecar = 40f;

    void Update()
    {
        float menorDistancia = EncontrarInimigoMaisProximo();

        if (menorDistancia < distanciaParaComecar)
        {
            // 1. Intensidade bruta (Linear: 0 a 1)
            float intensidadeLinear = 1 - (menorDistancia / distanciaParaComecar);

            // 2. APLICANDO A CURVA (Aqui está a mágica)
            // Mathf.Pow eleva o número à potência.
            // Ex: Se a intensidade for 0.5 e a velocidade for 2 -> 0.5 * 0.5 = 0.25 (Fica muito mais fraco rápido)
            float intensidadeCurva = Mathf.Pow(intensidadeLinear, velocidadeDoSumico);

            // 3. Aplica o limite máximo
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