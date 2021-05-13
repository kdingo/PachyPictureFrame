using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PictureFrame : UdonSharpBehaviour {
    public Texture[] pictureList;
    private int[] shuffleOrder;
    public bool shufflePictures = false;
    public float pictureInterval = 15f;
    private float pictureIntervalTime = 0;
    private readonly string screenName = "Screen";
    private MeshRenderer screenRenderer;
    private int pictureIndex = 0;
    private int pictureCount = -1;
    [UdonSynced] private int sync_pictureIndex = 0;

    void Start() {
        screenRenderer = gameObject.transform.Find(screenName).GetComponent<MeshRenderer>();
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
        screenRenderer.material.mainTexture = pictureList[pictureIndex];
        sync_pictureIndex = pictureIndex;
        pictureIntervalTime = Time.time + pictureInterval;
    }

    public override void OnDeserialization() {
        if (sync_pictureIndex != pictureIndex) {
            PictureChangeDeserialize();
        }
    }

    private void PictureChangeDeserialize() {
        pictureIndex = sync_pictureIndex;
        screenRenderer.material.mainTexture = pictureList[pictureIndex];
    }

    private int[] ShuffleList(Texture[] textures) {
        int[] shuffle = new int[textures.Length];
        for (int x = 0; x < textures.Length; x++) {
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
