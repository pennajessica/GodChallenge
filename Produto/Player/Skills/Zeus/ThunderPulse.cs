using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Skills.DamageSkill {

    [System.Serializable]
    public class ThunderPulse : DamageSkill {
        public ThunderPulse() {
            SkillIndex = 0;
            Name = "Thunder Pulse";
            Description = "Um campo elétrico que afasta os inimigos, como uma explosão do poder de Zeus.";
            CastingTime = 2500;
            Cooldown = 5000;
            Damage = 20;
			Icon = Resources.Load("Skills/Zeus/Thunder-Pulse") as Texture;
            IconInCooldown = Resources.Load("Skills/Zeus/Thunder-Pulse-cd") as Texture;
            Effect = Resources.Load("Skills/Objects/thunder_pulse") as GameObject;
            WaitAnimationTime = 1.7f;
        }

    }
}
