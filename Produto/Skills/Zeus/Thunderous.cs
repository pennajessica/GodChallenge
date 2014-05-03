using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GodChallenge.Skills.Zeus {
    public class Thunderous : SkillBehaviour {
        private ParticleSystem particleSys;
        public ParticleSystem thunder;
        private float timeStarted;

        void Awake() {
            this.particleSys = this.GetComponent<ParticleSystem>();
        }

        void Start() {
            Destroy(this.gameObject, this.particleSys.duration + 1);
            this.timeStarted = Time.time;
        }

        void Update() {
            if ((timeStarted + this.thunder.startDelay) - Time.time <= 0) {
                this.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }
}
