using UnityEngine;
using System.Collections;
namespace GUIHelper {
    public abstract class GUITextCreator : GUICreator {
        public Rect TextCanvas;
        public string text;
        public Font font;
        public Color fontColor, fontBackgroundColor;
        public int fontSize;
        public RectOffset padding;
        public TextAnchor alignInCanvas;
        public FontStyle fontStyle;
        public float lineHeight;
        public bool wordWrap;
        public Texture2D background;
        public GUIStyle Style { get; set; }

        void Awake() {
            this.Style = new GUIStyle();
        }

        void Update() {
            fontSize = Mathf.Clamp(fontSize, 1, base.MaxFontSize);
        }

        protected override void createCanvas() {
            float left = 0, top = 0, width = this.TextCanvas.width, height = this.TextCanvas.height;
            switch (base.position) {
                case ScreenPosition.TopLeft:
                    left = 0 + this.TextCanvas.x;
                    top = 0 + this.TextCanvas.y;
                    break;
                case ScreenPosition.TopCenter:
                    left = (Screen.width / 2) - (width / 2) + this.TextCanvas.x;
                    top = 0 + this.TextCanvas.y;
                    break;
                case ScreenPosition.TopRight:
                    left = Screen.width - width + this.TextCanvas.x;
                    top = 0 + this.TextCanvas.y;
                    break;
                case ScreenPosition.CenterLeft:
                    left = 0 + this.TextCanvas.x;
                    top = (Screen.height / 2) - (height / 2) + this.TextCanvas.y;
                    break;
                case ScreenPosition.Center:
                    left = (Screen.width / 2) - (width / 2) + this.TextCanvas.x;
                    top = (Screen.height / 2) - (height / 2) + this.TextCanvas.y;
                    break;
                case ScreenPosition.CenterRight:
                    left = Screen.width - width + this.TextCanvas.x;
                    top = (Screen.height / 2) - (height / 2) + this.TextCanvas.y;
                    break;
                case ScreenPosition.BottomLeft:
                    left = 0 + this.TextCanvas.x;
                    top = Screen.height - height + this.TextCanvas.y;
                    break;
                case ScreenPosition.BottomCenter:
                    left = (Screen.width / 2) - (width / 2) + this.TextCanvas.x;
                    top = Screen.height - height + this.TextCanvas.y;
                    break;
                case ScreenPosition.BottomRight:
                    left = Screen.width - width + this.TextCanvas.x;
                    top = Screen.height - height + this.TextCanvas.y;
                    break;
                default:
                    break;
            }
            base.Canvas = new Rect(left, top, width, height);
        }

        void OnGUI() {
            onGUI();
        }

        public virtual void onGUI() {
            this.Draw();
        }

        protected override void Draw() {
            settings();

            GUI.Label(base.Canvas, this.text, this.Style);
            this.Style.normal.textColor = this.fontColor;
            this.Style.fontSize = this.fontSize;
            GUI.Label(base.Canvas, this.text, this.Style);
        }

        private void settings() {
            GUI.depth = base.depth;
            this.createCanvas();

            this.Style.font = this.font;

            this.Style.fontStyle = this.fontStyle;

            this.Style.padding = this.padding;

            this.Style.normal.textColor = this.fontBackgroundColor;

            this.Style.normal.background = this.background;

            this.Style.wordWrap = this.wordWrap;

            this.Style.alignment = this.alignInCanvas;

            this.Style.fontSize = this.fontSize;

        }


        protected override void onClick() {
            throw new System.NotImplementedException();
        }
    }
}