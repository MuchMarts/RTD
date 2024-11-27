using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    private Stack<GameObject> ringStack = new Stack<GameObject>(6);

    public void Start()
    {
        Debug.Log("TowerBehaviour Start");
    }


    public bool AddRing(GameObject ring, GameObject ringAnchor)
    {
        Debug.Log("Trying to add Ring");
        if (ringStack.Count > 6)
        {
            return false;
        }

        // TODO, instead of type give neigbhour object
        if (ringStack.Count > 0 && ring.GetComponent<BaseRing>().CompareRings(ring, GetTopRing()))
        {
            ring.GetComponent<BaseRing>().AddAdjacentRing(GetTopRing());
            GetTopRing().GetComponent<BaseRing>().AddAdjacentRing(ring);
        }

        
        ringStack.Push(ring);

        ring.transform.SetParent(ringAnchor.transform);
        
        float offset = ringStack.Count * 0.5f;

        ring.transform.localPosition = new Vector3(0, offset, 0);

        return true;
    }

    public GameObject GetTopRing()
    {
        if (ringStack.Count == 0)
        {
            return null;
        }
        return ringStack.Peek();
    }

    public GameObject RemoveRing()
    {
        Debug.Log("Removing Ring");
        if (ringStack.Count == 0)
        {
            return null;
        }
        GameObject ring = ringStack.Pop();

        if (ringStack.Count > 0)
        {
            if(ring.GetComponent<BaseRing>().CompareRings(ring, GetTopRing())) 
            {
                ring.GetComponent<BaseRing>().RemoveAdjacentRing(GetTopRing());
                GetTopRing().GetComponent<BaseRing>().RemoveAdjacentRing(ring);
            }
        }

        return ring;
    }

    public bool IsRingStackFull()
    {
        return ringStack.Count >= 6;
    }

}
