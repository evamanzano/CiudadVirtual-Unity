
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovimientoMegan : MonoBehaviour
{
    public float velocidad = 3f;
    public float fuerzaSalto = 1.5f;
    public float gravedad = -9.81f;

    private CharacterController controlador;
    private Animator animador;
    private float velocidadVertical;

    void Start()
    {
        controlador = GetComponent<CharacterController>();
        animador = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direccion = new Vector3(horizontal, 0, vertical);
        direccion = Vector3.ClampMagnitude(direccion, 1f);

        if (direccion.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direccion),
                10f * Time.deltaTime
            );
        }

        if (controlador.isGrounded && velocidadVertical < 0)
        {
            velocidadVertical = -2f;
        }

        if (controlador.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocidadVertical = Mathf.Sqrt(
                fuerzaSalto * -2f * gravedad
            );

            animador.SetTrigger("Jump");
        }

        velocidadVertical += gravedad * Time.deltaTime;

        Vector3 movimiento = direccion * velocidad;
        movimiento.y = velocidadVertical;

        controlador.Move(movimiento * Time.deltaTime);

        animador.SetFloat("Speed", direccion.magnitude);
    }
}
