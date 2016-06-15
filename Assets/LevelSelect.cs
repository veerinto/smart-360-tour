using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

    public int Level;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void selectLevel () {
        Application.LoadLevel(Level);
	}
}
