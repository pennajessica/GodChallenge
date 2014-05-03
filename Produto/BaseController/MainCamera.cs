using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
	void Start () {
        AudioListener.volume = Configuration.GetVolume();
	}
}
