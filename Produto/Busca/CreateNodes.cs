using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Buscas.Grafos {
    public class CreateNodes : MonoBehaviour {

        public string name;
        public List<Nodo> listaNodos = new List<Nodo>();
        public float raio = 1.0f;
        float distancia = 0f;
        public int sizeX = 0;
        public int sizeZ = 0;
        public Transform prefabNodo;
        public enum Opcao {
            ESTATICO,
            GERAR_PONTOS
        }
        public Opcao opcao = Opcao.ESTATICO;
        public enum Ligar {
            NAO_LIGA,
            LIGA
        }
        public Ligar ligar = Ligar.NAO_LIGA;

        void OnDrawGizmosSelected() {
            if (opcao == Opcao.GERAR_PONTOS) {
                opcao = Opcao.ESTATICO;
                listaNodos.Clear();
                float z = 0;
                int cont = 0;
                for (int i = 0; i < sizeZ; i++) {
                    float x = 0;
                    for (int j = 0; j < sizeX; j++) {
                        Transform obj = Instantiate(prefabNodo) as Transform;
                        obj.transform.position = new Vector3(this.transform.position.x + x, 0, this.transform.position.z + z);
                        obj.gameObject.name = string.Format("[ P{0} - {1} ]", ++cont, name);
                        Nodo node = obj.gameObject.GetComponent<Nodo>();
                        node.Id = cont;
                        obj.parent = transform;
                        x += raio;
                    }
                    z += raio;
                }
            }

            if (ligar == Ligar.LIGA) {
                ligar = Ligar.NAO_LIGA;
                float raioRaizDe2 = raio * 1.415f;
                List<Nodo> listaTemp = new List<Nodo>();
                Nodo[] filhos = GetComponentsInChildren<Nodo>();
                for (int i = 0; i < filhos.Length; i++) {
                    listaTemp.Clear();
                    for (int j = 0; j < filhos.Length; j++) {
                        if (i != j) {
                            distancia = Vector3.Distance(filhos[i].transform.position, filhos[j].transform.position);
                            if (distancia < raioRaizDe2) {
                                listaTemp.Add(filhos[j]);
                            }
                        }
                    }
                    filhos[i].ListaAdj = listaTemp;
                    filhos[i].adjacentes = listaTemp.ToArray();
                }
            }
        }

    }
}