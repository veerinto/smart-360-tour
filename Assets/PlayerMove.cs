using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{	
	public override void OnStartLocalPlayer() {		
//		GetComponentInChildren<MeshRenderer>().material.color = Color.red;
	}

	void Update()
	{
//		Debug.Log ("1");
		if (!isLocalPlayer)
			return;
		
//		Debug.Log ("2");
		var x = Input.GetAxis("Horizontal")*0.1f;
		var z = Input.GetAxis("Vertical")*0.1f;
		
		transform.Translate(x, 0, z);
	}
}