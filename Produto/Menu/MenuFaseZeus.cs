using UnityEngine;
using System.Collections;
using GUIHelper;

public class MenuFaseZeus : GUIButtonCreator {
    public Texture2D hoverImage;
    private Texture2D defaultImage;

    void Awake() {
        this.defaultImage = this.texture;
    }

    protected override void onMouseOver() {
        base.onMouseOver();
        this.texture = hoverImage;
        this.createCanvas();
    }

    protected override void onMouseOut() {
        base.onMouseOut();
        this.texture = this.defaultImage;
        this.createCanvas();
    }

    protected override void onClick() {
        Application.LoadLevel("Zeus");
    }
}
