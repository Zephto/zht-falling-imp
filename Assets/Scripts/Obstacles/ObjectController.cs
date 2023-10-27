using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using MyBox;
using Unity.Mathematics;

[RequireComponent(typeof(BoxCollider2D))]
public class ObjectController : MonoBehaviour {

	public enum Type{
		OBSTACLE,
		COIN
	}

	#region Public variables
	public Type objectType;
	[SerializeField] private GameObject particles;
	#endregion

	#region Private variables
	private Camera mainCamera;
	private float finalPosition;
	private Tweener moveTween;
	#endregion

	private void Awake() {
		mainCamera = Camera.main;
        finalPosition   = mainCamera.ViewportToWorldPoint(Vector3.up).y;
	}

	private void OnEnable() {
		//Obtain upper border of the screen
		var errorRange = 5;

		moveTween = this.transform.DOMoveY(finalPosition + errorRange, 5f).SetEase(Ease.InCubic).OnComplete(()=>{
			this.gameObject.SetActive(false);
		});
	}

	private void OnTriggerEnter2D(Collider2D other) {
		Instantiate(particles, this.transform.position, quaternion.identity, this.transform.parent);
		moveTween.Kill();
		this.gameObject.SetActive(false);
	}

}
