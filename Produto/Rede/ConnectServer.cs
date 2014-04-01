using UnityEngine;
using System.Collections;
using GUIHelper;

public class ConnectServer : GUIButtonCreator {
    
    public Texture2D hoverImage;
    private Texture2D defaultImage;
    public GameObject createServerButton;
    public GameObject connectServerButton;

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
        this.createServerButton.SetActive(false);
        this.connectServerButton.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
