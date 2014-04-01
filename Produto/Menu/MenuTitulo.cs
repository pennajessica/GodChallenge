using UnityEngine;
using System.Collections;
using GUIHelper;

public class MenuTitulo : GUITextureCreator {

    public bool goUp = false;
    public float velocidade;

    void Update() {
        if (goUp) {
            positionAndScale.y = Mathf.Lerp(positionAndScale.y, -150, Time.deltaTime * velocidade);
        }
    }

    protected override void onClick() {
        throw new System.NotImplementedException();
    }
}
