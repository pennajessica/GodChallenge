using UnityEngine;
using System.Collections;
using Buscas.Grafos;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Buscas {
    public class MetodosDeBusca {
        Queue<Nodo> fila;
        int time;
        public List<Nodo> Visitacao { get; set; }
        public List<Nodo> Solucao { get; set; }

        #region [ A* Algorithm ]

        // A* Algorithm
        // ---------------------------------------------------------------------------
        // -initialize the open list
        // -initialize the closed list
        // put the starting node on the open list (you can leave its f at zero)
        //
        // while the open list is not empty
        //    find the node with the least f on the open list, call it "q"
        //    pop q off the open list
        //    generate q's 8 successors and set their parents to q
        //    for each successor
        //        if successor is the goal, stop the search
        //        successor.g = q.g + distance between successor and q
        //        successor.h = distance from goal to successor
        //        successor.f = successor.g + successor.h
        //
        //        if a node with the same position as successor is in the OPEN list
        //            which has a lower f than successor, skip this successor
        //        if a node with the same position as successor is in the CLOSED list 
        //            which has a lower f than successor, skip this successor
        //        otherwise, add the node to the open list
        //    end
        //    push q on the closed list
        // end
        // ---------------------------------------------------------------------------

        #endregion [ A* Algorithm ]

        public void AStar(Grafo grafo, Nodo origem, Nodo destino) {
            grafo.nodos = this.limpaNodos(grafo.nodos);
            
            fila = new Queue<Nodo>();
            Visitacao = new List<Nodo>();

            foreach (Nodo nodo in grafo.nodos) {
                if (nodo == origem)
                    continue;

                nodo.TempoInicio = int.MaxValue;
                nodo.predecessor = null;
                nodo.Cor = Cor.Branco;
            }
            origem.Cor = Cor.Cinza;
            origem.TempoInicio = 0;
            origem.predecessor = null;
            // Adiciona o nodo de origem na fila.
            fila.Enqueue(origem);
            while (fila.Count > 0) {
                Nodo u = fila.Dequeue();
                Visitacao.Add(u);
                if (u == destino)
                    break;
                foreach (Nodo v in u.ListaAdj) {
                    if (v.Cor == Cor.Branco) {
                        v.Cor = Cor.Cinza;
                        v.TempoInicio = u.TempoInicio + 1;
                        v.predecessor = u;
                        //fila.enqueueNodeOrdered(v, 'F');
                        fila.Enqueue(v);
                        fila = new Queue<Nodo>(fila.OrderBy(linq => linq.F));
                    }
                }
                u.Cor = Cor.Preto;
            }
            this.fazCaminhoSolucao();
        }
        public void fazCaminhoSolucao() {
            Nodo noFinal = this.Visitacao[this.Visitacao.Count - 1];
            this.Solucao = new List<Nodo>();

            for (Nodo element = noFinal; element != null; element = element.predecessor as Nodo) {
                this.Solucao.Add(element);
            }

            this.Solucao.Reverse();
        }
        public Nodo[] limpaNodos(Nodo[] nodos) {
            List<Nodo> nodoTest = new List<Nodo>();
            foreach (var no in nodos) {
                Nodo node = no;
                node.Cor = Cor.Branco;
                node.predecessor = null;
                nodoTest.Add(node);
            }
            return nodoTest.ToArray();
        }
        /// <summary>
        /// Método que imprimirá a ordem de visitação dos vertices de acordo com o 
        /// último método de busca utilizado.
        /// </summary>
        public void mostraOrdemVisitacao() {
            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < Visitacao.Count; x++) {
                sb.AppendFormat("{0} {1} ", (x == 0 ? "" : "->"), Visitacao[x]);
            }
            Debug.Log(sb.ToString());
        }
    }
}