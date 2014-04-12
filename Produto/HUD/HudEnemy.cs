using UnityEngine;
using System.Collections;
using GUIHelper;
using GodChallenge.Match;

public class HudEnemy : GUITextureCreator {
    private Scene scene;
    public bool isOffline;

    void Awake() {
        if(!this.isOffline)
            this.isOffline = (Application.loadedLevelName == "Tutorial");

        if (networkView.isMine || this.isOffline) {
            
            if (!this.isOffline)
                scene = GameMatch.getSceneInfo(Application.loadedLevel);
            else
                scene = GameMatch.getSceneInfo(1);

            base.texture = scene.Hud[2];
            base.createCanvas();
        }
    }

    protected override void onClick() {
        throw new System.NotImplementedException();
    }
}
