using UnityEngine;
using System.Collections;

public class RTSHudController : MonoBehaviour {
    public RTSController controller;

	void Start () {
        if (!networkView.isMine && !this.controller.isOffline) {
            Destroy(this.gameObject);
            //this.gameObject.SetActive(false);
        }
	}
	
	void Update () {
	
	}
}
