using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	public float speed = 2;
	public int distance = 5;
	private bool direction = true;
	private float distanceTravelled = 0; 
	private Vector3 lastPosition;

	void Start(){
		getLastPosition(); 
	}

	void Update () {
		distanceTravelled += Vector3.Distance(transform.position, lastPosition); 
		getLastPosition(); 
		if(Mathf.Abs(distanceTravelled) > distance){
			direction = !direction;
			distanceTravelled = 0;
			getLastPosition();
		}
		if(direction){
			transform.Translate(Vector3.right * speed * Time.deltaTime);
		}
		else if(!direction){
			transform.Translate(Vector3.left * speed * Time.deltaTime);
		}
	}
	void getLastPosition(){
		lastPosition = transform.position; 	
	}
}
