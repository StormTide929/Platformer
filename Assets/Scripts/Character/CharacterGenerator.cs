using UnityEngine;
using System.Collections;
using System;  					//used for Enum class

public class CharacterGenerator : MonoBehaviour {

	private PlayerCharacter _toon;
	private const int STARTING_POINTS = 20;		
	private const int MIN_STARTING_ATTRIBUTE_VALUE = 1;		//attributes can't be set to lower than this
	private const int STARTING_VALUE = 7;  				//attributes start at this value
	private int pointsLeft;

	private const int OFFSET_HORIZONTAL = 30;	//screen padding horizontally
	private const int PADDING_HORIZONTAL = 10;  //space between stuff horizontally
	private const int PADDING = 10;             //space between stuff vertically
	private const int LINE_HEIGHT = 32; 		//margin between lines
	private const int STAT_LABEL_WIDTH = 128;
	private const int BASEVALUE_LABEL_WIDTH = 32;
	private const int BUTTON_WIDTH = 32;
	private const int BUTTON_HEIGHT = 32;

	private int statStartingPos = 80;

	public GUIStyle attributeValue_style;

	public GUISkin myskin;

	public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
		GameObject pc = Instantiate (playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		PlayerController pcScript = pc.GetComponent<PlayerController>();
		pcScript.gravity = 0;
		pc.name = "pc";
		//_toon = new PlayerCharacter();
		_toon = pc.GetComponent<PlayerCharacter>();
		_toon.Awake();

		pointsLeft = STARTING_POINTS;
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++){
			_toon.GetPrimaryAttribute(cnt).BaseValue = STARTING_VALUE;
		}
		_toon.StatUpdate();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		GUI.skin = myskin;
		DisplayName();
		DisplayPointsLeft();
		DisplayAttributes();
		GUI.skin = null;
		DisplayVitals();
		GUI.skin = myskin;
		DisplaySkills();
		DisplayCreateButton();
	}
	private void DisplayName(){
		GUI.Label(new Rect(30, 20, 50, 25), "Name: ");
		_toon.Name = GUI.TextField(new Rect(85, 20, 100, 25), _toon.Name);
	}
	private void DisplayAttributes(){
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++){
			GUI.Label (new Rect(OFFSET_HORIZONTAL, 								//Rect x
			                    statStartingPos + (cnt * LINE_HEIGHT) + (cnt * PADDING), 	//y
			                    STAT_LABEL_WIDTH, LINE_HEIGHT), 		//width
			           			((AttributeName)cnt).ToString()		//length 
			           			);								//assign style
			GUI.Label (new Rect(STAT_LABEL_WIDTH + OFFSET_HORIZONTAL, 
			                    statStartingPos + (cnt * LINE_HEIGHT) + (cnt * PADDING), 
			                    BASEVALUE_LABEL_WIDTH, 
			                    LINE_HEIGHT), 
			           			_toon.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString(), attributeValue_style);
			if(GUI.Button (new Rect(PADDING_HORIZONTAL + OFFSET_HORIZONTAL + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH, 
			                        statStartingPos + (cnt * LINE_HEIGHT) + (cnt * PADDING), 
			                        BUTTON_WIDTH, 
			                        BUTTON_HEIGHT), 
			               			"-")){
				if(_toon.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE){
					_toon.GetPrimaryAttribute(cnt).BaseValue--;
					pointsLeft++;
					_toon.StatUpdate();
				}
			}
			if(GUI.Button (new Rect(PADDING_HORIZONTAL + OFFSET_HORIZONTAL + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH + 3, 
			                        statStartingPos + (cnt * LINE_HEIGHT) + (cnt * PADDING), 
			                        BUTTON_WIDTH, 
			                        BUTTON_HEIGHT),
			               			"+")){
				if(pointsLeft > 0){
					_toon.GetPrimaryAttribute(cnt).BaseValue++;
					pointsLeft--;
					_toon.StatUpdate();
				}
			}
		}
	}
	private void DisplayVitals(){
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++){
			GUI.Label (new Rect(OFFSET_HORIZONTAL, 
			                    statStartingPos + ((cnt + 10) * LINE_HEIGHT), 
			                    STAT_LABEL_WIDTH, 
			                    LINE_HEIGHT), 
			           			((VitalName)cnt).ToString());
			GUI.Label (new Rect(OFFSET_HORIZONTAL + STAT_LABEL_WIDTH, 
			                    statStartingPos + ((cnt + 10) * LINE_HEIGHT),
			                    BASEVALUE_LABEL_WIDTH, 
			                    25), 
			           			_toon.GetVital(cnt).AdjustedBaseValue.ToString());
		}
	}
	private void DisplaySkills(){
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++){
			GUI.Label (new Rect(OFFSET_HORIZONTAL + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET_HORIZONTAL * 2, 
			                    statStartingPos + (cnt * LINE_HEIGHT), 
			                    STAT_LABEL_WIDTH, 
			                    LINE_HEIGHT), 
			           			((SkillName)cnt).ToString());
			GUI.Label (new Rect(OFFSET_HORIZONTAL + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET_HORIZONTAL * 2 + STAT_LABEL_WIDTH, 
			                    statStartingPos + (cnt * LINE_HEIGHT),
			                    BASEVALUE_LABEL_WIDTH, 
			                    LINE_HEIGHT)
			           			, _toon.GetSkill(cnt).AdjustedBaseValue.ToString());
		}
	}
	private void DisplayPointsLeft(){
		GUI.Label(new Rect(300, 
		                   20, 
		                   100, 
		                   25), 
		          			"Points Left: " + pointsLeft.ToString());
	}
	private void DisplayCreateButton(){
		if(GUI.Button (new Rect(Screen.width / 2 - 50, 
		                  	statStartingPos + (18 * LINE_HEIGHT), 
		                    100, 
		                    LINE_HEIGHT), "CREATE")){

			GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();
			gsScript.SaveCharacterData();
			Application.LoadLevel("Level_01");
		}
	}
}
