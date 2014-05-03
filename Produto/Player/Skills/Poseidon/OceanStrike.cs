using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Skills.DamageSkill {
    [System.Serializable]
    public class OceanStrike : DamageSkill {
        public OceanStrike() {
            SkillIndex = 0;
            Name = "Ocean's Strike";
            Description = "Com destreza ímpar, Poseidon ataca seu inimigo com um golpe de água, semelhante à estocada de seu tridente.";
            CastingTime = 1000f;
            Cooldown = 1000f;
            Damage = 100;
            Icon = Resources.Load("Skills/Poseidon/Oceans-Strike") as Texture;
            IconInCooldown = Resources.Load("Skills/Poseidon/Oceans-Strike-cd") as Texture;
            Effect = Resources.Load("Skills/Objects/CFX3_IceBall_A") as GameObject;
            WaitAnimationTime = 1.0f;
            DestroyEffectTime = 10f;
        }
    }
}
