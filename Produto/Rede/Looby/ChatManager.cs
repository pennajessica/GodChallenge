using UnityEngine;
using System.Collections;
using GodChallenge.Manager;

public class ChatManager : MonoBehaviour {
    public Rect areaRect;
    public bool showInput = false;
    public string txtMsg = "";
    public Vector2 chatScroll = Vector2.zero;
    public GUIStyle chatStyle;

    void OnPlayerConnected(NetworkPlayer player) {
        string msg = string.Concat("[", GameManager.NetworkClock, "] Servidor: Jogador conectado!\n");
        GameManager.GameChat += msg;
        networkView.RPC("UpdateGameChat", RPCMode.OthersBuffered, msg);
    }

    void OnPlayerDisconnected(NetworkPlayer player) {
        GameManager.ClientMultiplayer p = GameManager.GetClientByOwner(player);

        string msg = string.Concat("[", GameManager.NetworkClock, "] Servidor: Jogador [", (p != null ? p.playerName : ""), "] desconectado!\n");
        GameManager.GameChat += msg;
        networkView.RPC("UpdateGameChat", RPCMode.OthersBuffered, msg);
    }

    [RPC]
    void UpdateGameChat(string msg) {
        GameManager.GameChat += msg;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Return)) {
            if (showInput) {
                SendMessage();
                showInput = false;
            } else {
                showInput = true;
                GUI.FocusControl("ChatInput");
            }
        }
    }

    private void SendMessage() {
        if (!string.IsNullOrEmpty(txtMsg)) {
            GameManager.ClientMultiplayer me = GameManager.GetClientByOwner(Network.player);
            GameManager.GameChat += string.Concat("[", GameManager.NetworkClock, "]", (me != null ? string.Concat(" ", me.playerName) : ""), ": ", txtMsg, "\n");
            networkView.RPC("UpdateGameChat", RPCMode.OthersBuffered, string.Concat("[", GameManager.NetworkClock, "]", (me != null ? string.Concat(" ", me.playerName) : ""), ": ", txtMsg, "\n"));
            txtMsg = string.Empty;
            chatScroll.y = Mathf.Infinity;
        }
    }


    void OnGUI() {
        if (Network.peerType != NetworkPeerType.Disconnected) {
            GUILayout.BeginArea(areaRect);

            GUI.Box(new Rect(0, 0, 350, 200), "");
            chatScroll = GUILayout.BeginScrollView(chatScroll, GUILayout.Width(350), GUILayout.Height(200));
            GUILayout.Label("Chat:\n" + GameManager.GameChat, chatStyle, GUILayout.MaxWidth(350));
            GUILayout.EndScrollView();

            if (showInput) {
                GUI.SetNextControlName("ChatInput");
                txtMsg = GUI.TextField(new Rect(0, 200, 290, 30), txtMsg, 100);
                if(GUI.Button(new Rect(295, 200, 55, 30), "Enviar"))
                    SendMessage();
            }

            GUILayout.EndArea();
        }
    }

}
