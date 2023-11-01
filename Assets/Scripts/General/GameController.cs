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
	/// <inheritdoc cref="GameplayHud"/>
	private GameplayHud _hud;
	/// <inheritdoc cref="ObjectSpawner"/>
	private ObjectSpawner _spawner;
	#endregion

	#region Private variables
	private float speed;
	private float maxSpeed;
	private float speedIncrement;
	private float currentScore;
	private float spawnTime;
	private float currentGameTime;
	private bool isGameStarted;
	#endregion

	private void Awake() {
		_hud 		= GameObject.FindObjectOfType<GameplayHud>();
		_spawner 	= GameObject.FindObjectOfType<ObjectSpawner>();
	}

	private void Start() {
		isGameStarted = false;
		currentGameTime = 10f; //Starts in 10 as a offset of the speed
		speedIncrement = 240f;
		currentScore = 0f;
		spawnTime = 0f;
		maxSpeed = 1f;
		speed = 0f;

		_hud.SetScore((int)currentScore);
		StartCoroutine(CountdownStart());
	}

	private void Update() {
		if(!isGameStarted) return;
		
		//Speed calculations that will be use in all the game
		if(currentGameTime < speedIncrement){
			speed = currentGameTime / speedIncrement;
			currentGameTime += Time.deltaTime;
		}else{
			speed = maxSpeed;
		}

		//Make the score calculations
		currentScore += 0.07f * speed;
		_hud.SetScore(Mathf.FloorToInt(currentScore));
		
		//Spawn new objects using the rate and speed calculations
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
        float countdownDuration = 2.0f;
        
        while (countdownDuration > 0) {
            yield return new WaitForSeconds(1.0f);
            countdownDuration -= 1.0f;
        }

        isGameStarted = true;
		yield return null;
	}
	#endregion
}
