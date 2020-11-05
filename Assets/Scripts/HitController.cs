﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    [SerializeField] private float maxHP = 10f;
    [SerializeField] private bool immortal = false;
    [SerializeField] private bool[] immunities = new bool[] {false, false, false};
    [SerializeField] private float curHP;

    public bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        curHP = Mathf.Clamp(curHP, 0, maxHP);

        if(immortal) { curHP = maxHP; }
        if(curHP <= 0f) { Destroy(gameObject); }
    }

    public void takeHit(float dmg, int shootType) {
        if(!immunities[shootType])
        {
            hit = true;
            curHP -= dmg;
        }
    }
}
