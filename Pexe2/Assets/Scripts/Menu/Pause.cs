using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; 

public class Pause : MonoBehaviour
{
    [Header("UI")]
    public GameObject painelPause;
    public static bool jogoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (jogoPausado) RetomarJogo();
            else Pausar();
        }
    }

    public void RetomarJogo()
    {
        painelPause.SetActive(false);
        Time.timeScale = 1f;
        jogoPausado = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

       
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Pausar()
    {
        painelPause.SetActive(true);
        Time.timeScale = 0f;
        jogoPausado = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CarregarMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MENU"); 
    }

    public void SairDoJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}