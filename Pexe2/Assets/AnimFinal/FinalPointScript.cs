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
    private void Awake()
    {
        fedor.Stop();
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

        point = PontosScript.pontuacao;
        text.text = point.ToString();

        Destroy(plataforma);
        yield return new WaitForSeconds(0.12f);
        if(point < 10) fedor.Play();
    }

}
