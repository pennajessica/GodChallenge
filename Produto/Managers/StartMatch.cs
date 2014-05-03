using UnityEngine;
using System.Collections;
using GodChallenge.Manager;

public class StartMatch : MonoBehaviour {
    GameManager.ClientMultiplayer me;
    public RTSCamera sceneCamera;
    public Texture2D[] icons;
    public GameObject[] gods;
    public GameObject[] spawns;

    void Awake() {
        //if (Network.isServer) {
        //    RandomizeSpawns();
        //}
        spawns = GameObject.FindGameObjectsWithTag("Respawn");
        //GameManager.startMatch(5);
        foreach (var item in Network.connections) {
            networkView.RPC("ImReady", item);
        }
        //networkView.RPC("ImReady", RPCMode.AllBuffered);
        me = GameManager.GetClientByOwner(Network.player);
    }

    void Start() {
        GameObject obj = (Network.Instantiate(gods[(int)me.selected.type], spawns[me.playerId].transform.position, Quaternion.identity, 0) as GameObject);
        RTSController control = obj.GetComponentInChildren<RTSController>();
        //control._animator = control.GetComponentInChildren<Animator>();
        RTSCamera cam = Network.Instantiate(sceneCamera, control.transform.position, Quaternion.identity, 0) as RTSCamera;
        cam.transform.parent = control.transform.parent;
        cam.transform.localPosition = new Vector3(0, 15, -15);
        cam.transform.Rotate(Vector3.right, 40);
        cam.name = string.Concat(sceneCamera.name, " (", control.playerName, ")");
        cam.rtsController = control;
        control.myCamera = cam.camera;
        control.myIcon = icons[(int)me.selected.type];
        RTSStatusBar statusBar = control.GetComponentInChildren<RTSStatusBar>();
        statusBar._controller = control;
        statusBar._mainCamera = cam.camera;
        //RTSMiniMap minimap = obj.GetComponent<RTSMiniMap>();
        //minimap.target = control.gameObject;
    }

    [RPC]
    void ImReady() {
        GameManager.SetReadyToPlay(Network.player);
    }

    //void RandomizeSpawns() {
    //    Object[] arr = Tools.MessArray(GameObject.FindGameObjectsWithTag("Respawn")) as Object[];
    //    networkView.RPC("SetSpawns", RPCMode.AllBuffered, arr);
    //}
    //[RPC]
    //void SetSpawns(Object spawn1, Object spawn2, Object spawn3, Object spawn4, Object spawn5, Object spawn6) {
    //    spawns = new GameObject[6];
    //    spawns[0] = (GameObject)spawn1;
    //    spawns[1] = (GameObject)spawn2;
    //    spawns[2] = (GameObject)spawn3;
    //    spawns[3] = (GameObject)spawn4;
    //    spawns[4] = (GameObject)spawn5;
    //    spawns[5] = (GameObject)spawn6;
    //}

    //void OnGUI() {
    //    string test = "";
    //    foreach (var item in GameManager.Clients) {
    //        test += item + "\n";
    //    }

    //    GUILayout.Label(test);
    //}

    void Update() {

    }
}
