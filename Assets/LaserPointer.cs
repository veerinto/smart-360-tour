using UnityEngine;
using System.Collections;

public class LaserPointer : MonoBehaviour
{
	public LineRenderer line;


	SteamVR_TrackedObject trackedObj;

	Vector3 targetPos;

	void Awake()
	{
		trackedObj = GetComponentInParent<SteamVR_TrackedObject>();
		line.enabled = false;
	}

	void Start ()
	{
		Transform eyeCamera = GameObject.FindObjectOfType<SteamVR_Camera>().GetComponent<Transform>();
		// The referece point for the camera is two levels up from the SteamVR_Camera
	}

	// Update is called once per frame
	void Update ()
	{

		var device = SteamVR_Controller.Input((int)trackedObj.index);

//		if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
			showMarker ();
//		}
	}

	void showMarker()
	{
		// Teleport
//		float refY = reference.position.y;

//		Plane plane = new Plane(Vector3.up, -refY);
		Ray ray = new Ray(this.transform.position, transform.forward);

		float dist = 50f;

		var device = SteamVR_Controller.Input((int)trackedObj.index);	
//		device.TriggerHapticPulse (800);
		targetPos = ray.origin + ray.direction * dist;

		line.enabled = true;
		line.SetPosition(0, transform.position);
		line.SetPosition(1, ray.origin + ray.direction * dist);
	}


//	void blink()
//	{		
//		if (hasGroundTarget)
//		{
//			if (teleportType == ETeleportType.EPlayerTeleport) {
//				reference.position = targetPos;
//			}
//			else if (teleportType == ETeleportType.EObjectTeleport) {
//				//				marker.GetChild (0).transform.localPosition = Vector3.zero;
//				//				marker.gameObject.SetActive(true);
//				marker.position = targetPos;
//				//				marker.position = targetPos - new Vector3 (marker.GetChild (0).localPosition.x, 0f, marker.GetChild (0).localPosition.z);
//			}
//		}
//		line.enabled = false;
//	}


	public IEnumerator triggerFeedback(ushort hapticForce)	{
		var device = SteamVR_Controller.Input((int)trackedObj.index);	

		for (int i=0; i < 3; i++){
			device.TriggerHapticPulse (hapticForce);
			yield return null;
		}
	}
}
