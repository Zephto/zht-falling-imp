using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 5.0f;
	private Rigidbody2D rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate() {

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

	private void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("collisione con: " + other.name);
	}
}