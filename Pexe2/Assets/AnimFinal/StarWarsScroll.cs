using UnityEngine;
using TMPro; // Não se esqueça deste using se estiver usando TextMeshPro

public class StarWarsScroll : MonoBehaviour
{
    public float velocidadeDeScroll = 50f;

    public float limiteSuperiorY = 500f;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.Translate(Vector3.up * velocidadeDeScroll * Time.deltaTime);

        if (rectTransform.localPosition.y > limiteSuperiorY)
        {
            Destroy(gameObject);
        }
    }
}