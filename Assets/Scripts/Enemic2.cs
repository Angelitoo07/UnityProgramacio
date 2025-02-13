using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Transform Player; 
    public float velocitat = 2.0f; // Velocitat del joc
    public float stoppingDistance = 1.5f; // Distancia minima per parar-se
    public int dany = 1; // Dany que fa al jugador

    private void Update()
    {
        if (Player != null)
        {
           float distance = Vector2.Distance(transform.position, Player.position);

           if (distance > stoppingDistance)
            {
                // Mou al enemic a per al jugador
                transform.position = Vector2.MoveTowards(transform.position, Player.position, velocitat * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerVides2 playerVides = other.GetComponent<PlayerVides2>();
            if (playerVides != null)
            {
                playerVides.TakeDany(dany);
            }
        }
    }
}


public class PlayerVides2 : MonoBehaviour
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