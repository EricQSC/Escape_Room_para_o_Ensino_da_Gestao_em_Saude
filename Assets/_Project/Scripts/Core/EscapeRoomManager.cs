using UnityEngine;

public class EscapeRoomManager : MonoBehaviour
{
    public static EscapeRoomManager Instance { get; private set; }

    [Header("Variáveis da Etapa 1 (Receção)")]
    public bool rececao = false;
    public bool telefone = false;
    public bool livro = false;

    [Header("Variáveis da Etapa 2 (Administração)")]
    public bool cadeado = false;
    public int progressoPistasEtapa2 = 0; // 0: Lock -> 1: Ruivo1 -> 2: Loiro1 -> 3: Loiro -> 4: ManPatternShirt

    [Header("Referências de Portas e Objetos")]
    public Animator portaRecepcaoAnimator;
    public Animator lockAnimator;
    public Renderer lockRenderer;
    public Animator portaAdministracao1Animator;
    public Animator portaAdministracao2Animator;

    [Header("Sons Globais")]
    public AudioSource audioSource;
    public AudioClip somCadeado;
    public AudioClip somPortaAntiga;
    public AudioClip somWinner;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void TentarAbrirPortaRecepcao()
    {
        if (rececao && telefone && livro)
        {
            UIManager.Instance.MostrarPainelInfo("Podes passar à etapa seguinte", "Parabéns", () =>
            {
                if (portaRecepcaoAnimator != null)
                    portaRecepcaoAnimator.SetTrigger("Open");

                UIManager.Instance.MostrarPainelInfo(
                    "ETAPA 2: ORGANIZAR PARA CUIDAR",
                    "Foram reportadas falhas na continuidade dos cuidados nesta enfermaria.\n\nA Direção de Enfermagem precisa de perceber como o trabalho está organizado.\n\nObservem o serviço e recolham os testemunhos.\n\nNo final, terão de identificar o método de trabalho utilizado."
                );
            });
        }
        else
        {
            UIManager.Instance.MostrarPainelInfo("Procura o código", "Sem o código não consegues passar ao nível seguinte");
        }
    }

    public void DesbloquearAdministracao()
    {
        cadeado = true;

        if (audioSource != null && somCadeado != null)
            audioSource.PlayOneShot(somCadeado);

        if (lockAnimator != null)
            lockAnimator.SetTrigger("Open");

        if (lockRenderer != null)
            lockRenderer.material.color = new Color(1f, 0.78f, 0.18f); // #FFC72E

        UIManager.Instance.MostrarPainelInfo(
            "📋 RELATÓRIO DE AUDITORIA",
            "A organização dos cuidados está centrada na distribuição de tarefas entre os profissionais. Foram identificados sinais de fragmentação dos cuidados e dificuldade em reconhecer um enfermeiro responsável pelo acompanhamento global do doente. A Direção de Enfermagem terá agora de decidir como reorganizar os cuidados.\nPassem ao gabinete da Enfermeira Diretora",
            () =>
            {
                if (audioSource != null && somPortaAntiga != null)
                    audioSource.PlayOneShot(somPortaAntiga);

                if (portaAdministracao1Animator != null)
                    portaAdministracao1Animator.SetTrigger("Open");

                if (portaAdministracao2Animator != null)
                    portaAdministracao2Animator.SetTrigger("Open");
            }
        );
    }
}
