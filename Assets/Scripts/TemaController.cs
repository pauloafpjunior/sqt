using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class TemaController : MonoBehaviour
{
    private CenaController CenaController;
    private Button BtnTema;
    private const int MAX_ESTRELAS = 3;

    [Header("Configuração do Tema")]
    public int IdTema;
    public string NomeTema;

    [Header("Configuração das Estrelas")]
    public bool estaBloqueado;
    private int NotaFinal;

    [Header("Configuração do Botão")]
    public Text IdTemaTxt;
    public GameObject[] Estrelas;

    void Start()
    {
        CenaController = FindObjectOfType(typeof(CenaController)) as CenaController;
        BtnTema = GetComponent<Button>();

        IdTemaTxt.text = IdTema.ToString();

        // Limpa número de estrelas obtidas
        foreach (GameObject item in Estrelas)
        {
            item.SetActive(false);
        }

        NotaFinal = PlayerPrefs.GetInt("NotaFinal_" + IdTema);

        CalculaEstrelas();
        VerificarNotaMinima();
    }

    public void VerificarNotaMinima()
    {
        if (estaBloqueado)
        {
            int notaTemaAnterior = PlayerPrefs.GetInt("NotaFinal_" + (IdTema - 1).ToString());
            int QtdeEstrelasTemaAnterior = Mathf.RoundToInt(notaTemaAnterior / MAX_ESTRELAS);
            BtnTema.interactable = (QtdeEstrelasTemaAnterior == MAX_ESTRELAS);
        }
    }

    public void SelecionarTema()
    {
        CenaController.NomeTemaTxt.text = NomeTema;

        PlayerPrefs.SetInt("IdTema", IdTema);
        PlayerPrefs.SetString("NomeTema", NomeTema);

        CenaController.BtnPlay.interactable = true;
    }

    // Nota         |  Número de Estrelas
    // -------------|--------------------
    // 0, 1 ou 2    |  0
    // 3, 4 ou 5    |  1
    // 6, 7 ou 8    |  2
    // 9 ou 10      |  3
    public void CalculaEstrelas()
    {
        int NumEstrelas = Mathf.RoundToInt(NotaFinal / MAX_ESTRELAS);

        for (int i = 0; i < NumEstrelas; i++)
        {
            Estrelas[i].SetActive(true);
        }
    }

}
