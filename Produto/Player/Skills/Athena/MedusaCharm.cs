using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Skills.DamageSkill {
    [System.Serializable]
    public class MedusaCharm : DamageSkill {

        public MedusaCharm() {
            SkillIndex = 0;
            Name = "Medusa Charm";
            Description = "Athena utiliza o feitiço da Medusa presente em seu escudo para paralisar o inimigo por alguns segundos.";
            CastingTime = 1416f;
            Cooldown = 1000f;
            Damage = 100;
            Icon = Resources.Load("Skills/Athena/Medusa-Charm") as Texture;
            IconInCooldown = Resources.Load("Skills/Athena/Medusa-Charm-cd") as Texture;
        }

        public override bool CastSkill(RTSController controller) {
            if (!base.Ready)
                return false;

            this.LastActivation = Time.time;
            Ray ray = controller.myCamera.ScreenPointToRay(controller.myMousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Vector3 look = hit.point;
                look.y = controller.transform.position.y;
                controller.transform.LookAt(look);
            }

            ApplyCooldown();

            return true;
        }
    }
}
