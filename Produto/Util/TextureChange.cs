using UnityEngine;
using System.Collections;

public class TextureChange : MonoBehaviour {

    public Texture[] textures;
    public Texture defaultTexture;
    public Material material;
    public float delay;
    private int actualIndex = 0;
    private bool increase = true;
    IEnumerator ChangeTextureIndex(float delay) {

        if (actualIndex == 0) {
            increase = true;
        } else if (actualIndex == textures.Length - 1) {
            increase = false;
        }

        if (increase)
            actualIndex++;
        else
            actualIndex--;
        yield return new WaitForSeconds(delay);

        StartCoroutine(ChangeTextureIndex(delay));
    }

    void Awake() {
        if (textures == null || textures.Length == 0 || !defaultTexture || !material)
            throw new System.Exception("Textures, Default Texture and Material is needed!!");
    }

    void Start() {
        StartCoroutine(ChangeTextureIndex(delay));
    }

    void OnDisable() {
        this.material.SetTexture(0, defaultTexture);
    }

	void Update () {
        if (textures[actualIndex] != this.material.mainTexture)
            this.material.mainTexture = textures[actualIndex];

	}
}
