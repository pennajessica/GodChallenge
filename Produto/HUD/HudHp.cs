using UnityEngine;
using System.Collections;
using GUIHelper;

public class HudHp : GUITextureCreator {

    public Texture2D iconPersonagem;
    public Texture2D preenchimentoHp;
    public RTSController _controller;

    public float x, y;

    void Awake() {
        if (networkView.isMine || this._controller.isOffline) {
            if (this._controller == null)
                Debug.Log("No Controller was found. Please attach that and turn on the game again.");
        }
    }

    void OnGUI() {
        if (networkView.isMine || this._controller.isOffline) {
            Rect rect = base.Canvas;

            rect.x -= 30;
            rect.y += 20;


            GUI.DrawTexture(rect, iconPersonagem);

            base.onGUI();

            Rect preenchimentoRect = base.Canvas;

            preenchimentoRect.y = preenchimentoRect.y - (preenchimentoHp.height * base.positionAndScale.height) / 100f * (this._controller.GetPlayer.hpPercent * 100) + (preenchimentoHp.height * base.positionAndScale.height);


            preenchimentoRect.height *= this._controller.GetPlayer.hpPercent;


            GUI.DrawTextureWithTexCoords(
                preenchimentoRect,
                preenchimentoHp,
                new Rect(0, 0, 1.0f, this._controller.GetPlayer.hpPercent)
            );
        }

    }

    protected override void onClick() {

        throw new System.NotImplementedException();
    }
}
