using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Defective.JSON;

//IMPORTANTE: REVISAR ESTE SCRIPT PARA HACERLO MAS GENERICO Y
//SEPARADO DE FIESTAAAA

///<summary>
///Script that find and sets the actual json to localize texts
///</summary>
public class Localizator: MonoBehaviour {
	///<summary>
	///Json reference of a common/general words
	///<summary>
	private static JSONObject fiestaJson;

	///<summary>
	///Json reference of the actual channel
	///</summary>
	private static JSONObject actualJson;

	#region Consts
	///<summary>
	///Fiesta path reference, where the i18n is located
	///</summary>
	private static JSONObject currentJson = new JSONObject();
	private const string FiestaPath = "00_Fiesta_Menu/i18n/ES"; //TODO: Check how to distinguish between languages
	private const string PathFormat = "00_Fiesta_Menu/i18n/{0}";
	#endregion

	void Awake() {
		//Exception to initialize this variable on Awake
		TextAsset textAsset = Resources.Load<TextAsset>(FiestaPath);
		fiestaJson = JSONObject.Create(textAsset.text.Replace("<br>", "\n"));
	}

	#region Methods
	///	<summary>
	///	Localize the key using the lang selected to translate the text in the TMPro component.
	/// </summary>
	public static void SetLangTraduction() {
		//Get file, obtain the langFile using player prefs
		var langFileName = string.Format(PathFormat, "ES" );//SavePrefs.GetLanguage()); // + ".json";
		var textAsset = Resources.Load < TextAsset > (langFileName) as TextAsset;
		if(textAsset!=null){
			currentJson = JSONObject.Create(textAsset.text);
		}else{
			Debug.LogError("Es nulo");
		}

	}

	///<summary>
	///Sets the actual json of the channel
	///</summary>
	///<param name="path">Path where the json is located</param>
	public static void SetActualJson(string path){
		var currentFile = Resources.Load<TextAsset>(path);

		if(currentFile==null){
			Debug.LogWarning("[Localizator]: (SetActualJson) Current path '" + path + "' not found.");
			return;
		}
		TextAsset textAsset = Resources.Load<TextAsset>(path);
		actualJson = JSONObject.Create(textAsset.text.Replace("<br>", "\n"));
	}

	///<summary>
	///Return the traduction value of a key
	///</summary>
	///<param name="key">Key of the json value</param>
	public static string GetValue(string key){
		if(fiestaJson.ToDictionary().ContainsKey(key))
			return fiestaJson[key].stringValue;


		if(actualJson.ToDictionary().ContainsKey(key))
			return actualJson[key].stringValue;

		return "ERROR_KEY_NOT_FOUND";
	}

	///<summary>
	///Return a random traduction value from a list with the same key value.
	///</summary>
	///<param name="key">Key of the json value</param>
	public static string GetRandomValueStartsWith(string key){
		var actualDictionary = actualJson.ToDictionary();

		var values = (
			from line in actualDictionary.ToList()
			where line.Key.StartsWith(key)
			select line.Value
		).ToList();

		return values[ Random.Range(0, values.Count) ];
	}

	///<summary>
	///Return a list of the values with the same key value.
	///</summary>
	///<param name="key">Key of the json value</param>
	public static List<string> GetListStartsWith(string key){
		var actualDictionary = actualJson.ToDictionary();

		var values = (
			from line in actualDictionary.ToList()
			where line.Key.StartsWith(key)
			select line.Value
		).ToList();

		return values;
	}
	#endregion
}