using UnityEngine;
using System.Collections;

public class PopupManager : MonoBehaviour {
    public bool isPanelOpen = false;

    GameObject panel;

    void Start()
    {
        panel = transform.FindChild("InfoPanel").gameObject;
    }

    public void togglePanel()
    {
        isPanelOpen = !isPanelOpen;
    }

    void Update()
    {
        panel.SetActive(isPanelOpen);
    }
}
