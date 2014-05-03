using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Skills.DamageSkill {
    [System.Serializable]
    class ShieldProtection: DamageSkill {

        public ShieldProtection() {
            SkillIndex = 0;
            Name = "Shield Protection";
            Description = "Athena acumula forças e se protege atrás de seu poderoso escudo, evitando que seus inimigos a ataquem.";
            CastingTime = 1458f;
            Cooldown = 1000f;
            Damage = 0;
            Icon = Resources.Load("Skills/Athena/Shield-Protection") as Texture;
            IconInCooldown = Resources.Load("Skills/Athena/Shield-Protection-cd") as Texture;
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