using UnityEngine;
using System.Collections;
using System.Threading;

namespace GodChallenge.Skills.Zeus {

    public class ThunderBolt : SkillBehaviour {

        void Update() {
            if (ReadyToStart)
                this.transform.Translate(Vector3.forward);
        }

    }
}