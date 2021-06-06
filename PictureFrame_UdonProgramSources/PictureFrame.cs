/* Pachy Picture Frame
 * by pachipon@VRC v20210518
 * ©2021 licensed under CC BY-SA
 */
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PictureFrame : UdonSharpBehaviour {
    public Texture[] pictureList;
    private int[] shuffleOrder;
    public bool shufflePictures = false;
    public float pictureInterval = 15f;
    public Material screenMaterial;
    private float pictureIntervalTime = 0;
    private MeshRenderer screenRenderer;
    private string screenName;
    private int pictureIndex = 0;
    private int pictureCount = -1;
    [UdonSynced] private int sync_pictureIndex = 0;

    void Start() {
        screenName = screenMaterial.name;
        screenRenderer = gameObject.GetComponent<MeshRenderer>();
        shuffleOrder = ShuffleList(pictureList);
        if (Networking.IsMaster) {
            pictureIntervalTime = Time.time + pictureInterval;
            PictureChange();
        }
    }

    private void Update() {
        if (Time.time >= pictureIntervalTime && Networking.IsMaster) {
            PictureChange();
        }
    }

    /// <summary>
    /// Changes the displayed picture. Must be called by World Master only
    /// </summary>
    public void PictureChange() {
        if(++pictureCount >= pictureList.Length) {
            pictureCount = 0;
        }
        if (shufflePictures) {
            pictureIndex = shuffleOrder[pictureCount];
        }
        else {
            pictureIndex = pictureCount;
        }
        sync_pictureIndex = pictureIndex;
        Material m = GetScreenMaterial();
        if (m) { m.mainTexture = pictureList[pictureIndex]; }
        pictureIntervalTime = Time.time + pictureInterval;
    }

    /// <summary>
    /// Cycle through the renderer's materials and get the material for the picture display. Must match with the material set as "Picture Material"
    /// </summary>
    /// <returns>Picture display material</returns>
    private Material GetScreenMaterial() {
        Material[] rendererMaterials = screenRenderer.materials;
        foreach (Material mat in rendererMaterials) {
            if (mat.name.Equals(screenName + " (Instance)")) {
                return mat;
            }
        }
        return null;
    }

    /// <summary>
    /// Check whenever the synced variable is changed by the master
    /// </summary>
    public override void OnDeserialization() {
        if (sync_pictureIndex != pictureIndex) {
            PictureChangeDeserialize();
        }
    }

    /// <summary>
    /// All non-masters will change the picture after the master changes the picture first
    /// </summary>
    private void PictureChangeDeserialize() {
        pictureIndex = sync_pictureIndex;
        Material m = GetScreenMaterial();
        if (m) { m.mainTexture = pictureList[pictureIndex]; }
    }

    /// <summary>
    /// Shuffle the picture list for a shuffled display.
    /// </summary>
    /// <param name="textures">The array of textures to shuffle</param>
    /// <returns>An array of shuffled textures</returns>
    private int[] ShuffleList(Texture[] textures) {
        int[] shuffle = new int[textures.Length];
        for (int x = 0; x < shuffle.Length; x++) {
            shuffle[x] = x;
        }

        int temp;
        for (int i = 0; i < shuffle.Length - 2; i++) {
            temp = shuffle[i];
            int j = Random.Range(i, shuffle.Length);
            shuffle[i] = shuffle[j];
            shuffle[j] = temp;
        }
        return shuffle;
    }
}
