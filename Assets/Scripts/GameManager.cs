using UnityEngine;
using System.Collections;

public class Creature{
	public int hitPoints_current;
	public int hitPoints_max;
	public int mana_current;
	public int mana_max;
	public int mana_regen;
	public int runAcceleration;
	public int runMaxSpeed; 
	public int jumpHeight;
	public int level;
	public int experience_current;
	public int experience_max;
}

public class GameManager : MonoBehaviour {
	
	public GameObject player;
	private GameObject currentPlayer;
	private GameCamera cam;
	private bool diedWait;
	private int secondsToWaitAfteryDying;

	public Creature playerStats = new Creature();

	void Awake() {
        DontDestroyOnLoad(transform.gameObject);
        playerStats.hitPoints_current = 10;
        playerStats.hitPoints_max = 10;
        playerStats.mana_current = 10;
        playerStats.mana_max = 40;
        playerStats.mana_regen = 3;
        playerStats.runAcceleration = 28;
        playerStats.runMaxSpeed = 12;
        playerStats.jumpHeight = 14;
        playerStats.level = 1;
        playerStats.experience_current = 0;
        playerStats.experience_max = 300;
    }

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
		GameObject[] allPlayerBloodSplashes = GameObject.FindGameObjectsWithTag("PlayerBloodSplash");
		//destroy them (player dead bodies)
	    for (int i=0; i < allPlayerRagdolls.Length; i++){
	    	Destroy(allPlayerRagdolls[i]);
	    }
	    for (int i=0; i < allPlayerBloodSplashes.Length; i++){
	    	Destroy(allPlayerBloodSplashes[i]);
	    }
		SpawnPlayer(Vector3.zero);
		diedWait=true;
	}

}
