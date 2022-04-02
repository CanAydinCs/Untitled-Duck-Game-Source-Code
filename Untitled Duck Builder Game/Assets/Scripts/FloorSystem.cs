using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSystem : MonoBehaviour
{
    public MeshRenderer[] renderers;

    public Material defaultMat, redMat;

    private bool isTrigger = false;
    private Collider cold;

    private void Awake() {
        cold = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("floor")){
            isTrigger = true;
            foreach(MeshRenderer rend in renderers){
                rend.material = redMat;
            }
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("floor")){
            isTrigger = false;
            foreach(MeshRenderer rend in renderers){
                rend.material = defaultMat;
            }
        }
    }

    public void ChangeTrigger() => cold.isTrigger = false;

    public bool GetIsTrigger() => isTrigger;
}
