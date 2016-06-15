using UnityEngine;
using System.Collections;

public class HandGrab : MonoBehaviour {

	Animator anim;
	int grabHash = Animator.StringToHash("Grab");


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1")) {
			anim.SetTrigger (grabHash);
		} else {
			anim.SetBool("Grab", false);
		}
	} 
}
