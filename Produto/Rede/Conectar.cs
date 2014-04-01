using UnityEngine;
using System.Collections;

public class Conectar : MonoBehaviour {
    public string remoteIP = "127.0.0.1";
    public int remotePort = 25000;
    public int listenPort = 25000;
    public bool useNAT = false;
    public string yourIP = "";
    public string yourPort = "";
    public Instanciar spawnCliente;
    public Instanciar spawnServidor;
    public Camera initialCamera;

    void OnGUI() {
        // Verificaçao de conexao
        if (Network.peerType == NetworkPeerType.Disconnected) {
            // Se nao estiver conectado conecta
            if (GUI.Button(new Rect(610, 210, 100, 50), "Conectar")) {


                Network.useNat = useNAT;
                // vai conectar ao ip informado do servidor
                Network.Connect(remoteIP, remotePort);
            }
            if (GUI.Button(new Rect(610, 270, 100, 50), "Criar Servidor")) {

                Debug.Log("clicou");
                Network.useNat = useNAT;
                // cria servidor
                Network.InitializeServer(32, listenPort);
            }
            //Local para o colocar IP e porta
            remoteIP = GUI.TextField(new Rect(720, 210, 100, 20), remoteIP);
            remotePort = System.Convert.ToInt32(GUI.TextField(new Rect(830, 210, 40, 20), remotePort.ToString()));
        } else {
            
            // pega IP e porta
            string ipaddress = Network.player.ipAddress;
            string port = Network.player.port.ToString();

            GUI.Label(new Rect(740, 520, 250, 40), "Endereço IP: " + ipaddress + ":" + port);
            if (GUI.Button(new Rect(610, 510, 100, 50), "Desconectar")) {
                // Desconecta
                Network.Disconnect(200);
            }

            if(this.initialCamera)
                Destroy(this.initialCamera.gameObject);
        }
    }

    void OnConnectedToServer() {
        // indica ao objeto sobre a conexao -quando ja conectado-
        spawnCliente.OnNetworkLoadedLevel();
    }
    void OnServerInitialized() {
        spawnServidor.OnNetworkLoadedLevel();
    }

}