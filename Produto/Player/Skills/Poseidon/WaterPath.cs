using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Skills.BlinkSkill {
    [System.Serializable]
    public class WaterPath : BlinkSkill {
        public WaterPath() {
            SkillIndex = 0;
            Name = "Lightning Blind";
            Description = "O deus dos mares engana seus inimigos, e controla sua consistência para caminhar como água por entre eles, e retornar em outro lugar.";
            CastingTime = 0;
            Cooldown = 10000;
            Icon = Resources.Load("Skills/Poseidon/water-path") as Texture;
            IconInCooldown = Resources.Load("Skills/Poseidon/water-path-cd") as Texture;
        }
    }
}
