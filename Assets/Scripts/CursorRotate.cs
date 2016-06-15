using UnityEngine;
using System.Collections;

public class CursorRotate : MonoBehaviour {
    public Transform cameraTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.rotation = cameraTarget.rotation;
	}
}
