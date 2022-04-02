using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSwitcher : MonoBehaviour
{
    public GameObject freeCamBrain;
    public PlayeController controller;
    public BuildingSystem buildSystem;

    public void SwitchToPlayer(){
        freeCamBrain.SetActive(false);
        controller.ChangeMovementState(true);
        buildSystem.ChangeBuildingState(false);
    }

    public void SwitchToBuild(){
        freeCamBrain.SetActive(true);
        controller.ChangeMovementState(false);
        buildSystem.ChangeBuildingState(true);
    }
}
