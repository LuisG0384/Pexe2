using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    TMPro.TMP_Text text;
    int count;
    private void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();
    }

    public void OnLifeRestore()
    {
        text.text = (++count).ToString();
    }

    public void OnHitTaken()
    {
        text.text = (--count).ToString();
    }

}
