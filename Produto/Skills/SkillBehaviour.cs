using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GodChallenge.Domain;
using GodChallenge.Domain.Skills;
using UnityEngine;

namespace GodChallenge.Skills {
    public class SkillBehaviour : MonoBehaviour {
        public RTSController from;
        public Player fromPlayer;
        public string skillName;
        public BaseSkill skill;

        public float WaitAnimationTime { get; set; }
        public bool ReadyToStart { get; set; }
        public GameObject StartPosition { get; set; }
        public float DestroyTime { get; set; }

        [RPC]
        void UpdateSkill(string skillName, float WaitAnimationTime, float DestroyEffectTime, byte[] player, byte[] skill) {
            this.skill = Tools.DeserializeObject<BaseSkill>(skill); 
            this.fromPlayer = Tools.DeserializeObject<Player>(player);
            this.skillName = skillName;
            this.WaitAnimationTime = WaitAnimationTime;
            this.DestroyTime = DestroyEffectTime;
        }

        void Start() {
            StartCoroutine(Tools.WaitSeconds(WaitAnimationTime, () => {
                ReadyToStart = true;
            }));

            //if (!StartPosition) {
            //GameObject obj = Network.Instantiate(StartPosition, StartPosition.transform.position, Quaternion.identity, 0) as GameObject;
            //obj.transform.parent;
            //}
            //if(networkView.isMine)
            networkView.RPC("UpdateSkill", RPCMode.Others, skillName, WaitAnimationTime, DestroyTime, Tools.SerializeObject<Player>(fromPlayer), Tools.SerializeObject<BaseSkill>(this.skill));

            Destroy(this.gameObject, DestroyTime);
        }

        void FixedUpdate() {
            if (!ReadyToStart && StartPosition)
                this.transform.position = StartPosition.transform.position;
        }

    }
}
