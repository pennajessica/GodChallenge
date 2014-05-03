using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Skills.BlinkSkill {

    public class LightningBlind : BlinkSkill {
        public LightningBlind() {
            SkillIndex = 0;
            Name = "Lightning Blind";
            Description = "Como um relâmpago, Zeus se teleporta uma certa distância entre seus inimigos, sem que eles possam perceber.";
            CastingTime = 0;
            Cooldown = 10000;
			Icon = Resources.Load("Skills/Zeus/Lighting-Blind") as Texture;
            IconInCooldown = Resources.Load("Skills/Zeus/Lighting-Blind-cd") as Texture;
            //Effect = Resources.Load("Skills/Objects/thunderous") as GameObject;
        }
    }
}
