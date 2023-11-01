using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
	private float spawnTime;
	private float currentTime;
	private bool isGameStarted;
	#endregion

	private void Awake() {
		_hud 		= GameObject.FindObjectOfType<GameplayHud>();
		_spawner 	= GameObject.FindObjectOfType<ObjectSpawner>();
	}

	private void Start() {
		isGameStarted = false;
		speed = 0f;
		maxSpeed = 1f;
		// speedIncrement = 0.005f;
		speedIncrement = 240f;
		currentTime = 10f;
		currentScore = 0f;
		spawnTime = 0f;

		_hud.SetScore((int)currentScore);
		StartCoroutine(CountdownStart());
	}

	private void Update() {
		if(!isGameStarted) return;
		
		if(currentTime < speedIncrement){
			speed = currentTime / speedIncrement;
			currentTime += Time.deltaTime;
		}else{
			speed = 1f;
		}

		Debug.Log("Speed: " + speed);

		currentScore += 0.07f * speed;
		_hud.SetScore(Mathf.FloorToInt(currentScore));
		
		float spawnRate = Mathf.Lerp(2f, 0.3f, speed);
		if(spawnTime <= 0){
			_spawner.SpawnObject(speed);
			spawnTime = spawnRate;
		}else{
			spawnTime -= Time.deltaTime;
		}
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
