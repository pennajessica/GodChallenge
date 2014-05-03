using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain {
    
    public interface ISkill {
        string Name { get; set; }
        string Description { get; set; }
        Texture Icon { get; }
        Texture IconInCooldown { get; }
        float Cooldown { get; }
        int Level { get; set; }
        float CastingTime { get; }
        bool Ready { get; }
        GameObject Effect { get; }
        GameObject InitialPosition { get; }
        float LastActivation { get; }
        int SkillIndex { get; }
        float WaitAnimationTime { get; }
        float DestroyEffectTime { get; }

        void ApplySkillEffect(Player targetPlayer, RTSController from);
        void ApplyCooldown();
        bool CastSkill(RTSController controller);
    }

}