using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Skills.DamageSkill {
    [System.Serializable]
    public class RiverExpansion : DamageSkill {
        public RiverExpansion() {
            SkillIndex = 0;
            Name = "River Expansion";
            Description = "Hades em representação a seu caminho para o Tártaro, expulsa seus inimigos em uma área como se os banhasse nas águas do rio Cíano.";
            CastingTime = 2500f;
            Cooldown = 1000f;
            Damage = 20;
            Icon = Resources.Load("Skills/Hades/River-Expansion") as Texture;
            IconInCooldown = Resources.Load("Skills/Hades/River-Expansion-cd") as Texture;
            Effect = Resources.Load("Skills/Objects/river_expansion") as GameObject;
            WaitAnimationTime = 1.0f;
            DestroyEffectTime = 10f;
        }
    }

}
