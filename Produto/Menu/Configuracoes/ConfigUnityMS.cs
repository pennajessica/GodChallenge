using UnityEngine;
using System.Collections;
using GUIHelper;

public class ConfigUnityMS : GUITextCreator {
    public GUISkin skin;
    public Rect toogleRect;

    public override void onGUI() {
        this.Settings();
        toogleRect = base.Canvas;
        toogleRect.x = base.Canvas.x - 20;
        toogleRect.y = base.Canvas.y;

        Configuration.ChangeUseUnityMasterServer(GUI.Toggle(toogleRect, Configuration.GetUseUnityMasterServer(), base.text, skin.toggle));
        //base.onGUI();
    }
}
