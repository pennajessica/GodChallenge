using UnityEngine;
using System.Collections;
using GUIHelper;

public class CreateServer : GUIButtonCreator {
    private string remoteIP = "127.0.0.1";
    private int remotePort = 25000;
    private int listenPort = 25000;
    private bool useNAT = false;
    private string yourIP = "";
    private string yourPort = "";
    public Instanciar spawnServidor;
    public Camera initialCamera;

    public Texture2D hoverImage;
    private Texture2D defaultImage;

    void Awake() {
        this.defaultImage = this.texture;
    }

    protected override void onMouseOver() {
        base.onMouseOver();
        this.texture = hoverImage;
        this.createCanvas();
    }

    protected override void onMouseOut() {
        base.onMouseOut();
        this.texture = this.defaultImage;
        this.createCanvas();
    }

    void Update() {
        if (Network.peerType != NetworkPeerType.Disconnected) {
            if (this.initialCamera)
                Destroy(this.initialCamera.gameObject);

            this.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }


    protected override void onClick() {
        if (Network.peerType == NetworkPeerType.Disconnected) {
            Debug.Log("clicou");
            Network.useNat = useNAT;
            // cria servidor
            Network.InitializeServer(32, listenPort);
        }
    }

    void OnServerInitialized() {
        spawnServidor.OnNetworkLoadedLevel();
    }
}
