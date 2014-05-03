using UnityEngine;
using System.Collections;
using System;
using System.Linq;

namespace GodChallenge.Domain.Skills.BlinkSkill {
    
    [System.Serializable]
    public class BlinkSkill : BaseSkill {

        public override bool CastSkill(RTSController controller) {
            if (!base.CastSkill(controller))
                return false;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] raycastHits = Physics.RaycastAll(ray);
            RaycastHit hit;

            try {
                hit = raycastHits.Where(linq => linq.transform.gameObject.tag == "Terrain").Last();
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

    }

}