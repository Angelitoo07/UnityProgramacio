using UnityEngine;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.UI;

public class ProvaQuadrat : MonoBehaviour
{
    public float velocitat = 2f;
    public float velocitatSalt = 7f;
    public int maxSalts = 2;
    public int salts = 0;
    public int vides = 3;
    public GameObject Guanyar;
    public GameObject Perdre;

    private Rigidbody2D rb;
    private int saltsRestants;
    private bool estaMuerto = false; // Nueva variable para detener el movimiento
    private bool hasGuanyat = false; // Para detener el movimiento al ganar

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        saltsRestants = maxSalts;
    }

    void Update()
    {
        if (!estaMuerto && !hasGuanyat) // Solo permite movimiento si no ha muerto o ganado
        {
            Mover();
            GestionarSalt();
        }
    }

    void Mover()
    {
        if (estaMuerto || hasGuanyat) return; // No permite moverse si ha ganado o muerto

        float inputHorizontal = Input.GetAxis("Horizontal");
        float velocitatHorizontal = inputHorizontal * velocitat;
        float velocitatVertical = rb.linearVelocity.y;

        rb.linearVelocity = new Vector2(velocitatHorizontal, velocitatVertical);
    }

    void GestionarSalt()
    {
        if (estaMuerto || hasGuanyat) return; // No permite saltar si ha ganado o muerto

        if (Input.GetKeyDown(KeyCode.Space) && salts < 2)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, velocitatSalt);
            saltsRestants--;
            salts++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            saltsRestants = maxSalts;
            salts = 0;
        }

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Enemy1"))
        {
            vides--;
            Debug.Log(vides);
            if (vides <= 0)
            {
                GameOver();
            }
        }

        if (collision.gameObject.CompareTag("Meta"))
        {
            HasGuanyat();
        }
    }

    void HasGuanyat()
    {
        Debug.Log("Has guanyat!!!");
        Guanyar.SetActive(true);
        hasGuanyat = true; // Bloquea los controles
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static; // Lo vuelve estático para evitar movimientos
    }

    void GameOver()
    {
        Debug.Log("Has perdut...");
        Perdre.SetActive(true);
        estaMuerto = true; // Bloquea los controles
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static; // Lo vuelve estático para evitar movimientos
    }
}