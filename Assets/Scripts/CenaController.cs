using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CenaController : MonoBehaviour
{
    public Text NomeTemaTxt;
    public Button BtnPlay;
    public Button BtnCertificado;

    // Start is called before the first frame update
    void Start()
    {
        BtnPlay.interactable = false;
        int cert = PlayerPrefs.GetInt("Certificado");
        BtnCertificado.interactable = (cert == 1);
    }

    public void Jogar()
    {
        int idCena = PlayerPrefs.GetInt("IdTema");
        if (idCena != 0) {
            SceneManager.LoadScene(idCena.ToString());
        }
    }
}
