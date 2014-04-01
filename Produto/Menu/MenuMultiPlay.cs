using UnityEngine;
using System.Collections;
using GUIHelper;

public class MenuMultiPlay : GUIButtonCreator {
    
    protected override void onMouseOver() {
        base.onMouseOver();
        fontColor.r = 1.0f;
        fontColor.g = 0.752f;
        fontColor.b = 0;
    }

    protected override void onMouseOut() {
        base.onMouseOut();
        fontColor.r = 0.996f;
        fontColor.g = 0.818f;
        fontColor.b = 0.262f;
    }

    protected override void onClick() {
        Application.LoadLevel("Lobby");
    }

}
