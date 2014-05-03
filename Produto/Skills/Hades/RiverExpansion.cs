using UnityEngine;
using System.Collections;
using System.Threading;

namespace GodChallenge.Skills.Hades {
    public class RiverExpansion : SkillBehaviour {
        public GameObject particle;
        private SphereCollider sphereCollider;
        private float raio = 0;

        void Awake() {
            this.sphereCollider = this.GetComponent<SphereCollider>();
        }

        void Start() {

			transform.rotation = Quaternion.identity;
			StartCoroutine (Tools.WaitSeconds (WaitAnimationTime, () => {
				ReadyToStart = true;
				transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
								for (int x = 0; x < 25; x++) {
										GameObject obj = Instantiate (particle, this.transform.position, this.transform.rotation) as GameObject;
										obj.transform.parent = this.transform;
								}
								new Thread (() => {
										while (true) {
												Thread.Sleep (200);
												raio += 1f;
										}
								}).Start ();
						}));
        }
        void Update() {
            this.sphereCollider.radius = this.raio;
        }
    }
}