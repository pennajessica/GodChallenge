using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Skills.DamageSkill {
    [System.Serializable]
    public class AthenaAttackSpear : DamageSkill {
        public AthenaAttackSpear() {
            SkillIndex = 0;
            Name = "Attack Spear";
            Description = "Com habilidade de costume, Athena ataca de forma impiedosa seus inimigos com um golpe certeiro com sua lança.";
            CastingTime = 1450f;
            Cooldown = 1000f;
            Damage = 200;
            Icon = Resources.Load("Skills/Athena/Attack-Spear") as Texture;
            IconInCooldown = Resources.Load("Skills/Athena/Attack-Spear-cd") as Texture;
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