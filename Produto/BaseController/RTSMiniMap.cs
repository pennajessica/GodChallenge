using UnityEngine;
using System.Collections;
using System;

public class RTSMiniMap : MonoBehaviour {
    public GameObject target;
    public Texture2D minimapTexture;
    public bool isOffline = false;

    void Awake() {
        if(!this.isOffline)
            this.isOffline = (Application.loadedLevelName == "Tutorial");

        if (networkView.isMine || this.isOffline) {
            if (this.target == null)
                throw new Exception("Attach the target that camera will follow.");
        } else {
            this.gameObject.SetActive(false);
        }
        //Debug.Log(Screen.width + "-" + Screen.height);

        /*float width = (float)minimapTexture.width * 0.17f / 317f;
        float height = (float)minimapTexture.height * 0.29f / 241f;

        this.camera.rect = new Rect(0.01f, 0.01f, width, height);*/
    }

	void Start () {
        if (networkView.isMine || this.isOffline) {
            moveCamera(this.target.transform.position);
        }
	}
	
	void Update () {
        if (networkView.isMine || this.isOffline) {
            moveCamera(this.target.transform.position);
        }
	}

    private void moveCamera(Vector3 position) {
        this.transform.position = new Vector3(position.x, this.transform.position.y, position.z);
    }
}
