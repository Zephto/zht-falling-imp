using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script controller of the game
/// </summary>
public class GameController : MonoBehaviour {
	
	#region Class references
	private GameplayHud _hud;
	private ObjectSpawner _spawner;
	#endregion

	#region Private variables
	private float speed;
	private float maxSpeed;
	private float speedIncrement;
	private int currentScore;
	private bool isGameStarted;
	#endregion

	private void Awake() {
		_hud = GameObject.FindObjectOfType<GameplayHud>();
	}

	private void Start() {
		isGameStarted = false;
		speed = 0f;
		maxSpeed = 1f;
		speedIncrement = 1.0f;
		currentScore = 0;

		_hud.SetScore(currentScore);
		StartCoroutine(CountdownStart());
	}

	private void Update() {
		if(!isGameStarted) return;

		if(speed < maxSpeed){
			speed += speedIncrement * Mathf.Pow(2, Time.time);

			currentScore = Mathf.FloorToInt(speed);
			_hud.SetScore(currentScore);
		}
	}

	#region Coroutines
	private IEnumerator CountdownStart(){
		// Establece la duración de la cuenta regresiva.
        float countdownDuration = 3.0f;
        
        while (countdownDuration > 0)
        {
            yield return new WaitForSeconds(1.0f); // Espera 1 segundo.
            countdownDuration -= 1.0f;
        }

        isGameStarted = true;
		yield return null;
	}
	#endregion
}
