
using UnityEngine;
using System.Linq;
using System;
using MyBox;
using TMPro;

///<summary>
///Component used to localize all the text components inside the Screens object
///</summary>
public class GameTextReferences: MonoBehaviour {

	#region Public references
	///<summary>
	///Public class to reference the name of the component, TextMeshPro component and key localization
	///</summary>
	[Serializable]
	public class StaticTextReferences{
		public string nameReference;
		public TextMeshProUGUI textMeshPro;
		public string keyLocalization;
	}

	///<summary>
	///Public class to reference the dynamic texts
	///</summary>
	[Serializable]
	public class DynamicTextReferences{
		public string nameReference;
		public TextMeshProUGUI textMeshPro;
	}

	[Separator("Static texts")]
	///<summary>
	///Array to reference all the static texts
	///</summary>
	[Tooltip("Set different names in the Text component")]
	[SerializeField] private StaticTextReferences[] staticTexts;
	
	[Separator("Button texts")]
	///<summary>
	///Array to reference all the button texts
	///</summary>
	[Tooltip("Set different names in the Text component")]
	[SerializeField] private StaticTextReferences[] buttonTexts;

	[Separator("Dynamic texts")]
	///<summary>
	///Array to reference all the dynamic texts<br/>
	///IMPORTANT: Dynamic texts will not localize automatically, this will be
	///localize in the logic game script
	///</summary>
	[Tooltip("Set different names in the Text component")]
	[SerializeField] private DynamicTextReferences[] dynamicTexts;
	#endregion

	public void Start(){
		SetTexts();
	}

	#region Private methods
	///<inheritdoc cref="TextReferencesBase.SetTexts"/>
	public void SetTexts(){
		//Set static and button text
		foreach(var item in staticTexts.Union(buttonTexts).ToList()){
			item.textMeshPro.text = Localizator.GetValue(item.keyLocalization);
		}
	}
	#endregion

	#region Public methods
	///<summary>
	///Returns the TextMeshPro component referenced in the list<br/>
	///IMPORTANT: be careful to set the same name in different text components,
	///the function returns the first value that contains the string name value
	///</summary>
	///<param name="name">The name of the component to find in the list of text referenced</param>
	public TextMeshProUGUI GetDynamicText(string name){
		//If any object exist with that name
		if(!dynamicTexts.Any(item => item.nameReference == name)) return null;
		
		return dynamicTexts
			.Select(item => item)
			.FirstOrDefault(item => item.nameReference == name)
			.textMeshPro;
	}

	///<summary>
	///Sets the text value in a dynamic text using the localization dictionary<br/>
	///IMPORTANT: be careful to set the same name in different text components,
	///the function returns the first value that contains the string name value
	///</summary>
	///<param name="name">The name of the component to find in the list of text referenced</param>
	///<param name="key">Key value to set using the dictionary</param>
	public void SetTextWithKey(string name, string key){
		//If any object exist with that name
		if(!dynamicTexts.Any(item => item.nameReference == name)){
			Debug.LogWarning($"[Game Text References]: (Set text with key) There is no component for name {name}, please verify the names in the inspector.");
			return;
		}

		dynamicTexts
			.Select(item => item)
			.FirstOrDefault(item => item.nameReference == name)
			.textMeshPro.text = Localizator.GetValue(key);
	}
	#endregion
}