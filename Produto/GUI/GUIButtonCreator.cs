using UnityEngine;
using System.Collections;

namespace GUIHelper {
    public abstract class GUIButtonCreator : GUICreator {
        /// <summary>
        /// Type of button. (Text or Texture)
        /// </summary>
        public ButtonType buttonType;
        /// <summary>
        /// The texture who will be drawn on screen.
        /// </summary>
        public Texture2D texture;
        /// <summary>
        /// The text who will be drawn on screen.
        /// </summary>
        public string text;
        /// <summary>
        /// Rect to manipulate the position and scale from the texture.
        /// If the width or height from this Rect is 0, the texture will not be drawn.
        /// The X and Y will assist the ScreenPostion.position to reallocate the texture to the right position
        /// which you need.
        /// </summary>
        public Rect positionAndScale;

        public bool defaultBorder;

        private bool mouseOver = false;

        public GUIStyle style;

        public Color fontColor;

        void Awake() {
            base.Canvas = new Rect();
            
            if (this.buttonType == ButtonType.Text)
                this.MaxScale = int.MaxValue;

            if(style == null)
                style = new GUIStyle();
        }

        protected override void createCanvas() {
            float left = 0, top = 0, width = 0, height = 0;
            if (texture) {
                width = this.texture.width;
                height = this.texture.height;
            } else {
                width = 1;
                height = 1;
            }

            switch (base.position) {
                case ScreenPosition.TopLeft:
                    left = 0;
                    top = 0;
                    break;
                case ScreenPosition.TopCenter:
                    left = (Screen.width / 2) - ((width * this.positionAndScale.width) / 2);
                    top = 0;
                    break;
                case ScreenPosition.TopRight:
                    left = Screen.width - (width * this.positionAndScale.width);
                    top = 0;
                    break;
                case ScreenPosition.CenterLeft:
                    left = 0;
                    top = (Screen.height / 2) - ((height * this.positionAndScale.height) / 2);
                    break;
                case ScreenPosition.Center:
                    left = (Screen.width / 2) - ((width * this.positionAndScale.width) / 2);
                    top = (Screen.height / 2) - ((height * this.positionAndScale.height) / 2);
                    break;
                case ScreenPosition.CenterRight:
                    left = Screen.width - width * this.positionAndScale.width;
                    top = (Screen.height / 2) - ((height * this.positionAndScale.height) / 2);
                    break;
                case ScreenPosition.BottomLeft:
                    left = 0;
                    top = Screen.height - (height * this.positionAndScale.height);
                    break;
                case ScreenPosition.BottomCenter:
                    left = (Screen.width / 2) - (width * this.positionAndScale.width / 2);
                    top = Screen.height - (height * this.positionAndScale.height);
                    break;
                case ScreenPosition.BottomRight:
                    left = Screen.width - (width * this.positionAndScale.width);
                    top = Screen.height - (height * this.positionAndScale.height);
                    break;
                default:
                    break;
            }
            base.Canvas = new Rect(left + positionAndScale.x, top + positionAndScale.y, width * this.positionAndScale.width, height * this.positionAndScale.height);
        }

        protected override void Draw() {
            GUI.depth = base.depth;
            this.createCanvas();

            if (this.buttonType == ButtonType.Texture) {
                if (this.texture) {
                    if (defaultBorder) {
                        if (GUI.Button(base.Canvas, texture, style)) {
                            this.onClick();
                        }
                    } else {
                        if (GUI.Button(base.Canvas, texture)) {
                            this.onClick();
                        }
                    }
                }
            } else {
                style.normal.textColor = fontColor;
            
                if (GUI.Button(base.Canvas, this.text, style)) {
                    this.onClick();
                }
            }
        }

        void OnGUI() {
            this.onGUI();
        }

        public virtual void onGUI() {
            this.positionAndScale.width = Mathf.Clamp(this.positionAndScale.width, 0, base.MaxScale);
            this.positionAndScale.height = Mathf.Clamp(this.positionAndScale.height, 0, base.MaxScale);
            this.Draw();

            Vector3 mousePos = Input.mousePosition;
            mousePos.y = Screen.height - mousePos.y;
            if (base.Canvas.Contains(mousePos)) {
                this.mouseOver = true;
                this.onMouseOver();
            } else if (this.mouseOver) {
                this.onMouseOut();
                this.mouseOver = false;
            }
        }

        protected virtual void onMouseOver() {

        }

        protected virtual void onMouseOut() {

        }

    }
}
