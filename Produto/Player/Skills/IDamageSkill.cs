using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Skills {

    public interface IDamageSkill {
        int Damage { get; }
        float DamageVariationLevel { get; }
        int KnockBackFactory { get; }

        /// <summary>
        /// Apply the skill damage to target player, that skill need to be an attack skill.
        /// </summary>
        /// <param name="targetPlayer"></param>
        void ApplyDamage(Player targetPlayer, RTSController from);

        /// <summary>
        /// Apply the skill's knockback after that collided with a player.
        /// </summary>
        /// <param name="targetPlayer"></param>
        /// <param name="from"></param>
        void ApplyKnockBack(Player targetPlayer, RTSController from);
    }

}