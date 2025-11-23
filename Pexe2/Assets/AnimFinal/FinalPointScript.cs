using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPoint : MonoBehaviour
{
    TMPro.TMP_Text text;
    int count = 30;
    [SerializeField] GameObject plataforma;
    private void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();

        StartCoroutine(LoopTextUpdate());
    }

    private IEnumerator LoopTextUpdate()
    {
        for (int i = 0; i < count; i++)
        {
            text.text = UnityEngine.Random.Range(0, 100).ToString();

            yield return new WaitForSeconds(0.125f);

        }

        Destroy(plataforma);
    }

}
