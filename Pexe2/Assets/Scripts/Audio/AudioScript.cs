using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public bool inimigoDetectado = false;
    private AudioSource som;

    float originalVolume;
    float fadeOutDuration = 0.5f;

    private void Start()
    {
        som = GetComponent<AudioSource>();
        originalVolume = som.volume;
    }
    private void Update()
    {
        if (inimigoDetectado && !som.isPlaying)
        {
            som.Play();
        }
        else if (!inimigoDetectado && som.isPlaying)
        {
            StartCoroutine(FadeOut());
        }
    }
    IEnumerator FadeOut()
    {
        float startVolume = som.volume;
        float timer = 0f;

        while (timer < fadeOutDuration)
        {
            timer += Time.deltaTime;
            som.volume = Mathf.Lerp(startVolume, 0f, timer / fadeOutDuration);
            yield return null;
        }
        som.volume = 0f;
        som.Stop();

        som.volume = originalVolume;
    }
}
