using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PictureFrame : UdonSharpBehaviour {
    public Texture[] pictureList;
    public bool randomOrder = false;
    public float pictureInterval = 15f;
    private float pictureIntervalTime = 0;
    private readonly string screenName = "Screen";
    private MeshRenderer screenRenderer;
    private int pictureIndex = -1;
    [UdonSynced] private int sync_pictureIndex = -1;

    void Start() {
        screenRenderer = gameObject.transform.Find(screenName).GetComponent<MeshRenderer>();
        if (Networking.IsMaster) {
            pictureIntervalTime = Time.time + pictureInterval;
            pictureChange();
        }
    }

    private void Update() {
        if (Time.time >= pictureIntervalTime && Networking.IsMaster) {
            pictureChange();
        }
    }

    public void pictureChange() {
        pictureIndex = nextPictureIndex();
        sync_pictureIndex = pictureIndex;
        screenRenderer.material.mainTexture = pictureList[pictureIndex];
        pictureIntervalTime = Time.time + pictureInterval;
    }

    private int nextPictureIndex() {
        if (randomOrder) {
            return Random.Range(0, pictureList.Length);
        }
        else {
            if(++pictureIndex >= pictureList.Length) {
                pictureIndex = 0;
            }
        }
        return pictureIndex;
    }

    public override void OnDeserialization() {
        if (sync_pictureIndex != pictureIndex) {
            pictureChangeDeserialize();
        }
    }

    private void pictureChangeDeserialize() {
        pictureIndex = sync_pictureIndex;
        screenRenderer.material.mainTexture = pictureList[pictureIndex];
    }
}
