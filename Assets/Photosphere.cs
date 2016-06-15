using UnityEngine;
using System.Collections;

public class Photosphere : MonoBehaviour {
	
	public Transform headTransform;
	public bool isActivated = false;

	PhotosphereManager psManager;

	// Use this for initialization
	void Start () {
		psManager = GetComponentInParent<PhotosphereManager> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if (other.GetComponent<PhotosphereDetector>() && !isActivated)
		{			
			//			headTransform = other.transform.position;
			psManager.deactivatePhotospheres ();
//			Debug.Log ("return");
			transform.localScale = new Vector3 (30f, 30f, 30f);
			GetComponent<Grabbable> ().isGrabbable = false;
			gameObject.layer = 10;
			isActivated = true;
		}
	}

	public void deactivatePhotosphere()
	{
		if (isActivated) {
			setReturnPosition ();
		Debug.Log ("deactivated");
			transform.localScale = new Vector3 (0.15f, 0.15f, 0.15f);
////			headTransform = FindObjectOfType(SteamVR_TrackedObject.
//			transform.position = new Vector3 (headTransform.position.x, headTransform.position.y, headTransform.position.z + 0.7f);
			transform.position = psManager.returnPosition.position;
//			transform.localScale = psManager.returnPosition.localScale;
		

			Debug.Log (transform.position);
			Debug.Log ("actual");
			Debug.Log (transform.position);
			GetComponent<Grabbable> ().isGrabbable = true;
			gameObject.layer = 0;
			Invoke ("setDeactivated", 0.1f);
			//			isActivated = false;
			Debug.Log ("deactivated");

			psManager.returnPosSet = false;
		}
	}

	void setDeactivated()
	{
		isActivated = false;
	}

	public void setReturnPosition()
	{
//		if (psManager.returnPosition) {
			psManager.returnPosition.position = transform.position;
			psManager.returnPosition.localScale = transform.localScale;
			Debug.Log (psManager.returnPosition.position);
//		}
	}


//	public IEnumerator activatePhotosphere()	{
//		for (int i=0; i < 5; i++){
//			
//			yield return null;
//		}
//	}
}
