using UnityEngine;
using System.Collections;
using GUIHelper;
using System;

public class ConfigPlayerName : GUITextCreator {
    private string name = "";
    public Rect textRect;
    private GUIStyle txtStyle;
    public Texture2D inputBg;
    //public float x, y;

    void Start() {
        textRect = new Rect();
        textRect.width = 240;
        textRect.height = 35;
        txtStyle = new GUIStyle();
    }

    public override void onGUI() {
        base.onGUI();

        float left = (Screen.width / 2) - (textRect.width / 2) - 85;
        float top = (Screen.height / 2) - (textRect.height / 2) + 30;
        textRect = new Rect(left, top, textRect.width, textRect.height);

        txtStyle.font = Style.font;
        txtStyle.alignment = TextAnchor.MiddleLeft;
        txtStyle.fontSize = Style.fontSize - 10;
        txtStyle.normal.background = inputBg;
        txtStyle.border = new RectOffset(3, 3, 3, 3);
        txtStyle.normal.textColor = Style.normal.textColor;
        txtStyle.padding = new RectOffset(5, 0, 0, 0);

        Configuration.ChangePlayerName(
            GUI.TextField(
                textRect,
                (String.IsNullOrEmpty(Configuration.GetPlayerName()) ? String.Empty : Configuration.GetPlayerName().Replace("\n", "")),
                15,
                txtStyle
            )
        );
    }

}
