
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class SkyboxSwapper : UdonSharpBehaviour {
    public UdonSharpBehaviour self;
    public GameObject[] skyboxes;

    private int _skyboxIndex = 0;

    private void Update() {
        var skybox = skyboxes[_skyboxIndex];
        if (!skybox.activeSelf) {
            self.InteractionText = $"[{_skyboxIndex}/{skyboxes.Length}] [{skybox.name}] Click to Swap Skybox";
        }
    }

    public override void Interact() {
        var skybox = skyboxes[_skyboxIndex];
        if (skybox.activeSelf && self.InteractionText.EndsWith("Downloading...")) {
            self.InteractionText = "Click again to Skip";
            return;
        }

        // Disable the old skybox
        skybox.SetActive(false);

        // Increment or loop back skybox index
        _skyboxIndex = (_skyboxIndex + 1) % skyboxes.Length;

        // Enable the new skybox
        skyboxes[_skyboxIndex].SetActive(true);

        // Set the interaction text
        self.InteractionText = $"[{_skyboxIndex}/{skyboxes.Length}] [{skybox.name}] Downloading...";
    }
}
