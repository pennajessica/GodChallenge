﻿using UnityEngine;
using System.Collections;
using GUIHelper;

public class ConfigBtnSave : GUIButtonCreator {
    public GameObject menuConfig;
    public GameObject menuInner;
    public GameObject titulo;
    

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
        DoSave();
        menuConfig.SetActive(false);
        menuInner.SetActive(true);
        titulo.SetActive(true);
    }

    void DoSave() {
        Util.WriteConfigFile(Configuration.GetVolume(), Configuration.GetPlayerName());
    }
}
