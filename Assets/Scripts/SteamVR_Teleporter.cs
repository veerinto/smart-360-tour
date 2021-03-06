using UnityEngine;
using System.Collections;

public class SteamVR_Teleporter : MonoBehaviour
{
	public enum ETeleportType
	{
		EPlayerTeleport,
		EObjectTeleport
	}

    Transform reference;
	public ETeleportType teleportType = ETeleportType.EPlayerTeleport;
	public Transform marker;
	public LineRenderer line;

	public Transform cameraRig;

	SteamVR_TrackedObject trackedObj;


	bool hasGroundTarget = false;
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
		reference = eyeCamera.parent.parent;
	}
	
	// Update is called once per frame
	void Update ()
    {

		var device = SteamVR_Controller.Input((int)trackedObj.index);

		if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
			showMarker ();
		}
		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
			blink ();
		}

		if (device.GetTouch (SteamVR_Controller.ButtonMask.Touchpad) && teleportType == ETeleportType.EPlayerTeleport) {
			float yAxis = device.GetAxis (Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y;
//			Debug.Log (yAxis);
			if (device.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
				if (yAxis > 0.3f) {
					cameraRig.localScale = new Vector3 (2f, 2f, 2f);
					StartCoroutine (triggerFeedback (3000));
				} else if (yAxis > -0.3f) {
					cameraRig.localScale = new Vector3 (1f, 1f, 1f);
					StartCoroutine (triggerFeedback (3000));
				} else {
					cameraRig.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
					StartCoroutine (triggerFeedback (3000));
				}
			}
		}

	}

	void showMarker()
    {
        // Teleport
        float refY = reference.position.y;

        Plane plane = new Plane(Vector3.up, -refY);
        Ray ray = new Ray(this.transform.position, transform.forward);

        float dist = 0f;
       
        hasGroundTarget = plane.Raycast(ray, out dist);

		if (hasGroundTarget) {
			var device = SteamVR_Controller.Input((int)trackedObj.index);	
			device.TriggerHapticPulse (800);
			targetPos = ray.origin + ray.direction * dist;

			if (teleportType == ETeleportType.EPlayerTeleport) {
				targetPos -= new Vector3 (reference.GetChild (0).localPosition.x, 0f, reference.GetChild (0).localPosition.z);
			}
		}

		line.enabled = true;
		line.SetPosition(0, transform.position);
		line.SetPosition(1, ray.origin + ray.direction * dist);
    }


	void blink()
	{		
		if (hasGroundTarget)
		{
			if (teleportType == ETeleportType.EPlayerTeleport) {
				reference.position = targetPos;
			}
			else if (teleportType == ETeleportType.EObjectTeleport) {
//				marker.GetChild (0).transform.localPosition = Vector3.zero;
//				marker.gameObject.SetActive(true);
				marker.position = targetPos;
//				marker.position = targetPos - new Vector3 (marker.GetChild (0).localPosition.x, 0f, marker.GetChild (0).localPosition.z);
			}
		}
		line.enabled = false;
	}


	public IEnumerator triggerFeedback(ushort hapticForce)	{
		var device = SteamVR_Controller.Input((int)trackedObj.index);	

		for (int i=0; i < 3; i++){
			device.TriggerHapticPulse (hapticForce);
			yield return null;
		}
	}
}
