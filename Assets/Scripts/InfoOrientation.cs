using UnityEngine;
using System.Collections;

public class InfoOrientation : MonoBehaviour {

    public Transform cameraPosition;

	// Use this for initialization
	void Awake () {
        transform.LookAt(cameraPosition);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
