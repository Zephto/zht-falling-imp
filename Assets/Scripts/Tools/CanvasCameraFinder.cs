using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

///<summary>
///Component used to set a camera in case the render mode in Canvas is Screen Space - Camera
///</summary>
[RequireComponent(typeof(Canvas))]
[ExecuteInEditMode]
public class CanvasCameraFinder: MonoBehaviour {	
	
	#region Private variables
	///<summary>
	///Reference of the canvas
	///</summary>
	private Canvas canvas;
	#endregion
	
	void Awake() {
		//Get and set camera
		this.GetComponent<Canvas>().worldCamera = Camera.main;

		// ONLY EDITOR, This is to display the elements on the main camera in the MinigameTest scene. 
		#if UNITY_EDITOR
			if (!EditorApplication.isPlaying)
			this.GetComponent<Canvas>().worldCamera = Camera.main;
        #endif
	}
}