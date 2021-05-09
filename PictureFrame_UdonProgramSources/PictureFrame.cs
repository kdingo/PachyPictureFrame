using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PictureFrame : UdonSharpBehaviour {
    public Texture[] pictureList;
    public bool randomOrder = false;
    public float pictureInterval = 15f;
    private float pictureIntervalTime = 0;
    private string screenName = "Screen";
    private MeshRenderer screenRenderer;
    private int pictureIndex = 0;

    void Start() {
        screenRenderer = gameObject.transform.Find(screenName).GetComponent<MeshRenderer>();
        pictureIntervalTime = Time.time + pictureInterval;
    }

    private void Update() {
        if (Time.time >= pictureIntervalTime) {
            pictureChange();
        }
    }

    public void pictureChange() {
        pictureIndex = nextPictureIndex();
        screenRenderer.material.mainTexture = pictureList[pictureIndex];
        pictureIntervalTime = Time.time + pictureInterval;
    }

    private int nextPictureIndex() {
        if (randomOrder) {
            return Random.Range(0, pictureList.Length - 1);
        }
        else {
            if(++pictureIndex >= pictureList.Length) {
                pictureIndex = 0;
            }
        }
        return pictureIndex;
    }

}
