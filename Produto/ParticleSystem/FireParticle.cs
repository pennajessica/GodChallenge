using UnityEngine;
using System.Collections;

public class FireParticle : MonoBehaviour {

    public Light light;
    private bool crescente = true;
    public float defaultIntensity, defaultRange;
    public float intensidade;
    public float limitIntensity;
    public float limitRange;
    public float time;
    public float range;

    void Start() {
        if (!light)
            throw new System.Exception("Attach the FCKING LIGHT!");

        defaultIntensity = light.intensity;
        defaultRange = light.range;
        intensidade = light.intensity;
    }

	void Update () {
        if (crescente) {
            intensidade += time * Time.deltaTime;
            range += time * Time.deltaTime;
        } else {
            intensidade -= time * Time.deltaTime;
            range -= time * Time.deltaTime;
        }

        if (range > limitRange)
            crescente = false;
        else if (range <= defaultRange)
            crescente = true;


        light.intensity = intensidade;
        light.range = range;
	}

}
