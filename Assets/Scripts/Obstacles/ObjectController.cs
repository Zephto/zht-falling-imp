using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using MyBox;
using System;
using UnityEditor;

/// <summary>
/// Component used to control all the spawned object parameters
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class ObjectController : MonoBehaviour {

	public enum Type{ OBSTACLE, MONSTER, COIN, SPECIAL_COIN, ITEM }

	[Serializable]
	public class HellObjectReferences{
		public string hellId;
		public Type hellType;
		public GameObject hellObject;
		public ParticleSystem hellParticles;
		[Range(0, 100)] public int probability;
	}

	#region Public variables
	public List<HellObjectReferences> objectsReferences;
	[HideInInspector] public float speedGame = 0;
	#endregion

	#region Private variables
	private HellObjectReferences currentSelection;
	private Camera mainCamera;
	private float finalPosition;
	private Tweener moveTween;
	#endregion

	#region Consts
	private const float DefaultSpeed = 6f;
	private const float MonsterSpeed = 5f;
	private const float ErrorRange = 5f;
	#endregion

	private void Awake() {
		mainCamera = Camera.main;
        finalPosition   = mainCamera.ViewportToWorldPoint(Vector3.up).y;
	}

	#region On Methods
	private void OnEnable() {
		//Set random type
		SelectRandomType();

		var objectSpeed = (currentSelection.hellType == Type.MONSTER)? MonsterSpeed : DefaultSpeed;
		var toPos = finalPosition + ErrorRange;
		var duration = objectSpeed - (speedGame*2);
		moveTween = this.transform.DOMoveY(toPos, duration).OnComplete(()=>{
			this.gameObject.SetActive(false);
		}).SetEase(Ease.Linear);
	}

	private void OnDisable() {
		currentSelection.hellObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		moveTween.Kill();
		if(currentSelection.hellParticles != null){
			Instantiate(currentSelection.hellParticles, 
				this.transform.position, Quaternion.identity, 
				this.transform.parent);
		}

		this.gameObject.SetActive(false);
	}
	#endregion

	#region Private Methods
	private void SelectRandomType(){
		if(objectsReferences.Count <= 0){
			Debug.LogWarning("There is no elements in objects references");
			return;
		}
		
		//Turn off all hell objects
		objectsReferences.ForEach(obj => obj.hellObject.SetActive(false));

		GameObject RandomFinder(){
			//En esta parte se puede jugar con las probabilidades, pero ya despues
			var randomIndex = new System.Random().Next(objectsReferences.Count);
			currentSelection = objectsReferences[randomIndex];
			// currentSelection.hellObject.SetActive(true);

			if(UnityEngine.Random.Range(0, 100) <= currentSelection.probability){
				return currentSelection.hellObject;
			}else{
				return RandomFinder();
			}
		}

		RandomFinder().SetActive(true);
	}
	#endregion

	#region Public Methods
	
	#endregion
}
