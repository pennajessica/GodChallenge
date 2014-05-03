using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Skills.DamageSkill {
    [System.Serializable]
    public class MankindsJudge : DamageSkill {
        public MankindsJudge() {
            SkillIndex = 0;
            Name = "Mankind's Judge";
            Description = "Hades em um momento de concentração, carrega seu poder divino por alguns instantes e julga aqueles que o defrontam à morte.";
            CastingTime = 3000f;
            Cooldown = 1000f;
            Damage = 150;
            Icon = Resources.Load("Skills/Hades/Mankinds-Judge") as Texture;
            IconInCooldown = Resources.Load("Skills/Hades/Mankinds-Judge-cd") as Texture;
            Effect = Resources.Load("Skills/Objects/mankinds_judge") as GameObject;
            WaitAnimationTime = 3.0f;
            DestroyEffectTime = 10f;
        }
    }

}
