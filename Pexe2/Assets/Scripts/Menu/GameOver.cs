using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    [SerializeField] private string nome_Level_Jogo;
    [SerializeField] private string nome_Level_Menu;


    public void Resetar()
    {
        SceneManager.LoadScene(nome_Level_Jogo);
    }

    public void Menu()
    {
        SceneManager.LoadScene(nome_Level_Menu);
    }

    
}
