using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	
	public GameObject player;
	public GameCamera mainCamera;
	public GameObject gameSettings;

	private GameObject _pc;
	private PlayerCharacter _pcScript;
	private bool diedWait;
	private int secondsToWaitAfteryDying;

	void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

	void Start () {
		SpawnPlayer(Vector3.zero);
		LoadCharacter();
		diedWait = true;
		secondsToWaitAfteryDying = 4;
	}

	private void Update() {
		if (!_pc) {
			if(diedWait)
				StartCoroutine(playerDied());
		}
	}

	// Spawn player
	private void SpawnPlayer(Vector3 spawnPos) {
		_pc = Instantiate(player,spawnPos,Quaternion.identity) as GameObject;
		_pc.name = "pc";

		_pcScript = _pc.GetComponent<PlayerCharacter>();

		mainCamera.SetTarget(_pc.transform);
	}

	public void LoadCharacter(){
		GameObject gs = GameObject.Find("__GameSettings");
		if(gs == null){
			GameObject gs1 = Instantiate(gameSettings, Vector3.zero, Quaternion.identity) as GameObject;
			gs1.name = "__GameSettings";
		}
		GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();
		gsScript.LoadCharacterData();
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
