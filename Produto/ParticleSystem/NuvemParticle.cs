using UnityEngine;
using System.Collections;
using System;
using System.Threading;

public class NuvemParticle : MonoBehaviour {
    public ParticleSystem thunder;
    public Light light;
    public AudioClip thunderSound;
    private float _intensityLightDefault, _intensityLight;
    private AudioSource _audio;
    private bool isAudioPlaying;

    void Awake() {
        if (this.light == null)
            throw new Exception("Light was not found");
        else if (this.thunder == null)
            throw new Exception("Thunder Particle was not found");
        else if (this.thunderSound == null)
            throw new Exception("Thunder Sound was not found");

        this._intensityLightDefault = this.light.intensity;
        this._intensityLight = this._intensityLightDefault;
        _audio = this.GetComponent<AudioSource>();
        if (_audio == null)
            throw new Exception("Audio Source was not found");

        _audio.clip = thunderSound;
    }

    void Start() {
        InvokeRepeating("playThunder", 0, 10.0f);
    }

    internal void playThunder() {
        thunder.gameObject.SetActive(true);
        thunder.Play();
    }

    internal void playThunderEffect() {
        if(!_audio.isPlaying)
            _audio.Play();
    }

    void Update() {
        if (thunder.isPlaying) {
            playThunderEffect();
            light.intensity = Mathf.Lerp(_intensityLightDefault, 2.5f, Time.deltaTime);
        } else {
            light.intensity = _intensityLightDefault;
        }
    }
}
