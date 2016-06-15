using UnityEngine;
using System.Collections;

public class ViveInput : MonoBehaviour {

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerButtonDown = false;
    public bool triggerButtonUp = false;
    public bool triggerButtonPressed = false;

    private SteamVR_Controller.Device controller { 
		get { 
			return SteamVR_Controller.Input((int)trackedObj.index); 
		} 
	}
    private SteamVR_TrackedObject trackedObj; 

	// Reference to hand object for animation
	public GameObject hand;
	private Animator anim;
	int grabHash = Animator.StringToHash("Grab");


	// Use this for initialization
	void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

		// Get the Animator component of hand
		anim = hand.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	    if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);

		// Trigger hand animation
        if (triggerButtonPressed)
        {
			anim.SetTrigger (grabHash);
		} else {
			anim.SetBool("Grab", false);
		}

/*        if (triggerButtonUp)
        {
            Debug.Log("Trigger Button was just released.");
        }
*/
    }
}
