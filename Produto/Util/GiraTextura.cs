using UnityEngine;
using System.Collections;

public class GiraTextura : MonoBehaviour {
    public Vector2 scrollSpeed;
    public bool randomize;
    public float delay;

    //void Start() {
    //    if(randomize)
    //        StartCoroutine(Randomize(delay));
    //}

    //IEnumerator Randomize(float delay) {
    //    yield return new WaitForSeconds(delay);
    //    scrollSpeed.x *= -1;
    //    StartCoroutine(Randomize(delay));
    //}
    

    void Update() {

        Vector2 offset = Time.time * scrollSpeed;
        renderer.material.mainTextureOffset = offset;
    }
}
