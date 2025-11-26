using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPoint : MonoBehaviour
{
    TMPro.TMP_Text text;
    int count = 30;
    [SerializeField] GameObject plataforma;
    [SerializeField] ParticleSystem fedor;
    [SerializeField] StarWarsScroll textoBom;
    [SerializeField] StarWarsScroll textoRuim;
    private void Awake()
    {
        fedor.Stop();
        textoBom.velocidadeDeScroll = 0;
        textoRuim.velocidadeDeScroll = 0;
        text = GetComponent<TMPro.TMP_Text>();

        StartCoroutine(LoopTextUpdate());
    }

    private IEnumerator LoopTextUpdate()
    {
        int point = 0;
        for (int i = 0; i < count; i++)
        {
            point = UnityEngine.Random.Range(0, 100);
            text.text = point.ToString();

            yield return new WaitForSeconds(0.125f);

        }

        //point = PontosScript.pontuacao;
        point = 100;
        text.text = point.ToString();

        Destroy(plataforma);
        yield return new WaitForSeconds(0.12f);
        if (point < 10)
        {
            fedor.Play();
            textoRuim.velocidadeDeScroll = 70f;
        }
        else
        {
            {
                textoBom.velocidadeDeScroll = 70f;
            }
        }
    }

}
