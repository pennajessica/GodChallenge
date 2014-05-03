using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System;
using GodChallenge.Domain.Skills.DamageSkill;
using GodChallenge.Domain.Skills.BlinkSkill;

namespace GodChallenge.Domain {
    [Serializable]
    public class Zeus : Player {
        //public const string XML_ZEUS_SKILL = "ZeusSkill";
        public Zeus(int hpMax, int level = 1)
            : base(hpMax, level) {
          //  base.readSkill(XML_ZEUS_SKILL, "Zeus");
            SkillList.Add(new ThunderBolt());
            SkillList.Add(new Thunderous());
            SkillList.Add(new ThunderPulse());
            SkillList.Add(new LightningBlind());
        }
    }
}