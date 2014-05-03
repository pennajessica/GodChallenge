using UnityEngine;
using System.Collections;
using System;

public class BotStatusBar : MonoBehaviour {
    public BotBehaviour _bot;
    public Texture2D statusBar, statusBarHp;
    private Camera _mainCamera;

    internal void Awake() {
        this._mainCamera = Camera.main;
        if (this._bot == null)
            Debug.Log("No Controller was found. Please attach that and turn on the game again.");
        else if (this._mainCamera == null)
            Debug.Log("No Main Camera was found. Please attach that and turn on the game again.");
        else if (this.statusBar == null)
            throw new Exception("The StatusBar's background was not found.");
    }

    internal void OnGUI() {
        Vector2 pos = this._mainCamera.WorldToScreenPoint(_bot.transform.position);
        
        // Draw the status bar background
        Rect rect = new Rect(pos.x - (statusBar.width / 2), Screen.height - (pos.y + _bot.height + (statusBar.height / 2)), statusBar.width, statusBar.height);
        GUIStyle style = new GUIStyle();
        style.normal.background = statusBar;
        style.normal.textColor = Color.white;
        
        // Draw hp background
        Rect hpBarRect = new Rect();
        hpBarRect.height = statusBarHp.height;
        hpBarRect.y = rect.y + 25;
        hpBarRect.x = rect.x + 9;

        hpBarRect.width = ((float)statusBarHp.width) * this._bot.GetPlayer.hpPercent;
        
        GUI.DrawTexture(hpBarRect, statusBarHp);
        
        Rect hpLabelRect = new Rect();
        hpLabelRect.width = statusBarHp.width;
        hpLabelRect.height = statusBarHp.height;
        hpLabelRect.y = rect.y + 25;
        hpLabelRect.x = rect.x + 5;
        
        // Draw hp text
        GUIStyle hpStyle = new GUIStyle();
        hpStyle.normal.textColor = Color.black;
        hpStyle.alignment = TextAnchor.MiddleCenter;
        string hpShow = string.Format("{0} / {1}", this._bot.GetPlayer.Hp, this._bot.GetPlayer.HpMax);
        GUI.Label(hpLabelRect, hpShow, hpStyle);

        // Draw the status bar background
        GUI.DrawTexture(rect, statusBar);
        // Draw the player name
        rect.x += 10;
        rect.y += 7;
        rect.width -= 5;
        GUIStyle nameStyle = new GUIStyle();
        nameStyle.normal.textColor = Color.black;
        // Draw the player name
        GUI.Label(rect, _bot.playerName, nameStyle);

    }

}
