using UnityEngine;
using System.Collections;

public class WallBuilder : MonoBehaviour {

	SteamVR_TrackedObject trackedObj;


	public LineRenderer line;
	Transform reference;
	bool hasGroundTarget = false;

	bool creatingWall = false;

	public GameObject startPole;
	public GameObject endPole;

	public GameObject wallPrefab;
	GameObject wall;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		Transform eyeCamera = GameObject.FindObjectOfType<SteamVR_Camera>().GetComponent<Transform>();
		reference = eyeCamera.parent.parent;
	}

	// Update is called once per frame
	void Update ()
	{
		getInput ();
	}

	void getInput()
	{
		laserRayCast ();
		var device = SteamVR_Controller.Input((int)trackedObj.index);

		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
			setStart ();
		} else if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
			adjustWall ();
		} //else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {
//			setEnd ();
//		}
	}

	void setStart()
	{
//		laserRayCast ();
		startPole.transform.position = snapToGrid(getTargetPoint());
		wall = (GameObject)Instantiate (wallPrefab, startPole.transform.position, Quaternion.identity);
	}


	void adjustWall()
	{		
//		laserRayCast ();
		endPole.transform.position = snapToGrid(getTargetPoint ());
		startPole.transform.LookAt (endPole.transform);
		endPole.transform.LookAt (startPole.transform);
		float wallLength = Vector3.Distance (startPole.transform.position, endPole.transform.position);
		wall.transform.position = startPole.transform.position + (wallLength / 2 * startPole.transform.forward);
		wall.transform.rotation = startPole.transform.rotation;
		wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, wallLength + wall.transform.localScale.x);
	}

	void showMarker()
	{
//		laserRayCast ();
//		Color hintColor = startPoleRend.material.color;
//		hintColor.r = 1f;
//		startPoleRend.material.color = hintColor;
//		startPole.transform.position = getTargetPoint();
	}

	void toggleLaser(bool laserEnabled, Vector3 groundPos)
	{
		if (laserEnabled) {
			line.enabled = true;
			line.SetPosition (0, transform.position);
			line.SetPosition (1, groundPos);
		} else {
			line.enabled = false;
		}
	}

	void laserRayCast()
	{
		toggleLaser (true, getTargetPoint());
		if (!hasGroundTarget)
			toggleLaser (false, Vector3.zero);
	}

	Vector3 getTargetPoint(bool absolutePos = false)
	{
		Vector3 targetPos = Vector3.zero;
		float refY = reference.position.y;

		Plane plane = new Plane (Vector3.up, -refY);
		Ray ray = new Ray (this.transform.position, transform.forward);

		float dist = 0f;

		hasGroundTarget = plane.Raycast (ray, out dist);

		if (hasGroundTarget) {
			targetPos = ray.origin + ray.direction * dist;
			if(absolutePos)
				targetPos -= new Vector3 (reference.GetChild (0).localPosition.x, 0f, reference.GetChild (0).localPosition.z);
		}

		return targetPos;
	}

	Vector3 snapToGrid(Vector3 origPos)
	{
		float snap = 0.2f;
		Vector3 snapPos = new Vector3 (Mathf.Round (origPos.x / snap) * snap, Mathf.Round (origPos.y / snap) * snap, Mathf.Round (origPos.z / snap) * snap);
		return snapPos;
	}
}

