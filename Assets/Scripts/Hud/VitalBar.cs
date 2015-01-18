using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VitalBar : MonoBehaviour {
	private PlayerCharacter _toon;
	private bool _isPlayerHealthBar;
	
	private int currentHealth;
	private int maxHealth;
	
	void Start () {
		_toon = GetComponent<PlayerCharacter>();
		currentHealth = _toon.GetVital(0).CurValue;
		maxHealth = _toon.GetVital(0).AdjustedBaseValue;
		Debug.Log(currentHealth + " / " + maxHealth);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void HandleHealth(){

	}

	//this method will calculate position of healthbar to show missing health
	public void ChangeHealthbarSize(int curHealth, int maxHealth){ 
	
	}

	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax){
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
	
}
