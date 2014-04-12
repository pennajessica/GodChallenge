using UnityEngine;
using System.Collections;
using System;

namespace GUIHelper {

    public abstract class GUITextureCreator : GUICreator {
        /// <summary>
        /// The texture who will be drawn on screen.
        /// </summary>
        public Texture2D texture;
        /// <summary>
        /// Rect to manipulate the position and scale from the texture.
        /// If the width or height from this Rect is 0, the texture will not be drawn.
        /// The X and Y will assist the ScreenPostion.position to reallocate the texture to the right position
        /// which you need.
        /// </summary>
        public Rect positionAndScale = new Rect() { height = 1, width = 1 };

        public bool fullSize;

        //public string MouseDownFunctionName { get; set; }

        //public string MouseUpFunctionName { get; set; }


        void Start() {
        }

        void Awake() {
            base.Canvas = new Rect();
        }

        internal void OnGUI() {
            this.onGUI();
        }

        protected virtual void onGUI() {
            this.positionAndScale.width = Mathf.Clamp(this.positionAndScale.width, 0, base.MaxScale);
            this.positionAndScale.height = Mathf.Clamp(this.positionAndScale.height, 0, base.MaxScale);
            this.Draw();
        }

        protected override void Draw() {
            GUI.depth = base.depth;

            if (this.texture != null) {
                this.createCanvas();
                GUI.DrawTexture(base.Canvas, this.texture);
            }
        }

        public static void Draw(Texture2D tex, ScreenPosition postion, float scaleW, float scaleH) {
            throw new NotImplementedException("Método não implementado!");
        }


        protected override void createCanvas() {
            float left = 0, top = 0, width = 0, height = 0;
            width = this.texture.width;
            height = this.texture.height;

            if (!fullSize) {
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
            } else {
                width = Screen.width;
                height = Screen.height;
            }
            base.Canvas = new Rect(left + positionAndScale.x, top + positionAndScale.y, width * this.positionAndScale.width, height * this.positionAndScale.height);
        }

    }
}