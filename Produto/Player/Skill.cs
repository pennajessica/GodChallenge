using UnityEngine;
using System.Collections;
using System.Threading;
using System;
using System.Linq;
using GodChallenge.Skills;

namespace GodChallenge.Domain {
    
    [System.Serializable]
    public class Skill {
        public string name;
        public string description;
        public string tooltip;
        public Texture2D icon;
        public Texture2D iconCd;
        public float cooldown;
        public float castingTime;
        public int damage;
        public int level;
        public int knockBackFactory;
        public string particleName;
        
        private bool enable;
        private SkillType? type = null;

        private bool inCooldown = false;
        public float lastActivation;

        private Thread cdThread;
        private bool stayRotate;

        public bool StayRotate { set { this.stayRotate = value; } }
        public bool InCooldown { get { return this.inCooldown; } }

        public void SetType(int type) {
            SkillType? value = null;
            switch (type) {
                case 1:
                    value = SkillType.BUFF;
                    break;
                case 2:
                    value = SkillType.DEBUFF;
                    break;
                case 3:
                    value = SkillType.ATTACK;
                    break;
                case 4:
                    value = SkillType.BLINK;
                    break;
                case 5:
                    value = SkillType.ULTIMATE;
                    break;
            }

            if (this.type == null)
                this.type = value;
        }

        /// <summary>
        /// Apply cooldown time in skill.
        /// If the skill is on cooldown, do nothing.
        /// </summary>
        private void ApplyCooldown() {
            if (this.inCooldown)
                return;

            this.lastActivation = Time.time;
            this.inCooldown = true;

            cdThread = new Thread(() => {
                Thread.Sleep(TimeSpan.FromMilliseconds(this.castingTime));

                Thread.Sleep(TimeSpan.FromMilliseconds(cooldown));
                this.inCooldown = false;
                Debug.Log(this.name + "(inCooldown: " + this.inCooldown + ")");
            });

            cdThread.Start();
        }

        /// <summary>
        /// Apply the skill damage to target player, that skill need to be an attack skill.
        /// </summary>
        /// <param name="targetPlayer"></param>
        protected virtual void ApplyDamage(Player targetPlayer, RTSController from) {
            if (type != SkillType.ATTACK)
                throw new SkillException("This isn't an Attack Skill.");

            this.SkillDamage(targetPlayer);

            this.ApplyKnockBack(targetPlayer, from);
            this.ApplyCooldown();
        }

        private void SkillDamage(Player targetPlayer) {
            targetPlayer.Hp -= this.damage;
            targetPlayer.DamageCount += this.damage;
        }

        /// <summary>
        /// Apply a buff to target player, that skill need to be a buff skill.
        /// </summary>
        /// <param name="targetPlayer"></param>
        protected virtual void ApplyBuff(Player targetPlayer) {
            if (type != SkillType.BUFF)
                throw new SkillException("This isn't a Buff Skill.");
        }

        /// <summary>
        /// Apply a debuff to target player, that skill need to be a debuff skill.
        /// </summary>
        /// <param name="targetPlayer"></param>
        protected virtual void ApplyDebuff(Player targetPlayer, RTSController from) {
            if (type != SkillType.DEBUFF)
                throw new SkillException("This isn't a Buff Skill.");

            if (this.damage > 0)
                this.SkillDamage(targetPlayer);

            targetPlayer.setSlowed(3);
            this.ApplyKnockBack(targetPlayer, from);
        }

        protected virtual void ApplyKnockBack(Player targetPlayer, RTSController from) {
            if (!targetPlayer.Controller)
                throw new SkillException("Controller was not found.");

            Debug.Log("DmgCount" + targetPlayer.DamageCount);
            Debug.Log("Knockback factory" + this.knockBackFactory);

            Debug.Log((targetPlayer.DamageCount / this.knockBackFactory));
            
            Ray ray = new Ray(from.transform.position, targetPlayer.Controller.transform.position);

            // TODO: Verificar como será feito o KNOCKBACK.

            Vector3 knockback = ray.direction * ((targetPlayer.DamageCount / 100) / this.knockBackFactory);

            Debug.Log(knockback);

            targetPlayer.Controller.transform.Translate(knockback);
        }

        public virtual void ApplySkillEffect(Player targetPlayer, RTSController from) {
            switch (type) {
                case SkillType.BUFF:
                    this.ApplyBuff(from.GetPlayer);
                    break;
                case SkillType.DEBUFF:
                    this.ApplyDebuff(targetPlayer, from);
                    break;
                case SkillType.ATTACK:
                    this.ApplyDamage(targetPlayer, from);
                    break;
                case SkillType.ULTIMATE:
                    this.ApplyDamage(targetPlayer, from);
                    this.ApplyDebuff(targetPlayer, from);
                    break;
            }
        }

        public bool CanCastSkill() {
            return !this.inCooldown;
        }

        /// <summary>
        /// Cast the skill.
        /// </summary>
        /// <returns>False if the skill can't be casted</returns>
        public virtual bool CastSkill(RTSController controller) {
            
            if (!this.CanCastSkill())
                return false;
            
            this.lastActivation = Time.time;

            if (this.type == SkillType.BLINK) {
                return this.CastBlink(controller);
            }

            GameObject skill = Resources.Load(string.Format("Skills/Objects/{0}", this.particleName)) as GameObject;
            
            Ray ray = /*Camera.main.ScreenPointToRay*/controller.myCamera.ScreenPointToRay(controller.myMousePosition);
            
            if (skill) {
                GameObject obj;
                if (!controller.isOffline) {
                    obj = Network.Instantiate(skill, controller.transform.position, skill.transform.rotation, 0) as GameObject;
                } else {
                    obj = GameObject.Instantiate(skill, controller.transform.position, skill.transform.rotation) as GameObject;
                }
                //obj.transform.parent = controller.transform;
                SkillBehaviour skillBehaviour = obj.GetComponent<SkillBehaviour>();
                if (skillBehaviour) {
                    skillBehaviour.from = controller;
                    skillBehaviour.fromPlayer = controller.GetPlayer;
                    //skillBehaviour.skill = this;
                    skillBehaviour.skillName = this.name;
                }
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && !this.stayRotate) {
                    Vector3 look = hit.point;
                    look.y = look.y + 3.5f;
                    obj.transform.LookAt(look);
                    look.y = controller.transform.position.y;
                    controller.transform.LookAt(look);
                }
            }
            ApplyCooldown();

            return true;
        }

        protected virtual bool CastBlink(RTSController controller) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] raycastHits = Physics.RaycastAll(ray);
            RaycastHit hit;

            try {
                hit = raycastHits.Where(linq => linq.transform.gameObject.tag == "Terrain").First();
                Vector3 position = hit.point;
                position.y += 3.5f;
                controller.transform.position = position;
                controller.targetPosition = position;
                ApplyCooldown();
            } catch (Exception) {
                return false;
            }

            return true;
        }

        public override string ToString() {
            return this.name;
        }

        //protected void BuildSkill(string path, string godName, string skillName) {
        //    XDocument document = Util.loadXMLFile(path);
        //    var root = document.Descendants("Skills");

        //    var skills = from linq in root.Descendants("Skill").Where(l => l.Element("name").Value == skillName)
        //                 select new {
        //                     name = linq.Element("name").Value,
        //                     description = linq.Element("description").Value,
        //                     tooltip = linq.Element("tooltip").Value,
        //                     castTime = linq.Element("castTime").Value,
        //                     cooldown = linq.Element("cooldown").Value,
        //                     damage = linq.Element("damage").Value,
        //                     skillType = linq.Element("skillType").Value,
        //                     knockBackFactory = linq.Element("knockBackFactory").Value,
        //                     particleName = linq.Element("particleName").Value,
        //                     iconName = linq.Element("iconName").Value,
        //                     stayRotate = linq.Element("stayRotate").Value
        //                 };

        //    foreach (var skill in skills) {
        //        this.name = skill.name;
        //        this.description = skill.description;
        //        this.tooltip = skill.tooltip;
        //        this.castingTime = float.Parse(skill.castTime);
        //        this.cooldown = float.Parse(skill.cooldown);
        //        this.SetType(int.Parse(skill.skillType));
        //        if (!String.IsNullOrEmpty(skill.iconName)) {
        //            Texture2D icon = Resources.Load(string.Format("Skills/{0}/{1}", godName, skill.iconName)) as Texture2D;
        //            Texture2D iconCD = Resources.Load(string.Format("Skills/{0}/{1}_cd", godName, skill.iconName)) as Texture2D;
        //            this.icon = icon;
        //            this.iconCd = iconCD;
        //        } else {
        //            this.icon = new Texture2D(45, 45);
        //        }
        //        this.particleName = skill.particleName;
        //        this.damage = int.Parse(skill.damage);
        //        this.StayRotate = Convert.ToBoolean(skill.stayRotate);
        //        this.knockBackFactory = Convert.ToInt32(skill.knockBackFactory);
        //    }
        //}

    }

    enum SkillType {
        BUFF = 1,
        DEBUFF = 2,
        ATTACK = 3,
        BLINK = 4,
        ULTIMATE = 5
    }

    public class SkillException : Exception {
        public SkillException(string msg)
            : base(msg) {

        }
    }
}