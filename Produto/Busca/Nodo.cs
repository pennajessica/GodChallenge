using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Buscas.Grafos {
    public class Nodo : MonoBehaviour, INodo {
        #region INodo members

        public float G {
            get {
                return ((this.isNull(predecessor) ? 0.0f : predecessor.G) + this.g);
            }
            set { this.g = value; }
        }

        public float H { get; set; }

        public float F {
            get { return this.G + this.H; }
            set { this.F = value; }
        }

        public INodo predecessor { get; set; }
        #endregion INodo members
        private float g;

        public float CustoNo {
            get {
                return this.g;
            }
        }
        
        public int Id;

        public Cor Cor { get; set; }
        
        public float custo;

        public float heuristica;
        
        public List<Nodo> ListaAdj { get; set; }

        public Nodo[] adjacentes;

        public int TempoInicio { get; set; }

        public int TempoFinal { get; set; }

        public override string ToString() {
            return string.Format("[No]: ID: {0}", this.Id);
            //return string.Format("[No]: ID: {0}, G: {1}, H: {2}, F: {3}", this.Id, this.G, this.H, this.F);
        }

        public bool isNull(object obj) {
            return obj == null;
        }

        void OnDrawGizmos() {
            if (adjacentes != null) {
                foreach (var item in adjacentes) {
                    if(item)
                        Gizmos.DrawLine(this.transform.position, item.transform.position);
                }
            }
        }

        void Update() {
            this.custo = this.g;
            this.heuristica = this.H;
        }

        void Awake() {
            this.ListaAdj = new List<Nodo>();
        }

        void Start() {
            for (int x = 0; x < this.adjacentes.Length; x++) {
                this.ListaAdj.Add(this.adjacentes[x]);
            }
            this.ListaAdj = this.ListaAdj;
        }
    }

    public enum Cor {
        Branco,
        Cinza,
        Preto
    }
}