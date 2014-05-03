using UnityEngine;
using System.Collections;

public class FountainEffect : MonoBehaviour {
    private GameObject fountainEffect;
    public GameObject effect;

    internal void Awake() {
        SM_prefabGenerator pref = effect.GetComponent<SM_prefabGenerator>();
        if (pref != null) {
            pref.thisManyTimes = 10;
            pref.xWidth = 4;
        }
    }

	void Update () {
        if (fountainEffect == null) {

            fountainEffect = GameObject.Instantiate(effect, this.transform.position, this.transform.rotation) as GameObject;
            fountainEffect.transform.parent = this.transform;

            //this.Destroy(gameObject, fountainEffect);
            GameObject.Destroy(fountainEffect, 3);
        }
	}
}
