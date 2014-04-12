using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Buscas.Grafos {
    public class Grafo {
        public Nodo[] nodos { get; set; }
        public Grafo() {
        }
        public Grafo(Nodo[] nodos) {
            this.nodos = nodos;
        }
    }
}
