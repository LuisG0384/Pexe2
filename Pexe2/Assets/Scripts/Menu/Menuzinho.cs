using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class Menuzinho: MonoBehaviour
{
    [SerializeField] private string nome_Level_Jogo;
    [SerializeField] private GameObject painel_Menu_Inicial;
    [SerializeField] private GameObject painel_Menu_Opcoes;

    public void Jogar()
    {
        SceneManager.LoadScene(nome_Level_Jogo);
    }

    public void Abrir_Opcoes()
    {
        painel_Menu_Inicial.SetActive(false);
        painel_Menu_Opcoes.SetActive(true);
    }

    public void Fechar_Opcoes()
    {
        painel_Menu_Opcoes.SetActive(false);
        painel_Menu_Inicial.SetActive(true);
    }

    public void Sair()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}
