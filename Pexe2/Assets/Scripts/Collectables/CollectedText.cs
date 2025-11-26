using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedText : MonoBehaviour
{
    public TMPro.TMP_Text texto;
    int count;
    private void Awake()
    {
        texto = GetComponent<TMPro.TMP_Text>();
    }

    public void OnCollectedFoodText()
    {
        texto.text = (++count).ToString();
    }

    public void OnCollectedTrashText()
    {
        texto.text = (--count).ToString();
    }

}
