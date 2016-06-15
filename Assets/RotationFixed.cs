using UnityEngine;
using System.Collections;

public class RotationFixed : MonoBehaviour {

	Vector3 rotInit;
	Vector3 posInit;

	// Use this for initialization
	void Start () {
		rotInit = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 forceRot = new Vector3 (rotInit.x, rotInit.y, rotInit.z);
		transform.rotation = Quaternion.Euler (forceRot);
//		transform.position = posInit;
	}


	void Update () {
//		posInit = transform.position;
	}
}
