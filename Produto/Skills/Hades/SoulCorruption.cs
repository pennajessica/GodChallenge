using UnityEngine;
using System.Collections;

namespace GodChallenge.Skills.Hades {

    public class SoulCorruption : SkillBehaviour {

        void Update() {
			if (ReadyToStart)
            	this.transform.Translate(Vector3.forward);
        }
    }
}