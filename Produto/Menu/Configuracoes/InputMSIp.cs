using UnityEngine;
using System.Collections;
using GUIHelper;

public class InputMSIp : GUITextCreator {
    public Rect textRect;
    public GUIStyle txtStyle;
    public Texture2D inputBg;

    public override void onGUI() {
        if (!Configuration.GetUseUnityMasterServer()) {
            this.Settings();

            txtStyle.font = Style.font;
            txtStyle.alignment = Style.alignment;
            txtStyle.fontSize = Style.fontSize;
            txtStyle.normal.background = inputBg;
            txtStyle.border = new RectOffset(3, 3, 3, 3);
            txtStyle.normal.textColor = Style.normal.textColor;
            txtStyle.padding = new RectOffset(5, 0, 0, 0);

            Configuration.ChangeMasterServerIp(
                GUI.TextField(
                        base.Canvas,
                        string.IsNullOrEmpty(Configuration.GetMasterServerIp()) ? string.Empty : Configuration.GetMasterServerIp(),
                        txtStyle
                    )
                );
        }
    }
}