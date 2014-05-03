using UnityEngine;
using System.Collections;
using GodChallenge.Skills;

namespace GodChallenge.Domain.Skills.DamageSkill {
    
    [System.Serializable]
    public class DamageSkill : BaseSkill, IDamageSkill {
        public int damage;
        public int knockBack;


        public DamageSkill() : base() {
            this.knockBack = 1;
        }

        public float DamageVariationLevel {
            get { return this.damage * (0.5f * this.Level - 1); }
        }

        public int Damage {
            get { return this.damage + (int)DamageVariationLevel; }
            protected set { this.damage = value; }
        }

        public int KnockBackFactory {
            get { return this.knockBack; }
            protected set { this.knockBack = value; }
        }

        public override void ApplySkillEffect(Player targetPlayer, RTSController from) {
            base.ApplySkillEffect(targetPlayer, from);
            this.ApplyDamage(targetPlayer, from);
        }

        public void ApplyDamage(Player targetPlayer, RTSController from) {

            targetPlayer.Hp -= this.damage;
            targetPlayer.DamageCount += this.damage;

            if(from != null)
                this.ApplyKnockBack(targetPlayer, from);

            this.ApplyCooldown();
        }


        public void ApplyKnockBack(Player targetPlayer, RTSController from) {
            if (!targetPlayer.Controller)
                throw new SkillException("Controller was not found.");

            Debug.Log("DmgCount" + targetPlayer.DamageCount);
            Debug.Log("Knockback factory" + this.KnockBackFactory);

            Debug.Log((targetPlayer.DamageCount / this.KnockBackFactory));

            Ray ray = new Ray(from.transform.position, targetPlayer.Controller.transform.position);

            // TODO: Verificar como será feito o KNOCKBACK.

            Vector3 knockback = ray.direction * ((targetPlayer.DamageCount / 100) / this.KnockBackFactory);

            Debug.Log(knockback);

            targetPlayer.Controller.transform.Translate(knockback);
        }

        public override bool CastSkill(RTSController controller) {
            if(!base.CastSkill(controller))
                return false;

            this.LastActivation = Time.time;
            Ray ray = /*Camera.main.ScreenPointToRay*/controller.myCamera.ScreenPointToRay(controller.myMousePosition);

            if (Effect) {
                GameObject obj;
                InitialPosition = controller.skillsInitialPositions[SkillIndex];
                if (!controller.isOffline) {
                    obj = Network.Instantiate(Effect, InitialPosition.transform.position, Quaternion.identity, 0) as GameObject;
                } else {
                    obj = GameObject.Instantiate(Effect, InitialPosition.transform.position, Quaternion.identity) as GameObject;
                }
                //obj.transform.parent = controller.transform;
                SkillBehaviour skillBehaviour = obj.GetComponent<SkillBehaviour>();
                if (skillBehaviour) {
                    skillBehaviour.from = controller;
                    skillBehaviour.fromPlayer = controller.GetPlayer;
                    skillBehaviour.skill = this;
                    skillBehaviour.skillName = this.Name;
                    skillBehaviour.WaitAnimationTime = WaitAnimationTime;
                    skillBehaviour.StartPosition = InitialPosition;
                    skillBehaviour.DestroyTime = DestroyEffectTime;
                }
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
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
    }
}