using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingComparer : MonoBehaviour
{

    public bool CompareRings(GameObject ring1, GameObject ring2)
    {
        RingAbility ringAbility1 = ring1.GetComponent<BaseRing>().ReadAbility();
        RingAbility ringAbility2 = ring2.GetComponent<BaseRing>().ReadAbility();

        return ringAbility1.type == ringAbility2.type;
    }

}
