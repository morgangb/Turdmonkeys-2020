using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    [SerializeField] private bool stayTriggered = true;
    private bool triggered = false;
    private Collider triggerer;

    void Update() {
        if(GetComponent<HitController>()) { GetComponent<HitController>().hit = triggered; }

        if(GetComponentInParent<enemyController>()) { GetComponentInParent<enemyController>().notice(triggerer); }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            triggered = true;
            triggerer = other;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player" && !stayTriggered) {
            triggered = false;
            triggerer = null;      
        }
    }
}
