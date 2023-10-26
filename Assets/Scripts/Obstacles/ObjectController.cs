using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ObjectController : MonoBehaviour {

	public enum Type{
		OBSTACLE_FIRE,
		COLLECTABLE
	}

	#region Private variables
	private Camera mainCamera;
	private float finalPosition;
	#endregion

	private void Awake() {
		mainCamera = Camera.main;
        finalPosition   = mainCamera.ViewportToWorldPoint(Vector3.up).y;
	}

	private void OnEnable() {
		//Obtain upper border of the screen
		var errorRange = 5;

		this.transform.DOMoveY(finalPosition + errorRange, 5f).SetEase(Ease.InCubic).OnComplete(()=>{
			this.gameObject.SetActive(false);
		});
	}

}
