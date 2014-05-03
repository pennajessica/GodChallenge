using UnityEngine;
using System.Collections;

public class NetworkMenu : MonoBehaviour {
    public GameObject[] children;
    public GameObject titulo;

    public void ShowNetworkMenu() {
        StartCoroutine(Tools.WaitSeconds(5, () => {
            foreach (var child in children) {
                child.SetActive(true);
            }
            titulo.SetActive(false);
        }));
    }
}
