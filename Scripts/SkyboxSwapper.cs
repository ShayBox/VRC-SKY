
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
            self.InteractionText = $"Click to Swap Skybox ({_skyboxIndex}/{skyboxes.Length}) ({skybox.name})";
        }
    }

    public override void Interact() {
        var skybox = skyboxes[_skyboxIndex];
        if (!skybox.activeSelf) {
            // Disable the old skybox
            skybox.SetActive(false);

            // Increment or loop back skybox index
            _skyboxIndex = (_skyboxIndex + 1) % skyboxes.Length;

            // Enable the new skybox
            skyboxes[_skyboxIndex].SetActive(true);

            // Set the interaction text
            self.InteractionText = "Loading...";
        } else {
            // Why not
            switch (self.InteractionText) {
                case "Loading...":
                    self.InteractionText = "Loading... please wait...";
                    break;

                case "Loading... please wait...":
                    self.InteractionText = "Still loading... please wait longer...";
                    break;

                case "Still loading... please wait longer...":
                    self.InteractionText = "You need to wait...";
                    break;

                case "You need to wait...":
                    self.InteractionText = "Maybe you don't have tooltips enabled...";
                    break;
            }
            
        }
    }
}
