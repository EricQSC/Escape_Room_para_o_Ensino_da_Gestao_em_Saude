using System;
using UnityEngine;

[Serializable]
public class PerguntaQuiz
{
    [TextArea(2, 4)] public string questao;
    public string[] opcoes = new string[3];
    [Range(1, 3)] public int respostaCorreta = 1;

    [Header("Feedback se acertar (Opcional a meio do quiz)")]
    public bool mostrarPainelIntermedio;
    public string tituloIntermedio;
    [TextArea(2, 3)] public string textoIntermedio;

    [Header("Feedback se errar")]
    public string tituloErro = "Errado";
    [TextArea(2, 3)] public string textoErro = "Tenta novamente.";
}

public class QuizTrigger : MonoBehaviour
{
    public enum TipoRecompensa { Livro, Telefone, Rececao, CadeadoAdministracao, MissaoFinalDiretora }

    [Header("Configuração do Desafio")]
    public TipoRecompensa recompensaAoConcluir;
    public PerguntaQuiz[] perguntas;

    [Header("Painel de Sucesso Final")]
    public string tituloSucesso = "Parabéns";
    [TextArea(2, 4)] public string textoSucesso = "Conseguiu desbloquear esta etapa!";

    private void OnMouseDown()
    {
        if (recompensaAoConcluir == TipoRecompensa.CadeadoAdministracao &&
            EscapeRoomManager.Instance.progressoPistasEtapa2 < 4)
        {
            return;
        }

        IniciarQuiz(0);
    }

    private void IniciarQuiz(int indicePergunta)
    {
        if (indicePergunta >= perguntas.Length)
        {
            ConcluirSequenciaQuiz();
            return;
        }

        PerguntaQuiz p = perguntas[indicePergunta];

        UIManager.Instance.MostrarPainelQuiz(p.questao, p.opcoes, (opcaoEscolhida) =>
        {
            if (opcaoEscolhida == p.respostaCorreta)
            {
                if (p.mostrarPainelIntermedio)
                {
                    UIManager.Instance.MostrarPainelInfo(p.tituloIntermedio, p.textoIntermedio, () =>
                    {
                        IniciarQuiz(indicePergunta + 1);
                    });
                }
                else
                {
                    IniciarQuiz(indicePergunta + 1);
                }
            }
            else
            {
                UIManager.Instance.MostrarPainelInfo(p.tituloErro, p.textoErro);
            }
        });
    }

    private void ConcluirSequenciaQuiz()
    {
        switch (recompensaAoConcluir)
        {
            case TipoRecompensa.Livro:
                EscapeRoomManager.Instance.livro = true;
                UIManager.Instance.MostrarPainelInfo(tituloSucesso, textoSucesso);
                break;

            case TipoRecompensa.Telefone:
                EscapeRoomManager.Instance.telefone = true;
                UIManager.Instance.MostrarPainelInfo(tituloSucesso, textoSucesso);
                break;

            case TipoRecompensa.Rececao:
                EscapeRoomManager.Instance.rececao = true;
                UIManager.Instance.MostrarPainelInfo(tituloSucesso, textoSucesso);
                break;

            case TipoRecompensa.CadeadoAdministracao:
                EscapeRoomManager.Instance.DesbloquearAdministracao();
                break;

            case TipoRecompensa.MissaoFinalDiretora:
                UIManager.Instance.MostrarPainelInfo(
                    "É esta a resposta que procurávamos",
                    "Decisão aprovada. O gestor de caso permite coordenar o percurso da pessoa entre diferentes contextos, promovendo continuidade e articulação dos cuidados.",
                    () =>
                    {
                        if (EscapeRoomManager.Instance.audioSource && EscapeRoomManager.Instance.somWinner)
                            EscapeRoomManager.Instance.audioSource.PlayOneShot(EscapeRoomManager.Instance.somWinner);

                        UIManager.Instance.MostrarPainelInfo(tituloSucesso, textoSucesso);
                    }
                );
                break;
        }
    }
}
