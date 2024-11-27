using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicRing : BaseRing
{
    private RingAbility ringAbility;
    private void Start()
    {
        ringAbility = new RingAbility(0, 0);
    }

    public override RingAbility ReadAbility()
    { 
        return ringAbility;        
    }
}
