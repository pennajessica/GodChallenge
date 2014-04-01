using UnityEngine;
using System.Collections;
using GUIHelper;

public class MenuBack : GUIButtonCreator {

    public Texture2D hoverImage;
    private Texture2D defaultImage;
    public GameObject[] back;

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
        this.transform.parent.gameObject.SetActive(false);
        foreach (GameObject item in back) {
            item.SetActiveRecursively(true);
        }
    }
}
