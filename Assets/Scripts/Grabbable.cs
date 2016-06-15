using UnityEngine;
using System.Collections;

public class Grabbable:MonoBehaviour {

    public bool isGrabbable = true;
    public bool isSpawner = false;
    public bool isHoldable = false;

    //If holdable tool is enabled
    public bool isEnabled = false;

    int grabber = 0;

    public int GrabberIndex { get; set; }

}
