using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageRing : BaseRing
{
    [SerializeField]
    private int damage;

    private RingAbility ringAbility;
    private void Start()
    {
        ringAbility = new RingAbility(1, damage);
    }

    public override RingAbility ReadAbility()
    { 
        ringAbility.damage = ringAbility.AdjacenyDamage(adjacentRings.Count);
        return ringAbility;        
    }
}
