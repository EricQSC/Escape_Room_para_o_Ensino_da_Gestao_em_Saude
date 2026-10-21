using UnityEngine;

[RequireComponent(typeof(QuizTrigger))]
public class ConfiguradorAutomaticoQuizzes : MonoBehaviour
{
    public enum PresetDesafio
    {
        Etapa1_Rececao_Teorias,
        Etapa1_Livro_GestaoEmAccao,
        Etapa1_Telefone_Conselho,
        Etapa2_Doente_Auditoria,
        Etapa3_EnfermeiraDiretora_Final
    }

    public PresetDesafio escolherDesafio;

    // Botão automático que preenche os dados no Editor do Unity assim que escolhes a opção!
    private void OnValidate()
    {
        QuizTrigger trigger = GetComponent<QuizTrigger>();
        if (trigger == null) return;

        switch (escolherDesafio)
        {
            case PresetDesafio.Etapa1_Rececao_Teorias:
                trigger.recompensaAoConcluir = QuizTrigger.TipoRecompensa.Rececao;
                trigger.tituloSucesso = "Acesso autorizado";
                trigger.textoSucesso = "Parabéns. Uma das verificações foi concluída!";
                trigger.perguntas = new PerguntaQuiz[]
                {
                    CriarPergunta(
                        "Qual das seguintes características está associada à Teoria Clássica de Fayol?",
                        "Motivação, comunicação e relações interpessoais",
                        "Hierarquia e centralização das decisões",
                        "Adaptação ao contexto", 2, "Resposta incorreta.", "Volta a tentar."),
                    CriarPergunta(
                        "Que teoria centra a eficiência na tarefa do trabalhador, numa perspetiva micro?",
                        "Teoria Clássica",
                        "Administração Científica",
                        "Teoria dos Sistemas", 2, "Resposta incorreta.", "Volta a tentar."),
                    CriarPergunta(
                        "Que teoria dá ênfase ao ambiente e entende a organização como um sistema aberto?",
                        "Teoria Clássica",
                        "Teoria Estruturalista",
                        "Teoria dos Sistemas", 3, "Resposta incorreta.", "Volta a tentar."),
                    CriarPergunta(
                        "Segundo a Teoria das Relações Humanas, que fatores influenciam particularmente o desempenho dos profissionais?",
                        "Motivação, comunicação e relações interpessoais",
                        "Hierarquia e centralização das decisões",
                        "Especialização das tarefas", 1, "Resposta incorreta.", "Volta a tentar."),
                    CriarPergunta(
                        "Que teoria procura máxima produção a mínimo custo através do estudo de tempos e movimentos?",
                        "Teoria Clássica",
                        "Administração Científica",
                        "Teoria Estruturalista", 2, "Resposta incorreta.", "Volta a tentar.")
                };
                break;

            case PresetDesafio.Etapa1_Livro_GestaoEmAccao:
                trigger.recompensaAoConcluir = QuizTrigger.TipoRecompensa.Livro;
                trigger.tituloSucesso = "Parabéns";
                trigger.textoSucesso = "Foi desbloqueado mais um item...";
                trigger.perguntas = new PerguntaQuiz[]
                {
                    CriarPergunta(
                        "Perante serviços com realidades distintas, qual é a decisão de gestão mais fundamentada?",
                        "Aplicar princípios, fundamentar decisões",
                        "Aplicar uma estrutura única",
                        "Adaptar a gestão ao contexto", 3, "Errado", "Tenta Novamente"),
                    CriarPergunta(
                        "Uma Direção analisa como uma mudança num serviço afeta os restantes. Que princípio está a aplicar?",
                        "Interdependência dos sistemas",
                        "Especialização das tarefas",
                        "Centralização da autoridade", 1, "Errado", "Tenta Novamente"),
                    CriarPergunta(
                        "Numa organização, qual é o foco da gestão segundo Fayol?",
                        "Eficiência de cada tarefa",
                        "Estrutura e funções da organização",
                        "Relações informais da equipa", 2, "Errado", "Tenta Novamente")
                };
                break;

            case PresetDesafio.Etapa1_Telefone_Conselho:
                trigger.recompensaAoConcluir = QuizTrigger.TipoRecompensa.Telefone;
                trigger.tituloSucesso = "Parabéns";
                trigger.textoSucesso = "Conseguiu outro código de acesso";
                trigger.perguntas = new PerguntaQuiz[]
                {
                    CriarPergunta(
                        "Que método de trabalho mais reflete os princípios de divisão e especialização das tarefas?",
                        "Método individual",
                        "Método funcional",
                        "Enfermeiro de referência", 2, "Errado", "Tenta novamente"),
                    CriarPergunta(
                        "Que princípio de gestão está mais presente num método de trabalho centrado na execução de tarefas?",
                        "Especialização do trabalho",
                        "Adaptação ao contexto",
                        "Participação nas decisões", 1, "Errado", "Tenta novamente"),
                    CriarPergunta(
                        "Que característica distingue o gestor de caso do enfermeiro de referência?",
                        "Planeia e avalia os cuidados",
                        "É responsável pelo cliente",
                        "Acompanha o cliente em diferentes contextos de saúde", 3, "Errado", "Tenta novamente")
                };
                break;

            case PresetDesafio.Etapa2_Doente_Auditoria:
                trigger.recompensaAoConcluir = QuizTrigger.TipoRecompensa.CadeadoAdministracao;
                trigger.perguntas = new PerguntaQuiz[]
                {
                    CriarPergunta(
                        "Com base nos testemunhos recolhidos, que método de trabalho está implementado nesta enfermaria?",
                        "Método individual",
                        "Método funcional",
                        "Enfermeiro de referência", 2,
                        "🔒 Relatório bloqueado.",
                        "A conclusão não é consistente com os dados recolhidos.\nRevejam os testemunhos e tentem novamente.")
                };
                break;

            case PresetDesafio.Etapa3_EnfermeiraDiretora_Final:
                trigger.recompensaAoConcluir = QuizTrigger.TipoRecompensa.MissaoFinalDiretora;
                trigger.tituloSucesso = "🏆 MISSÃO CONCLUÍDA";
                trigger.textoSucesso = "A reorganização dos cuidados foi aprovada pela Direção de Enfermagem. Ao longo desta missão, analisaram princípios de gestão, identificaram a organização do trabalho, reconheceram os seus efeitos e fundamentaram decisões para melhorar a continuidade dos cuidados. Gerir em enfermagem é transformar conhecimento em decisões que melhoram os cuidados. Parabéns. Missão cumprida!";

                PerguntaQuiz p1 = CriarPergunta(
                    "Que método favorece a responsabilização de um enfermeiro pelos cuidados globais de um grupo de doentes?",
                    "Método funcional",
                    "Enfermeiro de referência",
                    "Gestor de caso", 2, "Reavaliem a proposta.", "Não posso aprovar");
                p1.mostrarPainelIntermedio = true;
                p1.tituloIntermedio = " Proposta em análise";
                p1.textoIntermedio = "A proposta melhora a responsabilização durante o internamento. Mas tenho outro problema para resolver.\nUm doente com necessidades complexas prepara-se para ter alta. Necessitará de acompanhamento entre hospital, cuidados continuados, comunidade e família.\n\nA continuidade dos cuidados não pode terminar à porta do hospital.";

                PerguntaQuiz p2 = CriarPergunta(
                    "Que modelo responde melhor à necessidade de coordenação entre diferentes contextos de cuidados?",
                    "Método funcional",
                    "Enfermeiro de referência",
                    "Gestor de caso", 3, "Revejam a decisão", "Tente novamente.");

                PerguntaQuiz p3 = CriarPergunta(
                    "O que distingue, neste caso, o gestor de caso?",
                    "Executa todas as intervenções",
                    "Articula o percurso entre diferentes contextos",
                    "Distribui tarefas pela equipa", 2, "Revejam a decisão", "Tente novamente.");

                trigger.perguntas = new PerguntaQuiz[] { p1, p2, p3 };
                break;
        }
    }

    private PerguntaQuiz CriarPergunta(string q, string op1, string op2, string op3, int correta, string titErro, string txtErro)
    {
        return new PerguntaQuiz
        {
            questao = q,
            opcoes = new string[] { op1, op2, op3 },
            respostaCorreta = correta,
            tituloErro = titErro,
            textoErro = txtErro
        };
    }
}
