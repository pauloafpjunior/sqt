using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    private const int MAX_ESTRELAS = 3;

    public Text NomeTemaTxt;
    public Image Pergunta;
    public Text NumPerguntaTxt;
    public Text NotaFinalTxt;
    public GameObject BarraTempo;
    public Color normalColor, warningColor, dangerColor;
    public Button[] BtnAlternativas;
    public Sprite[] Perguntas;
    public string[] Respostas;
    private int PerguntaAtual, QtdeAcertos;
    public GameObject[] Estrelas;
    private float NotaPorPergunta;
    private const float MAX_NOTA = 10;

    private int NotaFinal, IdTema;

    public AudioClip sfxAcertou, sfxErrou, sfxTerminou;
    private AudioSource sfxPlayer;

    public bool JogarComTempo;
    public float TempoParaResponder;
    private float PercentTempo, TempoAux;
    public bool ultimoTema;

    [Header("Configurar paineis")]
    public GameObject[] paineis;


    void Start()
    {
        sfxPlayer = GetComponent<AudioSource>();
        IdTema = PlayerPrefs.GetInt("IdTema");
        NotaFinal = PlayerPrefs.GetInt("NotaFinal_" + IdTema);
        atualizarPaineis(true, false);
        criarListaPerguntas();
        atualizarNumPerguntaTxt();
        BarraTempo.SetActive(JogarComTempo);
        atualizarBarraTempo();
    }

    private void atualizarPaineis(bool painelJogo, bool painelResultado)
    {
        if (painelJogo != painelResultado)
        {
            paineis[0].SetActive(painelJogo);
            paineis[1].SetActive(painelResultado);
        }
    }

    public int CalculaEstrelas()
    {
        foreach (GameObject item in Estrelas)
        {
            item.SetActive(false);
        }

        int NumEstrelas = Mathf.RoundToInt(NotaFinal / MAX_ESTRELAS);

        for (int i = 0; i < NumEstrelas; i++)
        {
            Estrelas[i].SetActive(true);
        }

        return NumEstrelas;
    }

    public void sfxPlay(AudioClip audio)
    {
        sfxPlayer.PlayOneShot(audio, 0.2f);
    }

    public void atualizarBarraTempo()
    {
        if (JogarComTempo && TempoAux <= TempoParaResponder)
        {
            PercentTempo = ((TempoAux - TempoParaResponder) / TempoParaResponder) * -1;

            if (PercentTempo > 0.6)
            {
                BarraTempo.GetComponent<Image>().color = normalColor;
            }
            else if (PercentTempo > 0.3)
            {
                BarraTempo.GetComponent<Image>().color = warningColor;
            }
            else
            {
                BarraTempo.GetComponent<Image>().color = dangerColor;
            }

            BarraTempo.transform.localScale = new Vector3(PercentTempo, 1, 1);

        }
        else if (JogarComTempo && TempoAux > TempoParaResponder)
        {
            sfxPlay(sfxErrou);
            proximaPergunta();
        }

    }


    public void criarListaPerguntas()
    {

        PerguntaAtual = 0;
        QtdeAcertos = 0;
        NotaPorPergunta = MAX_NOTA / Perguntas.Length;

        string nomeTema = PlayerPrefs.GetString("NomeTema");
        if (nomeTema != null)
        {
            NomeTemaTxt.text = nomeTema;
        }

        Pergunta.sprite = Perguntas[PerguntaAtual];
    }

    private void atualizarNumPerguntaTxt()
    {
        NumPerguntaTxt.text = (PerguntaAtual + 1).ToString() + "/" + Perguntas.Length.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (JogarComTempo)
        {
            TempoAux += Time.deltaTime;
            atualizarBarraTempo();
        }
    }

    public void responder(string alternativa)
    {
        if (PerguntaAtual < Perguntas.Length)
        {
            if (Respostas[PerguntaAtual] == alternativa)
            {
                QtdeAcertos += 1;
                sfxPlay(sfxAcertou);
            }
            else
            {
                sfxPlay(sfxErrou);
            }

            proximaPergunta();
        }
    }

    public void proximaPergunta()
    {
        TempoAux = 0f;
        PerguntaAtual += 1;
        if (PerguntaAtual < Perguntas.Length)
        {
            Pergunta.sprite = Perguntas[PerguntaAtual];
            atualizarNumPerguntaTxt();
        }
        else
        {
            terminou();
        }
    }

    public void terminou()
    {
        sfxPlay(sfxTerminou);
        int notaFinalAux = Mathf.RoundToInt(QtdeAcertos * NotaPorPergunta);

        if (notaFinalAux > NotaFinal)
        {
            NotaFinal = notaFinalAux;
        }

        PlayerPrefs.SetInt("NotaFinal_" + IdTema.ToString(), NotaFinal);


        NotaFinalTxt.text = NotaFinal.ToString();
        int NumEstrelas = CalculaEstrelas();

        if (ultimoTema && NumEstrelas == MAX_ESTRELAS)
        {
            PlayerPrefs.SetInt("Certificado", 1);
        }
        atualizarPaineis(false, true); // jogo terminou
    }
}
