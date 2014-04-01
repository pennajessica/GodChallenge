using UnityEngine;
using System.Collections;
using GUIHelper;

public class PressKeyMenu : GUITextCreator {
    public float from, to;
    public Bezier bezier;
    public MenuTitulo title;
    public Menu menu;

    void Update() {
        fontColor.a = Mathf.Lerp(fontColor.a, to, Time.deltaTime);
        if (fontColor.a >= 0.9f) {
            to = 0;
        } else if (fontColor.a <= 0.5f) {
            to = 1;
        }
        if (Input.anyKey) {
            bezier.stop = false;
            title.goUp = true;
            menu.ShowInnerMenu();
            Destroy(this.gameObject);
        }
    }

}
