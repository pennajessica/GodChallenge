using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Skills.BlinkSkill {
    [System.Serializable]
    public class DarkRift : BlinkSkill {
        public DarkRift() {
            SkillIndex = 0;
            Name = "DarkRift";
            Description = "Sem Descrição - ADICIONAR!!!";
            CastingTime = 0;
            Cooldown = 10000;
            Icon = Resources.Load("Skills/Hades/Dark-Rift") as Texture;
            IconInCooldown = Resources.Load("Skills/Hades/Dark-Rift-cd") as Texture;
        }
    }

}
