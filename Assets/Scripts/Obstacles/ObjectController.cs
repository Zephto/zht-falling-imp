using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using MyBox;
using System;

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
	}

	#region Public variables
	public List<HellObjectReferences> objectsReferences;
	[SerializeField] private GameObject particles;
	[HideInInspector] public float speedGame = 0;
	#endregion

	#region Private variables
	private HellObjectReferences currentSelection;
	private Camera mainCamera;
	private float finalPosition;
	private Tweener moveTween;
	#endregion

	#region Consts
	private const float DefaultSpeed = 7f;
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
		Instantiate(particles, this.transform.position, Quaternion.identity, this.transform.parent);
		moveTween.Kill();
		this.gameObject.SetActive(false);
	}
	#endregion

	#region Private Methods
	private void SelectRandomType(){
		//En esta parte se puede jugar con las probabilidades, pero ya despues
		var randomIndex = new System.Random().Next(objectsReferences.Count);
		currentSelection = objectsReferences[randomIndex];
		currentSelection.hellObject.SetActive(true);
	}
	#endregion

	#region Public Methods
	
	#endregion
}
