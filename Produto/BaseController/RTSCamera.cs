using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(RTSController))]
public class RTSCamera : MonoBehaviour {
    // Velocidade em que a câmera irá se locomover no cenário.
    public float cameraSpeed = 1.0f;
    // sensibilidade do zoom da câmera
    public float zoomSensibilidade = 10.0f;
    // Posição atual do mouse
    private Vector3 mousePos;
    // flag para ver se a câmera está travada.
    private bool lockCamera = false;
    // Objeto que a câmera irá travar
    public Transform target;
    private float zoomDistance;
    public RTSController rtsController;
    public Material materialToHideObject;
    List<HidedObject> hidedObject = new List<HidedObject>();

    void Awake() {
        if (!networkView.isMine && !rtsController.isOffline) {
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
        AudioListener al = this.GetComponent<AudioListener>();
        //al.audio.volume = al.audio.volume * Configuration.GetVolume();
    }

    internal void FixedUpdate() {
        if (networkView.isMine || this.rtsController.isOffline) {
            if (rtsController.canMoveCamera) {
                if (Input.GetButtonUp("LockCamera"))
                    this.lockCamera = !this.lockCamera;

                this.mousePos = Input.mousePosition;
                if (!this.lockCamera) {
                    float moveCamSpeed = (cameraSpeed / 100f);
                    // Deslocamento de câmera na Horizontal
                    if (this.mousePos.x <= 0 || (Input.GetButton("MoveCameraX") && Input.GetAxis("MoveCameraX") < 0)) {
                        this.transform.Translate(-moveCamSpeed, 0, 0, Space.World);
                    } else if (this.mousePos.x >= Screen.width - 10 || (Input.GetButton("MoveCameraX") && Input.GetAxis("MoveCameraX") > 0)) {
                        this.transform.Translate(moveCamSpeed, 0, 0, Space.World);
                    }
                    // Deslocamento de câmera na Vertical
                    if (this.mousePos.y <= 0 || (Input.GetButton("MoveCameraY") && Input.GetAxis("MoveCameraY") < 0)) {
                        this.transform.Translate(0, 0, -moveCamSpeed, Space.World);
                    } else if (this.mousePos.y >= Screen.height - 10 || (Input.GetButton("MoveCameraY") && Input.GetAxis("MoveCameraY") > 0)) {
                        this.transform.Translate(0, 0, moveCamSpeed, Space.World);
                    }
                } else {
                    this.transform.position = new Vector3(this.target.transform.position.x, this.transform.position.y, this.target.transform.position.z - (50 - (this.zoomDistance > 0 ? this.zoomDistance : 0)));
                }
                // Zoom In, Zoom Out
                if (Input.GetAxis("ZoomCamera") > 0) {
                    this.zoomDistance = (cameraSpeed * Input.GetAxis("ZoomCamera") * this.zoomSensibilidade / 100);
                    this.transform.Translate(0, 0, this.zoomDistance);
                } else if (Input.GetAxis("ZoomCamera") < 0) {
                    this.zoomDistance = (cameraSpeed * Input.GetAxis("ZoomCamera") * this.zoomSensibilidade / 100);
                    this.transform.Translate(0, 0, this.zoomDistance);
                }
            }
        }
    }

    void Update() {
        if (networkView.isMine || this.rtsController.isOffline) {
            RaycastHit hit;

            Vector3 foward = this.transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(this.transform.position, foward, out hit)) {
                if (hit.collider.CompareTag("ObjectToHide")) {
                    GameObject obj = hit.collider.gameObject;
                    HidedObject hided = new HidedObject(obj, this.materialToHideObject);
                    hided.hideObject();

                    HidedObject objs = hidedObject.Find(linq => linq.gameObject == obj);

                    if (objs == null)
                        hidedObject.Add(hided);
                } else {
                    if (hidedObject.Count > 0) {
                        try {
                            foreach (HidedObject hide in hidedObject) {
                                hide.showObject();
                                hidedObject.Remove(hide);
                            }
                        } catch (Exception) {
                            return;
                        }
                    }
                }
            }
        }
    }
}
class HidedObject {
    private readonly List<Material> defaultMaterial;
    private List<Material> hideMaterial;
    public GameObject gameObject;
    public HidedObject(GameObject gameObject, Material hideMaterial) {
        this.gameObject = gameObject;
        this.defaultMaterial = new List<Material>();
        this.hideMaterial = new List<Material>();
        foreach (Material material in gameObject.renderer.materials) {
            this.defaultMaterial.Add(material);
            this.hideMaterial.Add(hideMaterial);
        }
        //this.defaultMaterial = gameObject.renderer.material;
        //this.hideMaterial = hideMaterial;
    }

    internal void hideObject() {
        this.gameObject.renderer.materials = this.hideMaterial.ToArray();
    }

    internal void showObject() {
        this.gameObject.renderer.materials = this.defaultMaterial.ToArray();
        //this.gameObject.renderer.material = this.defaultMaterial;
    }
}