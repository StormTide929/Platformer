using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject player;
	private GameObject currentPlayer;
	private GameCamera cam;
	private bool diedWait;
	private int secondsToWaitAfteryDying;

	void Start () {
		cam = GetComponent<GameCamera>();
		SpawnPlayer(Vector3.zero);
		diedWait = true;
		secondsToWaitAfteryDying = 4;
	}
	
	// Spawn player
	private void SpawnPlayer(Vector3 spawnPos) {
		currentPlayer = Instantiate(player,spawnPos,Quaternion.identity) as GameObject;
		cam.SetTarget(currentPlayer.transform);
	}

	private void Update() {
		if (!currentPlayer) {
			if(diedWait)
				StartCoroutine(playerDied());
		}
	}

	IEnumerator playerDied(){
		diedWait = false;
		yield return new WaitForSeconds(secondsToWaitAfteryDying);
		//get array of all player ragdoll instances for destruction
		GameObject[] allPlayerRagdolls = GameObject.FindGameObjectsWithTag("PlayerRagdoll");
		//destroy them (player dead bodies)
	    for (int i=0; i<allPlayerRagdolls.Length; i++){
	    	Destroy(allPlayerRagdolls[i]);
	    }
		SpawnPlayer(Vector3.zero);
		diedWait=true;
	}

}
