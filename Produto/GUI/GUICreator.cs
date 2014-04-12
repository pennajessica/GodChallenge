using UnityEngine;
using System.Collections;
using System.ComponentModel;
using System;

namespace GUIHelper {
    public abstract class GUICreator : MonoBehaviour {
        /// <summary>
        /// Screen position helper
        /// </summary>
        public ScreenPosition position;
        /// <summary>
        /// The Canvas to create a Texture or Text.
        /// </summary>
        public Rect Canvas { get; set; }
        /// <summary>
        /// Maximum scale to texture.
        /// This property need to be defined on constructor from this class.
        /// </summary>
        public int MaxScale { get; set; }

        public int MaxFontSize { get; set; }

        public int depth;

        public GUICreator() {
            this.MaxScale = 5;
            this.MaxFontSize = 100;
        }

        /// <summary>
        /// Método obrigatório para qualquer classe que irá herdar desta classe.
        /// </summary>
        protected abstract void createCanvas();

        protected abstract void Draw();

        protected abstract void onClick();

    }
    /// <summary>
    /// Enum to help on the screen position.
    /// </summary>
    public enum ScreenPosition {
        [Description("Canto superior esquerdo")]
        TopLeft,
        [Description("Canto superior central")]
        TopCenter,
        [Description("Canto superior direito")]
        TopRight,
        [Description("Canto central esquerdo")]
        CenterLeft,
        [Description("Centro da tela")]
        Center,
        [Description("Canto central direito")]
        CenterRight,
        [Description("Canto inferior esquerdo")]
        BottomLeft,
        [Description("Canto inferior central")]
        BottomCenter,
        [Description("Canto inferior direito")]
        BottomRight
    }

    public enum ButtonType {
        Text,
        Texture
    }
}