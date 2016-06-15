using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TransformProxy : NetworkBehaviour {

	public GameObject vivePlayer;

	Transform srcHMD;
	public GameObject proxyHMD;

	Transform srcLHand;
	public GameObject proxyLHand;

	Transform srcRHand;
	public GameObject proxyRHand;

	GameObject hmdObject;
	GameObject LHandObject;
	GameObject RHandObject;

//	public NetworkManager netMgr;
//
//	public GameObject headModel;
//	public GameObject LHandModel;
//	public GameObject RHandModel;

	SteamVR_TrackedObject[] trackedDevices;

	bool proxiesSpawned = false;
//
//	void Start()
//	{
//		CmdSpawn();
//	}

	public override void OnStartLocalPlayer() {
		trackedDevices = FindObjectsOfType<SteamVR_TrackedObject> ();

		int handCounter = 0;

		foreach (SteamVR_TrackedObject trackedDevice in trackedDevices) {
			if (trackedDevice.index == SteamVR_TrackedObject.EIndex.Hmd) {
				srcHMD = trackedDevice.transform;
			} else if (handCounter == 0) {
				srcLHand = trackedDevice.transform;
				handCounter++;
			} else if (handCounter == 1) {
				srcRHand = trackedDevice.transform;
				handCounter++;
			}
		}

		hmdObject = (GameObject)Instantiate (proxyHMD, Vector3.zero, Quaternion.identity);
		LHandObject = (GameObject)Instantiate (proxyLHand, Vector3.zero, Quaternion.identity);
		RHandObject = (GameObject)Instantiate (proxyRHand, Vector3.zero, Quaternion.identity);

		bool spawned = NetworkServer.SpawnWithClientAuthority (hmdObject, this.gameObject); 
		NetworkServer.SpawnWithClientAuthority (LHandObject, this.gameObject); 
		NetworkServer.SpawnWithClientAuthority (RHandObject, this.gameObject); 
		Debug.Log ("hmd spawned " + spawned);
//		CmdSpawn ();
		Debug.Log ("start local");

//		hmdObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
//		LHandObject.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
//		RHandObject.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (isLocalPlayer);
		if (!isLocalPlayer)
			return;

//		if (!proxiesSpawned) {
////			CmdSpawn ();
//		}
//		else
//		{
			hmdObject.transform.position = srcHMD.position;
			hmdObject.transform.rotation = srcHMD.rotation;
			LHandObject.transform.position = srcLHand.position;
			LHandObject.transform.rotation = srcLHand.rotation;
			RHandObject.transform.position = srcRHand.position;
			RHandObject.transform.rotation = srcRHand.rotation;
//		}
	}

	[Command]
	void CmdSpawn() {
		Debug.Log ("HELLO");
		hmdObject = (GameObject)Instantiate (proxyHMD, Vector3.zero, Quaternion.identity);
		LHandObject = (GameObject)Instantiate (proxyLHand, Vector3.zero, Quaternion.identity);
		RHandObject = (GameObject)Instantiate (proxyRHand, Vector3.zero, Quaternion.identity);

		bool spawned = NetworkServer.SpawnWithClientAuthority (hmdObject, this.gameObject); 
		NetworkServer.SpawnWithClientAuthority (LHandObject, this.gameObject); 
		NetworkServer.SpawnWithClientAuthority (RHandObject, this.gameObject); 
		Debug.Log ("hmd spawned " + spawned);

		proxiesSpawned = true;
	}
}