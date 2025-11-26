using UnityEngine;
using TMPro; // Não se esqueça deste using se estiver usando TextMeshPro

public class StarWarsScroll : MonoBehaviour
{
    [Tooltip("A velocidade em que o texto irá se mover para cima.")]
    public float velocidadeDeScroll = 50f;

    [Tooltip("O ponto Y onde o texto deve parar de subir e desaparecer (fora da tela).")]
    public float limiteSuperiorY = 500f;

    // O componente de Transform do objeto onde este script está anexado (o texto em si)
    private RectTransform rectTransform;

    void Start()
    {
        // Pega o componente RectTransform, que controla a posição do UI no World Space
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Move o texto para cima (eixo Y local) a cada frame
        // Time.deltaTime garante que a velocidade seja independente da taxa de quadros (framerate)
        rectTransform.Translate(Vector3.up * velocidadeDeScroll * Time.deltaTime);

        // Se o texto atingir o limite superior, você pode reiniciá-lo ou destruí-lo.
        if (rectTransform.localPosition.y > limiteSuperiorY)
        {
            // Exemplo: Destrói o objeto de texto quando ele sai da tela
            Destroy(gameObject);

            // Aqui você pode carregar a próxima cena, se o texto for a introdução
            // SceneManager.LoadScene("ProximaCena");
        }
    }
}