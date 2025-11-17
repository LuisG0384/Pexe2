using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedText : MonoBehaviour
{
    TMPro.TMP_Text text;
    int count;
    private void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();
    }

    public void OnCollectedFoodText()
    {
        text.text = (++count).ToString();
    }

    public void OnCollectedTrashText()
    {
        text.text = (--count).ToString();
    }

}
