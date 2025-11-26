using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class FinalPoint : MonoBehaviour
{
    TMPro.TMP_Text text;
    int count = 30;

    [SerializeField] GameObject plataforma;
    [SerializeField] ParticleSystem fedor;

    [SerializeField] StarWarsScroll TextoRuim;
    [SerializeField] StarWarsScroll TextoBom;

    private void Awake()
    {
        if (fedor != null) fedor.Stop();
        text = GetComponent<TMPro.TMP_Text>();

        StartCoroutine(LoopTextUpdate());
    }

    private IEnumerator LoopTextUpdate()
    {
        int point = 0;

        for (int i = 0; i < count; i++)
        {
            point = UnityEngine.Random.Range(0, 100);
            if (text != null) text.text = point.ToString();
            yield return new WaitForSeconds(0.125f);
        }

        point = PontosScript.pontuacao;
        if (text != null) text.text = point.ToString();

        if (plataforma != null) Destroy(plataforma);

        yield return new WaitForSeconds(0.12f);

        if (point < 5)
        {
            if (fedor != null) fedor.Play();

            if (TextoRuim != null)
            {
                TextoRuim.gameObject.SetActive(true); 
                TextoRuim.Iniciar(70f);
            }
        }
        else
        {
            if (TextoBom != null)
            {
                TextoBom.gameObject.SetActive(true); 
                TextoBom.Iniciar(70f); 
            }
        }
    }
}