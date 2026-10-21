using UnityEngine;

public class ObjetoInterativoSimples : MonoBehaviour
{
    public enum AcaoEspecial { Nenhuma, PortaRecepcao, CadeadoBloqueado }

    [Header("Ação Especial (Opcional)")]
    public AcaoEspecial acaoEspecial = AcaoEspecial.Nenhuma;

    [Header("Fala / Balão de Diálogo (Opcional)")]
    public bool temFala;
    [TextArea(2, 3)] public string textoFala;
    public float duracaoFala = 4f;

    [Header("Animação (Opcional)")]
    public Animator animatorAlvo;
    public string nomeTriggerAnimacao;

    [Header("Som (Opcional - ex: medical monitor1)")]
    public AudioSource audioSource;
    public AudioClip somParaTocar;

    [Header("Mudar Cor (Flower pot1 e Lamp floor)")]
    public bool mudarCorAoClicar;
    public Renderer objetoParaPintar;
    public Color novaCor = Color.yellow;

    [Header("Sequência de Pistas da Etapa 2 (0 = Não faz parte)")]
    [Tooltip("1 = Lock, 2 = enfermeiro riuvo1, 3 = enfermeiro loiro1, 4 = enfermeiro loiro")]
    public int passoSequenciaEtapa2 = 0;

    private void OnMouseDown()
    {
        if (acaoEspecial == AcaoEspecial.PortaRecepcao)
        {
            EscapeRoomManager.Instance.TentarAbrirPortaRecepcao();
            return;
        }

        if (passoSequenciaEtapa2 > 0)
        {
            if (EscapeRoomManager.Instance.progressoPistasEtapa2 == passoSequenciaEtapa2 - 1)
                EscapeRoomManager.Instance.progressoPistasEtapa2 = passoSequenciaEtapa2;
            else if (EscapeRoomManager.Instance.progressoPistasEtapa2 < passoSequenciaEtapa2 - 1)
                return;
        }

        if (acaoEspecial == AcaoEspecial.CadeadoBloqueado && !EscapeRoomManager.Instance.cadeado)
        {
            UIManager.Instance.MostrarPainelInfo(
                "🔒 RELATÓRIO BLOQUEADO",
                "Ainda não existem dados suficientes para concluir a auditoria. Recolham os testemunhos da enfermaria."
            );
        }

        if (temFala)
            UIManager.Instance.MostrarBalaoFala(transform, textoFala, duracaoFala);

        if (animatorAlvo != null && !string.IsNullOrEmpty(nomeTriggerAnimacao))
            animatorAlvo.SetTrigger(nomeTriggerAnimacao);

        if (audioSource != null && somParaTocar != null)
            audioSource.PlayOneShot(somParaTocar);

        if (mudarCorAoClicar && objetoParaPintar != null)
            objetoParaPintar.material.color = novaCor;
    }
}
