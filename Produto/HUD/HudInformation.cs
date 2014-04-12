using UnityEngine;
using System.Collections;
using GUIHelper;
using GodChallenge.Match;

public class HudInformation : GUITextureCreator {
    public GUIStyle fontStyle;
    void OnGUI() {
        base.onGUI();
        float startTimeTurn = GameMatch.getStartTurnTime();
        float endTimeTurn = GameMatch.getMaxTurnTime();

        //Debug.Log((endTimeTurn - startTimeTurn) / (Time.time - startTimeTurn) == 2);
        //Debug.Log(System.Math.Round((endTimeTurn - startTimeTurn), 2) / System.Math.Round((Time.time - startTimeTurn), 2));

        if ((endTimeTurn - startTimeTurn) / (Time.time - startTimeTurn) <= 2.00f) {
            fontStyle.normal.textColor = new Color(0.62f, 0.14f, 0.14f);
        }
        

        GUI.Label(base.Canvas, GameMatch.getTimeMatch(), fontStyle);
    }
    protected override void onClick() {
        throw new System.NotImplementedException();
    }
}
