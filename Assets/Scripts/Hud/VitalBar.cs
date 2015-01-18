using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VitalBar : MonoBehaviour {
	private PlayerCharacter _toon;
	private bool _isPlayerHealthBar;

	public RectTransform healthTransform;
	private float cachedY;
	private float minXValue;
	private float maxXValue;
	private int currentHealth;
	private int maxHealth;
	public Text healthText;
	public GUITexture visualHealth;

	// Use this for initialization
	void Start () {
		_toon = GetComponent<PlayerCharacter>();
		cachedY = healthTransform.position.y;
		maxXValue = healthTransform.position.x;
		minXValue = healthTransform.position.x - healthTransform.rect.width;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void HandleHealth(){
		healthText.text = "health: " + currentHealth;
		float currentXValue = MapValues(currentHealth, 0, maxHealth, minXValue, maxXValue);
		healthTransform.position = new Vector3(currentXValue, cachedY);
	}

	//this method will calculate position of healthbar to show missing health
	public void ChangeHealthbarSize(int curHealth, int maxHealth){ 
	
	}

	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax){
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
	
}
