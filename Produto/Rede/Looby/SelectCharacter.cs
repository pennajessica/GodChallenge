using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GodChallenge.Lobby;
using System.Linq;

public class SelectCharacter : MonoBehaviour {
    private int playerId;
    private string playerName;

    public List<GameObject> godModels;
    public List<Material> flagMaterials;
    public GameObject flag;
    private List<GodSelect> gods;

    private GameObject godSelected;
    private Material flagMaterialSelected;
    private bool canChangeGods = false;

    void Awake() {
        gods = new List<GodSelect>();
    }

    void Start() {
        playerName = Configuration.GetPlayerName();
        
        for (int x = 0; x < godModels.Count; x++) {
            gods.Add(new GodSelect(godModels[x], flagMaterials[x], godModels[x].name));
        }
    }

    void OnServerInitialized() {
        playerId = Network.connections.Length;
    }

    void OnConnectedToServer() {
        playerId = Network.connections.Length;
    }

    void OnGUI() {
        if (Network.peerType != NetworkPeerType.Disconnected) {
            GUILayout.Space(100);
            GUILayout.BeginHorizontal();

            for (int x = 0; x < gods.Count; x++) {
                if (GUILayout.Button(string.Format("{0}: {1}", gods[x].GetGodName(), gods[x].GetOwnerName()), GUILayout.Height(50), GUILayout.Width(100))) {
                    networkView.RPC("SelectGod", RPCMode.AllBuffered, x, playerName);
                    this.ChangeGodGraphics(x);
                }
            }

            GUILayout.EndHorizontal();
        }
    }

    private void ChangeGodGraphics(int index) {
        if (canChangeGods) {
            if(godSelected)
                godSelected.SetActive(false);

            godSelected = godModels[index];
            godSelected.SetActive(true);

            flagMaterialSelected = flagMaterials[index];
            flag.renderer.material = flagMaterialSelected;

            canChangeGods = false;
        }
    }

    [RPC]
    void SelectGod(int index, string nome) {
        GodSelect god = gods[index];

        //foreach (var item in gods) {
        //    if (item.IsMine(Network.connections.Length))
        //        item.SetDesselected();
        //}

        //List<GodSelect> mine = gods.FindAll(linq => linq.IsMine(playerId));
        //mine.ForEach(linq => linq.SetDesselected());

        if (!god.IsSelected()) {
            god.SetSelected(/*Network.player,*/ nome, playerId);
            Debug.Log("After Selected Owner: " + god.GetOwnerName());
            canChangeGods = true;
        }
    }
}
