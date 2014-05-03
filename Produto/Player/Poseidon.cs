using UnityEngine;
using System.Collections;
using GodChallenge.Domain.Skills.DamageSkill;
using GodChallenge.Domain.Skills.BlinkSkill;

namespace GodChallenge.Domain {
    [System.Serializable]
    public class Poseidon : Player {
        //private const string XML_POSEIDON_SKILL = "PoseidonSkill";
        public Poseidon(int hpMax, int level = 1)
            : base(hpMax, level) {
                //base.readSkill(XML_POSEIDON_SKILL, "Poseidon");
            SkillList.Add(new OceanStrike());
            SkillList.Add(new SwirlsForce());
            SkillList.Add(new WaterReverb());
            SkillList.Add(new WaterPath());
        }
    }
}