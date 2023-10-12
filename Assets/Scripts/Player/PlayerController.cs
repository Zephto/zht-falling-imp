using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // // Verifica si el juego se está ejecutando en un dispositivo móvil
        // if (Application.isMobilePlatform)
        // {
        //     // Mueve al jugador usando el acelerómetro
        //     Vector3 acceleration = Input.acceleration;
        //     Vector3 movement = new Vector3(acceleration.x, 0, 0);
        //     rb.AddForce(movement * moveSpeed);
        // }
        // else
        // {
        //     // Mueve al jugador usando las flechas del teclado en el editor
        //     float moveHorizontal = Input.GetAxis("Horizontal");
        //     Vector3 movement = new Vector3(moveHorizontal, 0, 0);
        //     rb.AddForce(movement * moveSpeed);
        // }

        float moveHorizontal = 0.0f;

        // Verifica si el juego se está ejecutando en un dispositivo móvil
        if (Application.isMobilePlatform)
        {
            // Mueve al jugador usando el acelerómetro
            moveHorizontal = Input.acceleration.x;
        }
        else
        {
            // Mueve al jugador usando las flechas del teclado en el editor
            moveHorizontal = Input.GetAxis("Horizontal");
        }

        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb.velocity = movement * moveSpeed;
    }
}