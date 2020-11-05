using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class enemyController : MonoBehaviour
{
    [SerializeField] private float timeToNotice = 5f;
    [SerializeField] private GameObject eyes;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Transform[] patrols;
    [SerializeField] private float coolDownDur = 1f;
    [SerializeField] private float dmg = 1f;
    private float noticeTime;
    private GameObject target;
    private int state;
    private int patrolNode = 0;
    private float coolDown;

    // Start is called before the first frame update
    void Start()
    {
        state = 1;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        switch(state)
        {
            case 1: //Idle
                noticeTime = 0f;

                transform.LookAt(patrols[patrolNode]);
                GetComponent<CharacterController>().Move(transform.TransformDirection(Vector3.forward) * Time.deltaTime * moveSpeed);

                if(Vector3.Distance(transform.position, patrols[patrolNode].position) <= 1f) { patrolNode += 1; }
                if(patrolNode >= patrols.Length) { patrolNode = 0; }
                break;
            case 2: //Noticing
                eyes.transform.LookAt(target.transform);

                if (Physics.Raycast(eyes.transform.position, eyes.transform.TransformDirection(Vector3.forward), out hit, 16) && hit.transform.tag == "Player")
                {
                    noticeTime += Time.deltaTime;
                }
                else {
                    noticeTime -= Time.deltaTime;
                }

                if (noticeTime >= timeToNotice)
                {
                    state = 3;
                }
                else if (noticeTime <= 0f)
                {
                    state = 1;
                }
                break;
            case 3: //Chasing
                transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
                GetComponent<CharacterController>().Move(transform.TransformDirection(Vector3.forward) * Time.deltaTime * moveSpeed);

                coolDown -= Time.deltaTime;

                if(Physics.Raycast(eyes.transform.position, eyes.transform.TransformDirection(Vector3.forward), out hit, 3) && coolDown <= 0f && hit.transform.tag == "Player")
                {
                    hit.transform.GetComponent<TurdmonkeysFirstPersonController>().curHP -= dmg;
                    coolDown = coolDownDur;
                }

                if (!Physics.Raycast(eyes.transform.position, eyes.transform.TransformDirection(Vector3.forward), out hit, 16) && hit.transform.tag == "Player")
                {
                    state = 2;
                }
                break;
            default:
                Debug.Log("STATE NOT RECOGNISED IN ENEMY CONTROLLER");
                break;
        }
    }

    public void notice(Collider toNotice) 
    {
        if (state == 1) {
            target = toNotice.gameObject;
            state = 2;
        }
    }
}
