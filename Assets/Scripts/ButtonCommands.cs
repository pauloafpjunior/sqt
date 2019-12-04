using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCommands : MonoBehaviour
{
    public GameObject[] paineis;

    public void mostrarPainelCreditos()
    {
        paineis[0].SetActive(false);
        paineis[1].SetActive(true);
    }

    public void mostrarPainelPrincipal()
    {
        paineis[0].SetActive(true);
        paineis[1].SetActive(false);
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        int idCena = PlayerPrefs.GetInt("IdTema");
        if (idCena != 0)
        {
            SceneManager.LoadScene(idCena.ToString());
        }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}
