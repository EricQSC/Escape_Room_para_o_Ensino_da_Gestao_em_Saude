using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movimento (WASD)")]
    public float velocidade = 5f;
    public float gravidade = -9.81f;

    [Header("Câmara (Olhar com Botão Direito do Rato)")]
    public Transform camaraTransform;
    public float sensibilidadeRato = 2f;

    private CharacterController controller;
    private float rotacaoX = 0f;
    private Vector3 velocidadeVertical;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // 1. Andar com WASD / Setas
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direcao = transform.right * x + transform.forward * z;
        controller.Move(direcao * velocidade * Time.deltaTime);

        // Gravidade simples para manter no chão
        if (controller.isGrounded && velocidadeVertical.y < 0)
            velocidadeVertical.y = -2f;

        velocidadeVertical.y += gravidade * Time.deltaTime;
        controller.Move(velocidadeVertical * Time.deltaTime);

        // 2. Rodar a câmara mantendo o Botão Direito do Rato premido
        // (Assim o cursor fica sempre livre para clicar nos objetos e responder aos Quizzes, tal como na Delightex!)
        if (Input.GetMouseButton(1) && camaraTransform != null)
        {
            float ratoX = Input.GetAxis("Mouse X") * sensibilidadeRato;
            float ratoY = Input.GetAxis("Mouse Y") * sensibilidadeRato;

            rotacaoX -= ratoY;
            rotacaoX = Mathf.Clamp(rotacaoX, -80f, 80f);

            camaraTransform.localRotation = Quaternion.Euler(rotacaoX, 0f, 0f);
            transform.Rotate(Vector3.up * ratoX);
        }
    }
}
