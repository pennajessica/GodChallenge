using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using GodChallenge.Skills;
using System.Threading;

namespace GodChallenge.Domain.Skills.DamageSkill {

    [System.Serializable]
    public sealed class ThunderBolt : DamageSkill {
        
        public ThunderBolt() {
            SkillIndex = 0;
            Name = "Thuder Bolt";
            Description = "O poderoso carro-chefe de Zeus, um raio único que atinge o inimigo com toda a fúria do deus do trovão e o empurra para trás.";
            CastingTime = 1000f;
            Cooldown = 1000f;
            Damage = 100;
			Icon = Resources.Load("Skills/Zeus/ThunderBolt") as Texture;
            IconInCooldown = Resources.Load("Skills/Zeus/ThunderBolt-cd") as Texture;
            Effect = Resources.Load("Skills/Objects/thunder_bolt") as GameObject;
            WaitAnimationTime = 1.0f;
            DestroyEffectTime = 2.5f;
        }

        public override bool CastSkill(RTSController controller) {
            return base.CastSkill(controller);
        }

    }
}