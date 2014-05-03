using UnityEngine;
using System.Collections;

public class Bezier : MonoBehaviour {
    public bool stop = true;
    public static bool acabou = false;
    public GameObject[] pontos;
    public GameObject obj;
    public GameObject Target;
    public float velocidade;
    public bool drawGizmos = true;
    public int X {
        get {
            return this.x;
        }
    }
    int[][] constantes = {
                             new int[1],
                             new int[2],
                             new int[3],
                             new int[4],
                             new int[5],
                             new int[6],
                             new int[7],
                             new int[8],
                             new int[9],
                             new int[10]
                         };
    int x;
    float u = 0;
    float distance;
    void preencheMatriz() {
        for (int x = 0; x < constantes.Length; x++) {
            for (int y = 0; y < constantes[x].Length; y++) {
                if (y == 0) {
                    constantes[x][y] = 1;
                } else if (y == x) {
                    constantes[x][y] = 1;
                } else {
                    constantes[x][y] = constantes[x - 1][y] + constantes[x - 1][y - 1];
                }
            }
        }
    }
    Vector3 Pu, PuAntes;
    public Vector3 calculaBezier(float u, int pos, int x, GameObject[] pontos) {
        Vector3 bezier;
        float a, b;
        int y;
        a = (1 - u);
        b = u;
        y = constantes[x][pos];
        a = Mathf.Pow(a, x - pos);
        b = Mathf.Pow(b, pos);
        bezier = y * a * b * pontos[pos].transform.position;
        if (pos < pontos.Length - 1) {
            pos++;
            return bezier + calculaBezier(u, pos, x, pontos);
        } else {
            return bezier;
        }
    }
    void OnDrawGizmos() {
        if (drawGizmos) {
            this.preencheMatriz();
            int x = pontos.Length - 1;

            for (float u = 0; u < 1; u += 0.01f) {
                Pu = calculaBezier(u, 0, x, pontos);
                if (u == 0.0f) {
                    PuAntes = Pu;
                }
                Gizmos.DrawLine(PuAntes, Pu);
                PuAntes = Pu;
                /*if (u % (u / pontos.Length) == 0) {
                    Debug.Log("Entrei u: " + u + " Pos : " + pos);
                    pos++;
                }*/
            }
        }
    }
    // Use this for initialization
    void Awake() {
        this.x = pontos.Length - 1;
        this.preencheMatriz();
    }
    void stopBezier() {
        stop = true;
        if(obj.GetComponent<Camera>())
            this.obj.camera.depth = -1;

        acabou = true;
    }
    // Update is called once per frame
    void FixedUpdate() {
        if (!stop) {
            distance = Vector3.Distance(obj.transform.position, Target.transform.position);
            if (u <= 1.0f) {
                if (distance > 2) {
                   // tgtMov.transform.position = calculaBezier(u + 0.02f, 0, this.x, pontos);
                    obj.transform.position = calculaBezier(u, 0, this.x, pontos);
                    obj.transform.LookAt(Target.transform);
                    u += 0.01f * (Time.deltaTime * velocidade);
                } else {
                    Invoke("stopBezier", 2);
                }
            }
        }
        // Gizmos.DrawLine(PuAntes, Pu);
        //PuAntes = Pu;
    }
}
