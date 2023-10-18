using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ObjectMovement : MonoBehaviour {

	private void Start() {
		//Obtain upper border of the screen
		var mainCamera = Camera.main;
        var worldPosition   = mainCamera.ViewportToWorldPoint(Vector3.up);
		var errorRange = 5;

		this.transform.DOMoveY(worldPosition.y + errorRange, 5f).SetEase(Ease.InCubic);
	}

	public void Move(float finalPosition, float velocity){
	}
}
