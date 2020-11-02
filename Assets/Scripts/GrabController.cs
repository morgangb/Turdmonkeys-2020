using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    private CharacterController myCharacter;
    
    public GameObject grabber;

    // Start is called before the first frame update
    void Start()
    {
        myCharacter = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move when grabbed
        if (grabber) {
            Vector3 inpVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            inpVec = Quaternion.LookRotation(transform.TransformDirection(Vector3.forward), Vector3.up) * inpVec;
            myCharacter.Move(inpVec * moveSpeed * Time.deltaTime);
        }
    }
}
