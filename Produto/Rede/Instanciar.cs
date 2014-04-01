using UnityEngine;
using System.Collections;

public class Instanciar : MonoBehaviour {

    public GameObject Spawn;

    public void OnNetworkLoadedLevel() {
        // Instancia o segundo objeto
        var temp = Network.Instantiate(Spawn, transform.position, transform.rotation, 0);
        Debug.Log(temp.name);
    }

}
