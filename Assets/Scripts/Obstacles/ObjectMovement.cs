using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ObjectMovement : MonoBehaviour {


	public void Move(float finalPosition, float velocity){
		this.transform.DOMoveX(finalPosition, velocity).SetEase(Ease.InCubic);
	}
}
