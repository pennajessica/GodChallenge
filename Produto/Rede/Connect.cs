using UnityEngine;
using System.Collections;
using GUIHelper;

public class Connect : GUIButtonCreator {
    public string remoteIP = "127.0.0.1";
    public int remotePort = 25000;
    public int listenPort = 25000;
    public bool useNAT = false;
    public string yourIP = "";
    public string yourPort = "";
    
    public Texture2D hoverImage;
    private Texture2D defaultImage;

    public Instanciar spawnCliente;

    public Camera initialCamera;

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

            this.gameObject.SetActive(false);
        }
    }

    public override void onGUI() {
        base.onGUI();
        Rect ipRect = base.Canvas;
        ipRect.y -= 55;
        ipRect.width -= 60;
        //Local para o colocar IP e porta
        remoteIP = GUI.TextField(ipRect, remoteIP);
        
        Rect portRect = base.Canvas;
        portRect.y -= 55;
        portRect.x = ipRect.width + ipRect.x;
        portRect.width = 50;

        remotePort = System.Convert.ToInt32(GUI.TextField(portRect, remotePort.ToString()));
    }
    protected override void onClick() {
        if (Network.peerType == NetworkPeerType.Disconnected) {
            Network.useNat = useNAT;
            // vai conectar ao ip informado do servidor
            Network.Connect(remoteIP, remotePort);
        }
    }

    void OnConnectedToServer() {
        // indica ao objeto sobre a conexao -quando ja conectado-
        spawnCliente.OnNetworkLoadedLevel();
    }

}
