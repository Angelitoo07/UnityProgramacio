using UnityEngine;

public class Enemic1 : MonoBehaviour
{
    public Transform Player;
    public float velocitat = 2.0f; // Velocitat del joc
    public int dany = 1; // Dany que fa a el jugador
    private Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1.5f; // Per ajustar la velocitat a la que cau
    }


    void Update()
    {
        // L'enemic solament cau per gravetat, no necessita anar cao a el jugador
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Es para al tocar el terra
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerVides playerVides = other.GetComponent<PlayerVides>();
            if (playerVides != null)
            {
                playerVides.TakeDany(dany);
            }
        }
    }
}

public class PlayerVides : MonoBehaviour
{
    public int vides = 3; // Vides del jugador

    public void TakeDany(int dany)
    {
        vides -= dany;
        Debug.Log("Vida restant: " + vides);

        if (vides <= 0)
        {
            Debug.Log("¡El jugador a mort!");
            Destroy(gameObject);
        }
    }
}

