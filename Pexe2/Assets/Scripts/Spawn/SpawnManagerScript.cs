using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManagerScript : MonoBehaviour
{
    public int count = 0;
    public bool pode = true;

    private void Update()
    {
        if (count < 100) pode = true;
        else pode = false;
        Debug.Log(count);
    }

    public void Diminui()
    {
        count--;
    }

    public void Aumenta()
    {
        count++;
    }
}
