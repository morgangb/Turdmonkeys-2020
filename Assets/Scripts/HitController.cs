using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    [SerializeField] private float maxHP = 10f;
    [SerializeField] private bool immortal = false;
    [SerializeField] private bool bulletImmune = false;
    [SerializeField] private bool laserImmune = false;
    private float curHP;

    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        curHP = maxHP;
        if(curHP <= 0f) { Destroy(this); }
    }

    public void takeHit(float dmg, bool hold) {
        if(!bulletImmune && !laserImmune || bulletImmune && hold || laserImmune && !hold) { curHP -= dmg; }
    }
}
