using UnityEngine;
using System.Collections;

namespace GodChallenge.Skills.Hades {
    public class MankindsJudge : SkillBehaviour {
 
		void Update() {
            if (ReadyToStart) {
					Vector3 direction = Vector3.forward / 3;
					this.transform.Translate (direction);
			}
        }
    }
}