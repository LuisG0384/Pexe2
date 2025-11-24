using UnityEngine;
using System.Collections;

public class SomAleatorio : MonoBehaviour
{
    [Header("Configurações")]
    public AudioSource audioSource;
    public AudioClip[] listaDeSons; 

    [Header("Tempo de Espera (Segundos)")]
    public float tempoMinimo = 5f; 
    public float tempoMaximo = 15f; 

    void Start()
    {
        
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        
        StartCoroutine(RotinaDeSom());
    }

    IEnumerator RotinaDeSom()
    {
        while (true) 
        {
            
            float tempoDeEspera = Random.Range(tempoMinimo, tempoMaximo);

            
            yield return new WaitForSeconds(tempoDeEspera);

            
            if (listaDeSons.Length > 0)
            {
               
                AudioClip somSorteado = listaDeSons[Random.Range(0, listaDeSons.Length)];

                audioSource.PlayOneShot(somSorteado);
            }
        }
    }
}