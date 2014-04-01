using UnityEngine;
using System.Collections;
using GUIHelper;
using System;

public class ConfigMasterVolume : GUITextCreator {
    private float masterVolume = 1;
    public Rect sliderRect;
    //public float x, y;

    void Start() {
        sliderRect = new Rect();
        sliderRect.width = 200;
        sliderRect.height = 10;
    }

    public override void onGUI() {
        base.onGUI();
        float left = (Screen.width / 2) - (sliderRect.width / 2);
        float top = (Screen.height / 2) - (sliderRect.height / 2) - 45;
        sliderRect = new Rect(left, top, sliderRect.width, sliderRect.height);

        Configuration.ChangeVolume(GUI.HorizontalSlider(sliderRect, Configuration.GetVolume(), 0, 1));

        Rect volumeTextRect = new Rect(sliderRect.xMax + 8, sliderRect.y - 5, 20, 20);
        GUIStyle volumeTxtStyle = base.Style;
        volumeTxtStyle.fontSize -= 10;

        GUI.Label(volumeTextRect, Convert.ToString(Mathf.RoundToInt(Configuration.GetVolume() * 100)), volumeTxtStyle);
    }
}
