
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Image;
using VRC.SDKBase;
using VRC.Udon.Common.Interfaces;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class Skybox : UdonSharpBehaviour {
    public GameObject parent;
    public Material skyboxMaterial;
    public VRCUrl[] skyboxUrls = new VRCUrl[6];
    public TextureInfo[] skyboxTexturesInfo = new TextureInfo[6] {
        new TextureInfo() {
            MaterialProperty = "_FrontTex",
            WrapModeU = TextureWrapMode.Clamp,
            WrapModeV = TextureWrapMode.Clamp,
            WrapModeW = TextureWrapMode.Clamp,
        },
        new TextureInfo() {
            MaterialProperty = "_BackTex",
            WrapModeU = TextureWrapMode.Clamp,
            WrapModeV = TextureWrapMode.Clamp,
            WrapModeW = TextureWrapMode.Clamp,
        },
        new TextureInfo() {
            MaterialProperty = "_LeftTex",
            WrapModeU = TextureWrapMode.Clamp,
            WrapModeV = TextureWrapMode.Clamp,
            WrapModeW = TextureWrapMode.Clamp,
        },
        new TextureInfo() {
            MaterialProperty = "_RightTex",
            WrapModeU = TextureWrapMode.Clamp,
            WrapModeV = TextureWrapMode.Clamp,
            WrapModeW = TextureWrapMode.Clamp,
        },
        new TextureInfo() {
            MaterialProperty = "_UpTex",
            WrapModeU = TextureWrapMode.Clamp,
            WrapModeV = TextureWrapMode.Clamp,
            WrapModeW = TextureWrapMode.Clamp,
        },
        new TextureInfo() {
            MaterialProperty = "_DownTex",
            WrapModeU = TextureWrapMode.Clamp,
            WrapModeV = TextureWrapMode.Clamp,
            WrapModeW = TextureWrapMode.Clamp,
        },
    };

    private VRCImageDownloader _imageDownloader;
    private IUdonEventReceiver _udonEventReceiver;
    private int _downloadedCount = 0;

    void Start() {
        if (skyboxUrls.Length != 6) {
            Debug.Log("Skybox URLS must be a length of 6");
            return;
        }

        if (skyboxTexturesInfo.Length != 6) {
            Debug.Log("Skybox Textures Info must be a length of 6");
            return;
        }

        _imageDownloader = new VRCImageDownloader();
        _udonEventReceiver = (IUdonEventReceiver)this;

        for (int i = 0; i < skyboxUrls.Length; i++) {
            var skyboxUrl = skyboxUrls[i];
            var skyboxTexInfo = skyboxTexturesInfo[i];
            _imageDownloader.DownloadImage(skyboxUrl, skyboxMaterial, _udonEventReceiver, skyboxTexInfo);
        }

        RenderSettings.skybox = skyboxMaterial;
    }

    private void Update() {
        if (_downloadedCount == 6) {
            parent.SetActive(false);
        }
    }

    public override void OnImageLoadSuccess(IVRCImageDownload result) {
        Debug.Log($"Image loaded: {result.SizeInMemoryBytes} bytes.");
        _downloadedCount++;
    }

    public override void OnImageLoadError(IVRCImageDownload result) {
        Debug.Log($"Image not loaded: {result.Error}: {result.ErrorMessage}.");
    }

    private void OnDestroy()
    {
        _imageDownloader.Dispose();
    }

    private void OnDisable() {
        _imageDownloader.Dispose();
    }
}
