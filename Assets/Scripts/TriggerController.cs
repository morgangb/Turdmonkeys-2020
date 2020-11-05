using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    [SerializeField] private bool stayTriggered = true;
    private bool triggered = false;

    void Update() {
        if(GetComponent<HitController>()) { GetComponent<HitController>().hit = triggered; }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            triggered = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player" && !stayTriggered) {
            triggered = false;        
        }
    }
}
