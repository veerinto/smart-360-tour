using UnityEngine;
using System.Collections;
//using UnityEditor.SceneManagement;

public class PhotosphereManager : MonoBehaviour {

	public Photosphere[] photospheres;

	public float activePhotosphere = 0;
	public Photosphere activePS;

	public Transform returnPosition;
	public GameObject apartmentModel;

	public bool startAt360 = true;


	public bool returnPosSet = false;

//	public S

	// Use this for initialization
	void Start () {
		photospheres = GetComponentsInChildren<Photosphere> ();

		findActivePhotosphere ();
		if (activePS) {
			returnPosition = activePS.transform;
		} else {
			returnPosition.rotation = Quaternion.identity;
			returnPosition.position = Vector3.zero;
			returnPosition.localScale = Vector3.one;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (returnPosition.position);	
	}

	public void deactivatePhotospheres()
	{
		apartmentModel.SetActive (false);
//		EditorSceneManager.UnloadScene ("GearVR");
		foreach (Photosphere ps in photospheres) {
			ps.deactivatePhotosphere ();
		}
	}

	public void findActivePhotosphere()
	{
		foreach (Photosphere ps in photospheres) {
			if (ps.isActivated) {
				activePS = ps;
			}
		}
	}
}	
