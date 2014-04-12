using UnityEngine;
using System.Collections;
using GUIHelper;

public class HudOptions : GUITextureCreator {
    public Font font;
    public int fontSize;
    public Color txtColor;
    public bool showHud = false;
    public float x, y, w, h;
    
    void Start() {
        showHud = false;
    }

    protected override void onGUI() {
        if (showHud) {
            base.onGUI();

            Rect grpRect = Canvas;
            grpRect.xMin += 52;
            grpRect.yMin += 64.54f;
            grpRect.width = 162;
            grpRect.height = 237;

            GUI.BeginGroup(grpRect);
            GUIStyle btnStyle = new GUIStyle();
            btnStyle.font = font;
            btnStyle.fontSize = fontSize;
            btnStyle.normal.textColor = txtColor;
            btnStyle.alignment = TextAnchor.UpperCenter;

            if (GUI.Button(new Rect(0, 0, 162, 30), "Voltar", btnStyle)) {
                this.showHud = !this.showHud;
            }
            if (GUI.Button(new Rect(0, 45, 162, 30), "Opções", btnStyle)) {

            }
            if (GUI.Button(new Rect(0, 90, 162, 30), "Sair", btnStyle)) {
                Application.LoadLevel("Menu");
            }

            GUI.EndGroup();
        }
    }

    protected override void onClick() {
        throw new System.NotImplementedException();
    }
}
