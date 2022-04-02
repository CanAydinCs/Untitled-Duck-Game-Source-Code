using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    public GameObject floor;

    public bool canPlace;

    private Camera _mainCamera;

    private GameObject floatingPart;

    private CharacterActions actions;

    private InputAction mousePosition;

    public Collider hitCollider;

    private Ray ray;
    private RaycastHit hit;

    private Vector3 oldRotation;

    private bool canBuild = true;

    private void Awake() {
        actions = new CharacterActions();

        _mainCamera = Camera.main;

        canPlace = true;

        oldRotation = new Vector3();
    }

    private void Update() {
        if(!canBuild)
            return;

        if(floatingPart == null){
            floatingPart = Instantiate(floor);
            floatingPart.transform.eulerAngles = oldRotation;
        }
        
        HandleRaycast();
    }

    private void HandleRaycast(){
        ray = _mainCamera.ScreenPointToRay(mousePosition.ReadValue<Vector2>());
        if(Physics.Raycast(ray,out hit) && hit.collider == hitCollider){
            floatingPart.transform.position = hit.point;
        }
    }

    private void Place(){
        FloorSystem floorSystem = floatingPart.GetComponent<FloorSystem>();

        if(!floorSystem.GetIsTrigger()){
            floorSystem.ChangeTrigger();
            floatingPart = null;
        }
    }

    private void Rotate(){
        oldRotation.z += 45;

        if(oldRotation.z >= 360){
            oldRotation.z -= 360;
        }

        floatingPart.transform.eulerAngles = oldRotation;
    }

    public void ChangeBuildingState(bool value){
        canBuild = value;
    }

    private void OnEnable() {
        actions.Enable();

        mousePosition = actions.Player.PlacePosition;

        actions.Player.Place.performed += _ => Place();
        
        actions.Player.Rotate.performed += _ => Rotate();
    }

    private void OnDisable() {
        actions.Disable();
    }
}
