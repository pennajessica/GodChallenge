using UnityEngine;
using System.Collections;
using GodChallenge.Domain.Skills.DamageSkill;
using GodChallenge.Domain.Skills.BlinkSkill;

namespace GodChallenge.Domain {
    [System.Serializable]
    public class Athena : Player {
        public Athena(int hpMax, int level = 1)
            : base(hpMax, level) {

            SkillList.Add(new AthenaAttackSpear());
            SkillList.Add(new ExpelForce());
            SkillList.Add(new MedusaCharm());
            SkillList.Add(new ShieldProtection());
        }
    }
}