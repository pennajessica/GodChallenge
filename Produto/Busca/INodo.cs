using UnityEngine;
using System.Collections;

namespace Buscas.Grafos {
    public interface INodo {
        /// <summary>
        /// Custo do Nodo
        /// </summary>
        float G { get; set; }
        /// <summary>
        /// Heuristica do nodo
        /// </summary>
        float H { get; set; }
        /// <summary>
        /// Soma de G + H
        /// </summary>
        float F { get; set; }
        /// <summary>
        /// Nodo predecessor a este.
        /// Caso null, este será o nodo raiz.
        /// </summary>
        INodo predecessor { get; set; }
    }
}
