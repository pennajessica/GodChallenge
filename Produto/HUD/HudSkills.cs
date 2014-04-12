using UnityEngine;
using System.Collections;
using GUIHelper;
using System;
using System.Collections.Generic;
using GodChallenge.Domain;
using GodChallenge.Match;
using GodChallenge.Domain.Skills;

public class HudSkills : GUITextureCreator {
    public RTSController controller;
    public Texture2D descriptionBackground;
    public Texture hpBar;
    private List<BaseSkill> _skillList;

    public float x1, y1, w, h;

    private Scene scene;

    public Font font;

    internal void Awake() {
        if (networkView.isMine || this.controller.isOffline) {
            if (controller == null)
                throw new Exception("Controller is needed.");
            
            if(!this.controller.isOffline)
                scene = GameMatch.getSceneInfo(Application.loadedLevel);
            else
                scene = GameMatch.getSceneInfo(1);


            base.depth = 1;
            base.texture = scene.Hud[0];
            base.positionAndScale = scene.positionAndScale;

            base.createCanvas();

            getSkills();
        }
    }

    private void getSkills() {
        _skillList = controller.GetPlayer.SkillList;
        if (_skillList.Count <= 0) {
            Debug.Log("This Player doens't have any skill.");
            return;
        }
    }

    protected override void onGUI() {
        if (networkView.isMine || this.controller.isOffline) {
            base.onGUI();

            Rect rect = base.Canvas;
            rect.y += scene.SkillStartY;
            rect.x += scene.SkillStartX;
            int x = 0;

            #region [ SKILLS ]

            foreach (BaseSkill skill in this._skillList) {
                if (x == 0) {
                    rect.x = rect.x + skill.Icon.width;
                }/* else if (x == this._skillList.Count - 1) {
                    rect.x = rect.x + skill.Icon.width + 20.64f;
                }*/ else {
                    rect.x = rect.x + skill.Icon.width + 11;
                }
                x++;

                rect.width = skill.Icon.width;
                rect.height = skill.Icon.height;
                GUI.depth = -1;
                GUIStyle skillStyle = new GUIStyle();

                if (GUI.Button(rect, skill.Icon, skillStyle)) {
                    if (!this.audio.isPlaying) {
                        this.audio.clip = this.controller.hudSounds[1];
                        this.audio.Play();
                    }
                    Debug.Log("Can Cast " + x + ": " + skill.CastSkill(controller));
                }


                GUI.depth = 1000;
                if (!skill.Ready) {
                    //Rect cdRect = rect;

                    //cdRect.width *= ((Time.time - skill.lastActivation) / ((skill.castingTime + skill.cooldown) / 1000));

                    GUI.DrawTexture(rect, skill.IconInCooldown);
                    float cooldown = skill.LastActivation + ((skill.Cooldown + skill.CastingTime) / 1000) - Time.time;
                    GUIStyle fontStyle = new GUIStyle();
                    fontStyle.font = this.font;
                    fontStyle.fontSize = 35;
                    fontStyle.normal.textColor = Color.black;
                    fontStyle.fontStyle = FontStyle.Bold;
                    fontStyle.alignment = TextAnchor.MiddleCenter;

                    GUI.Label(rect, Convert.ToString(Mathf.RoundToInt(cooldown)), fontStyle);
                }

                Vector3 mousePos = Input.mousePosition;
                mousePos.y = Screen.height - mousePos.y;
                if (rect.Contains(mousePos)) {
                    if (!this.audio.isPlaying) {
                        this.audio.clip = this.controller.hudSounds[2];
                        this.audio.Play();
                    }
                    GUI.depth = -1000;

                    Rect backgroundRect = rect;
                    backgroundRect.y -= descriptionBackground.height;
                    backgroundRect.x -= 10;
                    backgroundRect.width = descriptionBackground.width;
                    backgroundRect.height = (descriptionBackground != null ? descriptionBackground.height : base.Canvas.height);
                    GUI.DrawTexture(backgroundRect, descriptionBackground);

                    Rect descriptionRect = backgroundRect;
                    descriptionRect.width -= 55;
                    descriptionRect.height -= 10;
                    descriptionRect.x += 25;
                    descriptionRect.y += 25;

                    GUIStyle descriptionStyle = new GUIStyle();
                    descriptionStyle.wordWrap = true;
                    descriptionStyle.font = font;
                    string desc = string.Format("Nome: {0}\n", skill.Name);
                    desc += string.Format("Descrição:\n{0}\n", skill.Description);
                    if(skill is IDamageSkill)
                        desc += string.Format("Dano: {0}\n", ((IDamageSkill)skill).Damage);
                    desc += string.Format("Tempo de Recarga: {0} segundos\n", skill.Cooldown / 1000);
                    desc += string.Format("Tempo de Carregamento: {0} segundos", skill.CastingTime / 1000);
                    GUI.Label(descriptionRect, desc, descriptionStyle);
                }
            }
            #endregion [ SKILLS ]

            Rect inventRect = base.Canvas;
            inventRect.xMin += 70;
            inventRect.yMin += 13;
            controller.GetPlayer.Inventario.DrawItens(inventRect);

            Rect hpRect = base.Canvas;
            hpRect.xMin += 120.9f + x1;
            hpRect.yMin += 166.8f + y1;
            hpRect.width = (hpBar.width) * this.controller.GetPlayer.hpPercent;
            hpRect.height = hpBar.height;
            GUI.DrawTexture(hpRect, hpBar);

            Rect hpTxtRect = hpRect;
            hpTxtRect.width = hpBar.width;
            string hpTxt = string.Format("{0} / {1}", controller.GetPlayer.Hp, controller.GetPlayer.HpMax);
            GUIStyle hpStyle = new GUIStyle();
            hpStyle.font = font;
            hpStyle.fontSize = 15;
            hpStyle.normal.textColor = Color.white;
            hpStyle.alignment = TextAnchor.MiddleCenter;

            GUI.Label(hpTxtRect, hpTxt, hpStyle);
        }
    }
    protected override void onClick() {
        throw new System.NotImplementedException();
    }
}
