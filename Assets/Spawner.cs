using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Spawner : NetworkBehaviour {

	public GameObject treePrefab;

	// Use this for initialization
	public override void OnStartLocalPlayer() {		
		
		GameObject tree = (GameObject)Instantiate(treePrefab, transform.position, transform.rotation);
		NetworkServer.Spawn(tree);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
