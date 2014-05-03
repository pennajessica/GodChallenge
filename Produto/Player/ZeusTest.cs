using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System;
using GodChallenge.Domain.Skills.DamageSkill;
using GodChallenge.Domain.Skills.BlinkSkill;

namespace GodChallenge.Domain {
    public class ZeusTest : Player {
        public ZeusTest(int hpMax, int level = 1)
            : base(hpMax, level) {
            SkillList.Add(new ThunderBolt());
            SkillList.Add(new Thunderous());
            SkillList.Add(new ThunderPulse());
            SkillList.Add(new LightningBlind());
        }
    }
}