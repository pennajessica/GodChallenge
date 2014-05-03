using UnityEngine;
using System.Collections;
using System;
using System.Linq;

namespace GodChallenge.Domain.Skills.DamageSkill {
    [System.Serializable]
    class ExpelForce : DamageSkill {

        public ExpelForce() {
            SkillIndex = 0;
            Name = "Expel Force";
            Description = "Athena acumula forças e bruscamente golpeia seu inimigo com seu poderoso escudo, afastando o inimigo.";
            CastingTime = 1875f;
            Cooldown = 1000f;
            Damage = 250;
            Icon = Resources.Load("Skills/Athena/Expel-Force") as Texture;
            IconInCooldown = Resources.Load("Skills/Athena/Expel-Force-cd") as Texture;
        }

        public override bool CastSkill(RTSController controller) {
            if (!base.Ready)
                return false;

            this.LastActivation = Time.time;
            Ray ray = controller.myCamera.ScreenPointToRay(controller.myMousePosition);

            RaycastHit[] raycastHits = Physics.RaycastAll(ray);
            RaycastHit hit;
            Vector3 look = Vector3.zero;
            Vector3 position = Vector3.zero;
            try {
                raycastHits = raycastHits.Where(linq => linq.transform.gameObject.tag == "Terrain").ToArray();
                hit = raycastHits.Last();
                position = hit.point;
                position.y += 3.5f;
                look = hit.point;
                look.y = controller.transform.position.y;
                controller.transform.LookAt(look);
            } catch (Exception) {
                return false;
            }

           
            ApplyCooldown();
            //Debug.Log(controller.transform.position.y - hit.point.y);

            //if (controller.transform.position.y - hit.point.y > 3.48f && controller.transform.position.y - hit.point.y < 3.52f) {
            //    look.y = controller.transform.position.y;
            //} else if (controller.transform.position.y < hit.point.y) {
            //    look.y = controller.transform.position.y + hit.point.y - 2;
            //} else if (controller.transform.position.y > hit.point.y) {
            //    look.y = controller.transform.position.y - hit.point.y - 2;
            //}

            Vector3 direction = position - controller.transform.position;
            Vector3 newPos = controller.transform.position + (direction.normalized * 15);
            controller.MoveToPosition(1f, newPos, .5f);

            return true;
        }
    }
}