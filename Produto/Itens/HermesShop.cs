using UnityEngine;
using System.Collections;
using GodChallenge.Domain;
using System.Collections.Generic;

public class HermesShop : MonoBehaviour {
    public Shop shop;
    private List<RTSController> controllers;
    public Texture background;
    public Rect positionAndScale = new Rect() { height = 1, width = 1 };
    public float left1, top1, width, height;
    public Vector2 shopScroll;
    public float scaleIcon = 0.25f;
    public Font font;
    public int fontSize;
    public Color fontColor;
    private bool canShowShop = false;
    private RTSController actualController;

    void Awake() {
        controllers = new List<RTSController>();
        shop = new Shop();
    }

    public void AddPlayerOnQueue(RTSController control) {
        this.controllers.Add(control);
        control.shopIndex = this.controllers.Count - 1;
    }

    public void ShowShop(RTSController control) {
        canShowShop = true;
        actualController = control;
    }

    void OnGUI() {
        if (canShowShop) {
            if (background) {
                float left = (Screen.width / 2) - ((background.width * this.positionAndScale.width) / 2);
                float top = (Screen.height / 2) - ((background.height * this.positionAndScale.height) / 2);

                Rect BackgroundCanvas = new Rect(left + positionAndScale.x, top + positionAndScale.y, background.width * this.positionAndScale.width, background.height * this.positionAndScale.height);

                GUI.depth = -100;
                GUI.DrawTexture(BackgroundCanvas, background);

                Rect BoxCanvas = BackgroundCanvas;
                BoxCanvas.xMin += 155;
                BoxCanvas.yMin += 180;
                BoxCanvas.width += -150;
                BoxCanvas.height += -165;

                Rect nomeShop = BoxCanvas;
                nomeShop.yMin += -103;

                GUIStyle nomeShopStyle = new GUIStyle();
                nomeShopStyle.font = font;
                nomeShopStyle.fontSize = fontSize + 10;
                nomeShopStyle.normal.textColor = fontColor;
                nomeShopStyle.alignment = TextAnchor.UpperCenter;
                nomeShopStyle.fontStyle = FontStyle.Bold;

                GUI.Label(nomeShop, "Loja do Hermes", nomeShopStyle);

                GUI.Box(BoxCanvas, "");
                shopScroll = GUI.BeginScrollView(BoxCanvas, shopScroll, new Rect(0, 0, BoxCanvas.width - 16, 580));
                int x = 0;

                foreach (var item in shop.itens) {
                    Rect iconBox = new Rect(0, (80 + 15) * x, BoxCanvas.width, 80);

                    GUI.Box(iconBox, "");

                    Rect iconName = iconBox;
                    GUIStyle nameStyle = new GUIStyle();
                    nameStyle.font = font;
                    nameStyle.fontSize = fontSize + 5;
                    nameStyle.normal.textColor = fontColor;
                    nameStyle.alignment = TextAnchor.UpperCenter;

                    GUI.Label(iconName, item.Nome, nameStyle);

                    Rect iconRect = new Rect(5, ((item.Icone.height * scaleIcon) + 45) * x + 20, item.Icone.width * scaleIcon, item.Icone.height * scaleIcon);

                    GUI.DrawTexture(iconRect, item.Icone);

                    Rect iconDesc = iconRect;
                    iconDesc.xMin = iconRect.xMax + 15;
                    iconDesc.width = BoxCanvas.width - iconRect.width;

                    GUIStyle descStyle = nameStyle;
                    descStyle.alignment = TextAnchor.UpperLeft;
                    descStyle.fontSize = fontSize;

                    GUI.Label(iconDesc, item.Descricao, descStyle);

                    Rect iconBuy = new Rect(iconBox.width - 120, (iconBox.height - 35) + iconBox.yMin, 100, 30);
                    if (GUI.Button(iconBuy, "Comprar")) {
                        if (shop.BuyItem(actualController.GetPlayer, x)) {
                            Debug.Log("Item comprado!");
                        } else {
                            Debug.Log("Dinheiro insuficiente!");
                        }
                    }
                    Rect iconPrice = iconBuy;
                    iconPrice.xMin += -100;
                    iconPrice.yMin += 10;

                    GUIStyle priceStyle = descStyle;
                    GUI.Label(iconPrice, string.Concat(item.Custo, " Dracma(s)"), priceStyle);

                    x++;
                }
                GUI.EndGroup();

                Rect fecharRect = new Rect(BoxCanvas.xMax - 72, BoxCanvas.yMin - 22, width, height);
                GUIStyle fecharStyle = nomeShopStyle;
                fecharStyle.fontSize -= 5;
                fecharStyle.fontStyle = FontStyle.Normal;

                if(GUI.Button(fecharRect, "Fechar", fecharStyle)) {
                    this.canShowShop = false;
                }

                Rect dracmas = fecharRect;
                dracmas.xMin = BoxCanvas.xMin + 60;
                dracmas.width = 70;
                dracmas.height = 30;
                GUIStyle dracmasStyle = fecharStyle;
                GUIContent content = new GUIContent();
                content.text = string.Format("Meus dracmas: {0}", actualController.GetPlayer.Gold);

                GUI.Label(dracmas, content, dracmasStyle);
            }
        }
    }
}
