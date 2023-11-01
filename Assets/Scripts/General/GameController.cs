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
	private float currentScore;
	private bool isGameStarted;
	#endregion

	private void Awake() {
		_hud = GameObject.FindObjectOfType<GameplayHud>();
	}

	private void Start() {
		isGameStarted = false;
		speed = 0f;
		maxSpeed = 1f;
		speedIncrement = 1f;
		currentScore = 0f;

		_hud.SetScore((int)currentScore);
		StartCoroutine(CountdownStart());
	}

	private void Update() {
		if(!isGameStarted) return;

		if(speed < maxSpeed){
			speed += speedIncrement * Time.deltaTime;
		}else{
			speed = maxSpeed;
		}

		currentScore += 0.1f * speed + Time.deltaTime;
		_hud.SetScore(Mathf.FloorToInt(currentScore));
	}

	#region Coroutines
	private IEnumerator CountdownStart(){
		// Establece la duración de la cuenta regresiva.
        float countdownDuration = 3.0f;
        
        while (countdownDuration > 0)
        {
			Debug.Log("time: "+countdownDuration);
            yield return new WaitForSeconds(1.0f); // Espera 1 segundo.
            countdownDuration -= 1.0f;
        }

        isGameStarted = true;
		yield return null;
	}
	#endregion
}
