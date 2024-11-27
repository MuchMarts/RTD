using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class BaseRing : MonoBehaviour
{
    // TODO Normalize Adjacency
    protected List<GameObject> adjacentRings = new List<GameObject>();

    public bool CompareRings(GameObject ring1, GameObject ring2)
    {
        RingAbility ringAbility1 = ring1.GetComponent<BaseRing>().ReadAbility();
        RingAbility ringAbility2 = ring2.GetComponent<BaseRing>().ReadAbility();

        return ringAbility1.type == ringAbility2.type;
    }

    public void AddAdjacentRing(GameObject ring)
    {
        adjacentRings.Add(ring);
        Debug.Log("Added adjacent ring: " + adjacentRings.Count);
    }

    public void RemoveAdjacentRing(GameObject ring)
    {
        adjacentRings.Remove(ring);
        Debug.Log("Removed adjacent ring: " + adjacentRings.Count);
    }

    public abstract RingAbility ReadAbility();
}

public struct RingAbility {
    public int type; // change to enum
    public int damage;

    
    public RingAbility(int type, int damage)
    {
        this.type = type;
        this.damage = damage;

    }

    public int AdjacenyDamage(int adjacentRings)
    {
        // 1 = 100dmg 100 + 0 adj
        // 2 = 300dmg 200 + 100 adj
        // 3 = 500dmg 300 + 200 adj

        return damage + (adjacentRings - 1) * 100;

    }

}
