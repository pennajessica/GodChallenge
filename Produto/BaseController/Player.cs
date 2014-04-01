using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System;
using System.Linq;
using System.Threading;
using GodChallenge.Domain.Skills;

namespace GodChallenge.Domain {
    [System.Serializable]
    public class Player {
        private int hp;
        private int hpMax;
        private string name;
        private int level;
        private CharacterState state;
        private List<BaseSkill> skillList;
        private int damageCount;
        private float defaultSpeed;
        private float actualSpeed;
        private bool slowed;
        private Inventario inventario;
        private float gold;
        private int defense;
        private int actualDefense;
        private int attackDamage;
        // Mudar para RTSController
        private GameObject controller;

        public Player(int hpMax, int level = 1) {
            this.hpMax = hpMax;
            this.hp = hpMax;
            this.level = level;
            this.state = CharacterState.IDLE;
            this.skillList = new List<BaseSkill>();
            this.inventario = new Inventario(this);
            this.AttackDamage = 1;
            this.gold = 2500;
        }

        public int Hp {
            get { return hp; }
            set {
                if (hp <= 0) {
                    this.state = CharacterState.DEAD;
                    this.hp = 0;
                } else if (hp > hpMax) {
                    hp = hpMax;
                } else {
                    hp = value;
                }
            }
        }

        public int HpMax {
            get { return hpMax; }
        }

        public string Name {
            get { return name; }
        }

        public int Level {
            get { return level; }
        }

        public bool Slowed {
            get { return this.slowed; }
        }

        public CharacterState State {
            get { return state; }
            set { state = value; }
        }

        public float hpPercent {
            get {
                return (float)this.hp / (float)this.hpMax;
            }
        }

        public List<BaseSkill> SkillList {
            get { return this.skillList; }
        }

        public int DamageCount {
            get { return this.damageCount; }
            set { this.damageCount = value; }
        }

        public float DefaultSpeed {
            get { return this.defaultSpeed; }
            set { this.defaultSpeed = (value > 10 ? value : 10); }
        }

        public float ActualSpeed {
            get { return this.actualSpeed; }
            set { this.actualSpeed = (value > 10 ? value : 10); }
        }

        public GameObject Controller {
            get { return this.controller; }
            set { this.controller = value; }
        }

        public Inventario Inventario {
            get { return this.inventario; }
            protected set { this.inventario = value; }
        }

        public float Gold {
            get { return this.gold; }
            set {
                if (this.gold <= 0)
                    this.gold = 0;
                else
                    this.gold = value;
            } 
        }

        public int Defense {
            get { return defense; }
            protected set { defense = value; }
        }

        public int ActualDefense {
            get { return actualDefense; }
            set { actualDefense = value; }
        }


        public int AttackDamage {
            get { return attackDamage; }
            set { attackDamage = value; }
        }



        //public void readSkill(string xmlFilePath, string godName) {
        //    XDocument document = Util.loadXMLFile(xmlFilePath);
        //    var root = document.Descendants("Skills");

        //    var skills = from linq in root.Descendants("Skill")
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
        //        Skill sk = new Skill();
        //        sk.name = skill.name;
        //        sk.description = skill.description;
        //        sk.tooltip = skill.tooltip;
        //        sk.castingTime = float.Parse(skill.castTime);
        //        sk.cooldown = float.Parse(skill.cooldown);
        //        sk.SetType(int.Parse(skill.skillType));
        //        if (!String.IsNullOrEmpty(skill.iconName)) {
        //            Texture2D icon = Resources.Load(string.Format("Skills/{0}/{1}", godName, skill.iconName)) as Texture2D;
        //            Texture2D iconCD = Resources.Load(string.Format("Skills/{0}/{1}_cd", godName, skill.iconName)) as Texture2D;
        //            sk.icon = icon;
        //            sk.iconCd = iconCD;
        //        } else {
        //            sk.icon = new Texture2D(45, 45);
        //        }
        //        sk.particleName = skill.particleName;
        //        sk.damage = int.Parse(skill.damage);
        //        sk.StayRotate = Convert.ToBoolean(skill.stayRotate);
        //        sk.knockBackFactory = Convert.ToInt32(skill.knockBackFactory);
        //        //this.skillList.Add(sk);
        //    }
        //}

        /// <summary>
        /// TODO: Verificar qual a velocidade que o personagem irá mover / perder life.
        /// </summary>
        /// <returns></returns>
        public void applyDeadZone() {
            this.Hp -= 5;
            this.ActualSpeed = this.setSlowed(2);
        }

        public float setSlowed(int slowFactory) {
            this.slowed = true;
            new Thread(() => {
                Thread.Sleep(2000);
                this.slowed = false;
                this.ActualSpeed = this.DefaultSpeed;
            }).Start();

            return this.DefaultSpeed / slowFactory;
        }

    }

    public enum CharacterState {
        IDLE = 0,
        WALKING = 1,
        CASTING = 2,
        TAKING_HIT = 3,
        DEAD = 4
    }

    public enum CharacterType {
        Zeus = 0,
        Hades = 1,
        Poseidon = 2,
        ZeusTest = 3
    }
}