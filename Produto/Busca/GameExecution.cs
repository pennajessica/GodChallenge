using UnityEngine;
using System.Collections;
using Buscas.Grafos;
using System.Collections.Generic;
using Buscas;

public class GameExecution {
    Grafo grafo;
    GameObject[] gameObjectsNodos;
    List<Nodo> nodos;
    public void criaNodos(Nodo origem, Nodo destino) {
        nodos = new List<Nodo>();
        nodos.Add(origem);
        gameObjectsNodos = GameObject.FindGameObjectsWithTag("Nodo");
        foreach (var nodo in gameObjectsNodos) {
            Nodo aux = nodo.GetComponent<Nodo>();
            nodos.Add(aux);
        }
        nodos.Add(destino);
        grafo = new Grafo(nodos.ToArray());
    }
    
    public List<Nodo> buscaAStar(Nodo origem, Nodo destino) {
        MetodosDeBusca busca = new MetodosDeBusca();
        busca.AStar(grafo, origem, destino);
        //busca.mostraOrdemVisitacao();
        return busca.Solucao;
    }
}
