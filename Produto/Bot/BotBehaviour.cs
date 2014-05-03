using UnityEngine;
using System.Collections;
using GodChallenge.Domain;
using GodChallenge.Skills;
using System.Threading;
using System;

public class BotBehaviour : MonoBehaviour {

    private Animator _animator;
    private Player _player;
    public float height;
    public string playerName;
    public CharacterType typeBot;

    public Player GetPlayer { 
        get {
            return this._player;
        } 
    }

    void Awake() {
        this._animator = this.GetComponentInChildren<Animator>();
        this._player = SelectionPlayer.getPlayer(this.typeBot.GetHashCode());
        this._player.Controller = this.gameObject;
    }


    
    void OnTriggerEnter(Collider hit) {
        Debug.Log(hit.tag);
        if (hit.gameObject.tag == "Skill") {
            this._player.State = CharacterState.TAKING_HIT;
            SkillBehaviour skillBehaviour = hit.GetComponent<SkillBehaviour>();

            skillBehaviour.skill.ApplySkillEffect(this._player, skillBehaviour.from);

            Destroy(skillBehaviour.gameObject);
            
            float animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;

            new Thread(() => {
                Thread.Sleep(TimeSpan.FromSeconds(animationLength));
                this._player.State = CharacterState.IDLE;
            }).Start();

        }
    }
	
	void Update () {
        Debug.Log(this._player.State);
        this._animator.SetInteger("Action", this._player.State.GetHashCode());
	}
}
