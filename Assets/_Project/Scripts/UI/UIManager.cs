using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Painel de Informação (Título + Texto + Botão OK)")]
    public GameObject painelInfoObj;
    public TMP_Text infoTituloText;
    public TMP_Text infoCorpoText;
    public Button infoBotaoFechar;

    [Header("Painel de Quiz (Pergunta + 3 Botões)")]
    public GameObject painelQuizObj;
    public TMP_Text quizPerguntaText;
    public Button[] botoesOpcoes; // Array de 3 botões
    public TMP_Text[] textosOpcoes; // Textos dentro dos 3 botões

    [Header("Balão de Fala Temporário (Enfermeiros/Pacientes)")]
    public GameObject painelFalaObj;
    public TMP_Text falaText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void MostrarPainelInfo(string titulo, string texto, Action aoFechar = null)
    {
        if (painelInfoObj == null)
        {
            Debug.Log($"[INFO] {titulo}: {texto}");
            aoFechar?.Invoke();
            return;
        }

        painelInfoObj.SetActive(true);
        infoTituloText.text = titulo;
        infoCorpoText.text = texto;

        infoBotaoFechar.onClick.RemoveAllListeners();
        infoBotaoFechar.onClick.AddListener(() =>
        {
            painelInfoObj.SetActive(false);
            aoFechar?.Invoke();
        });
    }

    public void MostrarPainelQuiz(string pergunta, string[] opcoes, Action<int> aoEscolherOpcao)
    {
        if (painelQuizObj == null) return;

        painelQuizObj.SetActive(true);
        quizPerguntaText.text = pergunta;

        for (int i = 0; i < botoesOpcoes.Length; i++)
        {
            int indiceOpcao = i + 1; // 1, 2 ou 3
            if (i < opcoes.Length)
            {
                botoesOpcoes[i].gameObject.SetActive(true);
                textosOpcoes[i].text = opcoes[i];
                botoesOpcoes[i].onClick.RemoveAllListeners();
                botoesOpcoes[i].onClick.AddListener(() =>
                {
                    painelQuizObj.SetActive(false);
                    aoEscolherOpcao?.Invoke(indiceOpcao);
                });
            }
        }
    }

    public void MostrarBalaoFala(Transform alvo, string texto, float duracao)
    {
        if (painelFalaObj == null)
        {
            Debug.Log($"[FALA {alvo.name}]: {texto}");
            return;
        }

        StopAllCoroutines();
        StartCoroutine(RotinaFala(texto, duracao));
    }

    private System.Collections.IEnumerator RotinaFala(string texto, float duracao)
    {
        painelFalaObj.SetActive(true);
        falaText.text = texto;
        yield return new WaitForSeconds(duracao);
        painelFalaObj.SetActive(false);
    }
}
