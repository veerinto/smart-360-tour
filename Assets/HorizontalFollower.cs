using UnityEngine;
using System.Collections;

public class HorizontalFollower : MonoBehaviour {

    public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion newRotation = Quaternion.LookRotation(target.position - transform.position);
        //newRotation.x = 0;
        newRotation.z = 0;
        newRotation.x = 0;
        //newRotation.x = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 2);
    }
}
