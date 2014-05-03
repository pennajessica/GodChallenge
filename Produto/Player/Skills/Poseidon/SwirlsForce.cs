using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Skills.DamageSkill {
    [System.Serializable]
    public class SwirlsForce : DamageSkill {
        public SwirlsForce() {
            SkillIndex = 0;
            Name = "Swirl's Force";
            Description = "SEM DESCRIÇÃO - ADICIONAR!!";
            CastingTime = 10000f;
            Cooldown = 1000f;
            Damage = 150;
            Icon = Resources.Load("Skills/Poseidon/Swirls-Force") as Texture;
            IconInCooldown = Resources.Load("Skills/Poseidon/Swirls-Force-cd") as Texture;
            Effect = Resources.Load("Skills/Objects/Wave") as GameObject;
            WaitAnimationTime = 1.0f;
            DestroyEffectTime = 10f;
        }

    }
}
