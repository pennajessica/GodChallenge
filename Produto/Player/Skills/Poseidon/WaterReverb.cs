using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Skills.DamageSkill {
    [System.Serializable]
    public class WaterReverb : DamageSkill {
        public WaterReverb() {
            SkillIndex = 0;
            Name = "Water Reverb";
            Description = "Poseidon meneja seu tridente e atinge o chão com seu cabo, invocando as ondas obstinadas que expulsam seus inimigos.";
            CastingTime = 2500f;
            Cooldown = 1000f;
            Damage = 20;
            Icon = Resources.Load("Skills/Poseidon/Water-Reverb") as Texture;
            IconInCooldown = Resources.Load("Skills/Poseidon/Water-Reverb-cd") as Texture;
            Effect = Resources.Load("Skills/Objects/Wave") as GameObject;
            WaitAnimationTime = 1.0f;
            DestroyEffectTime = 10f;
        }
    }
}
