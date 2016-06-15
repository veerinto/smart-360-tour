using UnityEngine;
using System.Collections;

public class Smartphone : MonoBehaviour {

	public GameObject frontCam;
	public GameObject backCam;
	public GameObject sStick;

	public Transform ssHandlePt;
	public Transform ssPhonePt;

	FixedJoint ssJoint;

	bool isFrontCam = true;
	public bool isTakingPhoto = false;

	public bool isSsUsed = false;

	// Use this for initialization
	void Start () {
		frontCam.SetActive (true);
		backCam.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void switchCam()
	{
		if (isFrontCam) {
			frontCam.SetActive (false);
			backCam.SetActive (true);
			isFrontCam = false;
		}
		else
		{
			frontCam.SetActive (true);
			backCam.SetActive (false);
			isFrontCam = true;
		}
	}

	public void snapPhoto()
	{
		isTakingPhoto = true;
		frontCam.SetActive (false);
		backCam.SetActive (false);
		Invoke ("revertCam", 1.0f);
	}

	public void revertCam()
	{
		if (isFrontCam) {
			frontCam.SetActive (true);
			backCam.SetActive (false);
		}
		else
		{
			frontCam.SetActive (false);
			backCam.SetActive (true);
		}
		isTakingPhoto = false;
	}

	public void toggleSelfieStick()
	{
		if (!isSsUsed) {
			sStick.SetActive (true);
			ssHandlePt.position = transform.position;
//			ssHandlePt.rotation = transform.rotation;
			transform.position = ssPhonePt.position;
			transform.rotation = ssPhonePt.rotation;
			transform.localScale = new Vector3 (1.5f,1.5f,1.5f);
//			transform.parent = sStick.transform;
			if (!ssJoint) {
				ssJoint = gameObject.AddComponent<FixedJoint> ();
				ssJoint.connectedBody = ssPhonePt.GetComponent<Rigidbody> ();
			}
			isSsUsed = true;
		}
		else {
			sStick.SetActive (false);
//			ssHandlePt.position = transform.position;
			//			ssHandlePt.rotation = transform.rotation;
			transform.position = ssHandlePt.position;
			transform.rotation = ssHandlePt.rotation;
			transform.localScale = new Vector3 (1f,1f,1f);
			//			transform.parent = sStick.transform;
			if(ssJoint)
				Destroy(ssJoint);
//			ssJoint = gameObject.AddComponent<FixedJoint> ();
//			ssJoint.connectedBody = ssPhonePt.GetComponent<Rigidbody>();
			isSsUsed = false;
		}
	}
}
