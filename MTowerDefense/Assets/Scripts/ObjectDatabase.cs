using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectDatabase : ScriptableObject
{
    public List<TowerObject> towerObject;
    public List<RingObject> ringObject;
}

[Serializable]
public class TowerObject
{

    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
    [field: SerializeField]
    public int Cost { get; private set; }
    [field: SerializeField]
    public Vector2Int Size { get; private set; }

}

[Serializable]
public class RingObject
{
    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
    [field: SerializeField]
    public int Cost { get; private set; }
}