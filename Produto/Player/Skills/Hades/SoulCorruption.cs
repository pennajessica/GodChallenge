using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Skills.DamageSkill {
    [System.Serializable]
    public class SoulCorruption : DamageSkill {
        public SoulCorruption() {
            SkillIndex = 0;
            Name = "Soul Corruption";
            Description = "O poderoso deus dos mortos estende sua mão misericordiosamente para colher a alma daqueles que o enfrentam.";
            CastingTime = 1000f;
            Cooldown = 1000f;
            Damage = 100;
            Icon = Resources.Load("Skills/Hades/Soul-Corruption") as Texture;
            IconInCooldown = Resources.Load("Skills/Hades/Soul-Corruption-cd") as Texture;
            Effect = Resources.Load("Skills/Objects/soul_corruption") as GameObject;
            WaitAnimationTime = 1.0f;
            DestroyEffectTime = 10f;
        }
    }

}
