using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Hud references where can modify texts, screens and buttons
/// </summary>
public class GameplayHud : MonoBehaviour {
	
	#region Class references
	/// <inheritdoc cref="GameTextReferences">
	private GameTextReferences _textRefs;

	//TODO - hacer la contraparte de script de botones, similar a la de los textos
	// privarte GameButtonReferences _buttonRefs;

	//TODO - hacer otra contraparte para canvas, similar a la de los textos
	// private GameCanvasReferences _canvasRefs;
	#endregion

	#region Public variables
	#endregion

	#region Private variables
	#endregion

	#region Consts
	#endregion

	private void Awake() {
		_textRefs = this.GetComponent<GameTextReferences>();
	}

	#region Text Methods
	/// <summary>
	/// Set the score in the UI text element
	/// </summary>
	public void SetScore(int newScore){
		_textRefs.GetDynamicText("SCORE_TEXT").text = newScore.ToString();
	}

	/// <summary>
	/// Set the coins in the UI text element
	/// </summary>
	public void SetCoins(int newCoins){
		_textRefs.GetDynamicText("COINS_TEXT").text = newCoins.ToString();
	}
	#endregion
}
