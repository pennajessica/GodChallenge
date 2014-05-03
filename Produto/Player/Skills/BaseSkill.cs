using UnityEngine;
using System.Collections;
using System;
using System.Threading;

namespace GodChallenge.Domain.Skills {

    [System.Serializable]
    public class BaseSkill : ISkill {

        public string name;
        public string description;
        
        [NonSerialized]
        private Texture icon;
        
        [NonSerialized]
        private Texture iconCooldown;
        public float cooldown;
        public float castingTime;
        
        [NonSerialized]
        private GameObject initialPosition;
        private bool ready;
        
        [NonSerialized]
        private GameObject effect;
        public float lastActivation;
        private int skillIndex;
        private float waitAnimationTime;
        private float destroyEffectTime;

        public BaseSkill() {
            Level = 1;
            Ready = true;
            LastActivation = 0.0f;
        }

        public string Name {
            get {
                return this.name;
            }
            set {
                this.name = value;
            }
        }

        public string Description {
            get {
                return this.description;
            }
            set {
                this.description = value;
            }
        }

        public int Level { get; set; }

        public Texture Icon {
            get { return this.icon; }
            protected set { this.icon = value; }
        }

        public Texture IconInCooldown {
            get { return this.iconCooldown; }
            protected set { this.iconCooldown = value; }
        }

        public float Cooldown {
            get {
                return this.cooldown;
            }
            protected set {
                this.cooldown = value;
            }
        }

        public float CastingTime {
            get {
                return this.castingTime;
            }
            protected set {
                this.castingTime = value;
            }
        }

        public bool Ready {
            get { return this.ready; }
            protected set {
                this.ready = value;
            }
        }

        public GameObject Effect {
            get { return this.effect; }
            protected set { this.effect = value; }
        }

        public GameObject InitialPosition {
            get {
                return this.initialPosition;
            }
            protected set {
                this.initialPosition = value;
            }
        }

        public float LastActivation {
            get {
                return this.lastActivation;
            }
            protected set {
                this.lastActivation = value;
            }
        }

        public int SkillIndex {
            get { return this.skillIndex; }
            protected set {
                this.skillIndex = value;
            }
        }

        public float WaitAnimationTime {
            get { return this.waitAnimationTime; }
            protected set {
                this.waitAnimationTime = value;
            }
        }

        public float DestroyEffectTime {
            get { return this.destroyEffectTime; }
            protected set {
                this.destroyEffectTime = value;
            }
        }

        public virtual void ApplySkillEffect(Player targetPlayer, RTSController from) {
        }
        
        [System.NonSerialized]
        private Thread cdThread;
        
        /// <summary>
        /// Apply cooldown time in skill.
        /// If the skill is on cooldown, do nothing.
        /// </summary>
        public void ApplyCooldown() {
            this.LastActivation = Time.time;
            this.Ready = false;

            cdThread = new Thread(() => {
                Thread.Sleep(TimeSpan.FromMilliseconds(this.CastingTime));

                Thread.Sleep(TimeSpan.FromMilliseconds(cooldown));
                this.Ready = true;
                Debug.Log(this.Name + "(isReady: " + this.Ready + ")");
            });

            cdThread.Start();
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <returns>False if the skill can't be casted</returns>
        public virtual bool CastSkill(RTSController controller) {
            return Ready;
        }



    }
}
