using UnityEngine;
using System.Collections;
using GUIHelper;
using GodChallenge.Match;

public class HudMiniMap : GUITextureCreator {
    private Scene scene;
    private bool isOffline;

    internal void Awake() {
        this.isOffline = (Application.loadedLevelName == "Tutorial");
        if (networkView.isMine || this.isOffline) {
            scene = GameMatch.getSceneInfo(Application.loadedLevel);
            base.texture = scene.Hud[1];
            base.positionAndScale = scene.positionAndScale;

            base.createCanvas();
        }
    }

    protected override void onClick() {
        throw new System.NotImplementedException();
    }
}
