using System;
using UnityEngine;

namespace GodChallenge.Lobby {
    public class GodSelect {
        private bool selected;
        private GameObject godModel;
        private Material flagMaterial;
        private NetworkPlayer owner;
        private string playerName;
        private string godName;
        private int playerId;

        public GodSelect(GameObject godModel, Material flagMaterial, string godName) {
            this.godModel = godModel;
            this.flagMaterial = flagMaterial;
            this.selected = false;
            this.godName = godName;
        }

        public void SetSelected(/*NetworkPlayer owner, */string playerName, int playerId) {
            //this.owner = owner;
            this.selected = true;
            this.playerName = playerName;
            this.playerId = playerId;
        }

        public bool IsSelected() {
            return this.selected;
        }

        public void SetDesselected() {
            this.selected = false;
            this.owner = default(NetworkPlayer);
            this.playerName = string.Empty;
        }

        public bool IsMine(int playerId) {
            return this.playerId == playerId;
        }

        public string GetOwnerName() {
            return this.playerName;
        }

        public string GetGodName() {
            return this.godName;
        }
    }
}
