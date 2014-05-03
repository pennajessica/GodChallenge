using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Skills.DamageSkill {

    [System.Serializable]
    public class Thunderous : DamageSkill {
        
        public Thunderous() {
            SkillIndex = 0;
            Name = "Thunderous";
            Description = "Carregando a força elétrica em seu punho, Zeus dispara sua potência contra seus inimigos, depois de alguns segundos de carga.";
            CastingTime = 2000f;
            Cooldown = 10000f;
            Damage = 150;
			Icon = Resources.Load("Skills/Zeus/Thunderous") as Texture;
            IconInCooldown = Resources.Load("Skills/Zeus/Thunderous-cd") as Texture;
            Effect = Resources.Load("Skills/Objects/thunderous") as GameObject;
            WaitAnimationTime = 20.0f;
        }

    }

}