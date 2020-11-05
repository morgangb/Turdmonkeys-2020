using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleObjScript : MonoBehaviour
{
    [SerializeField] private HitController[] activators;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool activated = false;

        foreach(HitController activator in activators) {
            if(!activator.hit) {
                activated = true;
                break;
            }
        }

        gameObject.SetActive(activated);
    }
}
