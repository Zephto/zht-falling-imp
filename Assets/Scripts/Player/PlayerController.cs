using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 5.0f;
	private Rigidbody2D rb;
	private float minX, maxX;

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	private void Start() {
		//Obtain screen to world limits
		var minScreenPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);
		var maxScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

		//Save screen limits
		minX = minScreenPoint.x;
		maxX = maxScreenPoint.x;
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

		var currentPlayerPosition = this.transform.position;
		currentPlayerPosition.x = Mathf.Clamp(currentPlayerPosition.x, minX, maxX);
		this.transform.position = currentPlayerPosition;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("collisione con: " + other.name);
	}
}