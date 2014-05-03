using UnityEngine;
using System.Collections;
using GodChallenge.Domain.Skills.DamageSkill;
using GodChallenge.Domain.Skills.BlinkSkill;

namespace GodChallenge.Domain {
    [System.Serializable]
    public class Artemis : Player {
        //private const string XML_HADES_SKILL = "HadesSkill";
        public Artemis(int hpMax, int level = 1)
            : base(hpMax, level) {
                //base.readSkill(XML_HADES_SKILL, "Hades");
            SkillList.Add(new SoulCorruption());
            SkillList.Add(new MankindsJudge());
            SkillList.Add(new RiverExpansion());
            SkillList.Add(new DarkRift());
        }
    }
}