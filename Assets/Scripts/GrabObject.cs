using UnityEngine;
using System.Collections;

public class GrabObject : MonoBehaviour
{
	public Rigidbody attachPoint;
	public GameObject handModel;

	bool usedGravity = false;

	SteamVR_TrackedObject trackedObj;
	FixedJoint joint;

	public PhotosphereManager psManager;

	void Awake()
	{
		trackedObj = GetComponentInParent<SteamVR_TrackedObject>();
	}

	void FixedUpdate()
	{
		var device = SteamVR_Controller.Input((int)trackedObj.index);	

		if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
		{
			releaseObject ();
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.attachedRigidbody)
		{			
			grabObject (other.gameObject);
		}
		if (other.GetComponent<PhotosphereDetector> ()) {
			releaseObject ();
		}
	}
	 
	void OnTriggerExit(Collider other)
	{
	}

	void grabObject(GameObject objectToGrab) {
		var device = SteamVR_Controller.Input((int)trackedObj.index);

		Grabbable grabbedObject = objectToGrab.GetComponent<Grabbable>();
		if (grabbedObject)
		{
			if (grabbedObject.isGrabbable) {
				if (joint == null && device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {

					attachObject (objectToGrab);
					grabbedObject.GrabberIndex = (int)trackedObj.index;				
				} 
				if (!joint) {
					device.TriggerHapticPulse (800);
				}

				if (grabbedObject.GetComponent<Smartphone> ()) {
					trackSmartphone (grabbedObject.GetComponent<Smartphone> ());
				}
			}
		}
	}

	public bool attachObject(GameObject objectToAttach)
	{
		if (joint == null) {

			usedGravity = objectToAttach.GetComponent<Rigidbody> ().useGravity;
			objectToAttach.GetComponent<Rigidbody> ().useGravity = false;

			joint = objectToAttach.AddComponent<FixedJoint> ();
			attachPoint.gameObject.SetActive (true);
			joint.connectedBody = attachPoint;

			if (handModel)
				handModel.SetActive (false);
			else
				GetComponent<Renderer> ().enabled = false;
			
////			if (!psManager.returnPosSet) {
//				if (objectToAttach.GetComponent<Photosphere> ()) {
//					objectToAttach.GetComponent<Photosphere> ().setReturnPosition ();
//					Debug.Log ("set");
//				}
//				psManager.returnPosSet = true;
//			}

			return true;
		} else {
			return false;
		}
	}

	void releaseObject() {
		var device = SteamVR_Controller.Input((int)trackedObj.index);

		if (joint) {
			var go = joint.gameObject;
			var rigidbody = go.GetComponent<Rigidbody> ();
//			Object.DestroyImmediate (joint);
			Destroy (joint);
			joint = null;

			if (handModel)
				handModel.SetActive(true);
			else
				GetComponent<Renderer> ().enabled = true;


			var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
//			if (origin != null) {
//				rigidbody.velocity = origin.TransformVector (device.velocity);
//				rigidbody.angularVelocity = origin.TransformVector (device.angularVelocity);
//			} else {
//				rigidbody.velocity = device.velocity;
//				rigidbody.angularVelocity = device.angularVelocity;
//			}

			rigidbody.useGravity = usedGravity;
			rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
		}		

		psManager.returnPosSet = false;
	}

	public bool isHoldingSomething()
	{
		return (joint != null);
	}

	public IEnumerator triggerFeedback(ushort hapticForce)	{
		var device = SteamVR_Controller.Input((int)trackedObj.index);	

		for (int i=0; i < 3; i++){
			device.TriggerHapticPulse (hapticForce);
			yield return null;
		}
	}

	public void trackSmartphone(Smartphone sp)
	{
		var device = SteamVR_Controller.Input((int)trackedObj.index);

		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.ApplicationMenu)) {
			sp.switchCam ();
			StartCoroutine (triggerFeedback (1000));
		} 

		if (device.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)
			&& !sp.isTakingPhoto) {
			sp.snapPhoto ();
			StartCoroutine (triggerFeedback (1000));
		} 
//
//		if (device.GetPressDown (SteamVR_Controller.ButtonMask.Grip)) {
//			sp.toggleSelfieStick ();
//			releaseObject ();
//			attachObject (sp.sStick);
//		} 

	}
}
