using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private LayerMask towerLayerMask;
    [SerializeField]
    private ObjectDatabase ringObjectDatabase;

    private GameObject currentRing = null;
    private int selectedNewRingID = -1;
    private TowerBehaviour ringOriginTower;

    private void Start()
    {
        StopPlacement();
        inputManager.OnClicked += SelectTopRing;
    }
    
    private void Destroy()
    {
        inputManager.OnClicked -= SelectTopRing;
    }
    public void StartPlacementWithNewRing(int ID)
    {
        StopPlacement();

        selectedNewRingID = ringObjectDatabase.ringObject.FindIndex(x => x.ID == ID);
        if (selectedNewRingID < 0)
        {
            Debug.LogError("Invalid ring ID");
            return;
        }

        GameObject ring = Instantiate(ringObjectDatabase.ringObject[selectedNewRingID].Prefab);
        ring.transform.position = new Vector3(0, -10, 0);
        currentRing = ring;

        inputManager.OnClicked += AddRing;
        inputManager.OnExit += StopPlacement;
        mouseIndicator.SetActive(true);
    }
    
    public void SelectTopRing()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }

        if (currentRing != null)
        {
            return;
        }    

        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        if (!inputManager.IsPointerOverTower(grid.GetCellCenterWorld(gridPos)))
        {
            Debug.Log("No Tower Selected");
            return;
        }
        Collider[] intersecting = Physics.OverlapSphere(grid.GetCellCenterWorld(gridPos), 0.01f, towerLayerMask);
        GameObject tower = intersecting[0].gameObject;

        TowerBehaviour towerBehaviour = tower.GetComponentInParent<TowerBehaviour>();
        if (towerBehaviour == null)
        {
            Debug.LogError("No tower behaviour found");
            return;
        }

        if (towerBehaviour.GetTopRing() != null)
        {
            StartPlacementWithExistingRing(towerBehaviour.GetTopRing(), towerBehaviour);
        }
        else
        {
            Debug.Log("No ring on tower");
        }
    }

    private void StartPlacementWithExistingRing(GameObject ring, TowerBehaviour tower)
    {
        StopPlacement();
        currentRing = ring;
        ringOriginTower = tower;
        inputManager.OnClicked += AddRing;
        inputManager.OnExit += StopPlacement;
        mouseIndicator.SetActive(true);

    }
    
    private void AddRing()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }

        if (currentRing == null)
        {
            Debug.LogError("No ring selected");
            return;
        }

        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        if (!inputManager.IsPointerOverTower(grid.GetCellCenterWorld(gridPos)))
        {
            Debug.Log("No Tower Selected");
            return;
        }
        Collider[] intersecting = Physics.OverlapSphere(grid.GetCellCenterWorld(gridPos), 0.01f, towerLayerMask);
        GameObject tower = intersecting[0].gameObject;

        TowerBehaviour towerBehaviour = tower.GetComponentInParent<TowerBehaviour>();
        if (towerBehaviour == null)
        {
            Debug.LogError("No tower behaviour found");
            if (selectedNewRingID != -1) Destroy(currentRing);
            if (ringOriginTower != null) ringOriginTower = null;
            
            StopPlacement();
            return;
        }

        if (towerBehaviour.IsRingStackFull())
        {
            Debug.Log("Tower is full");
            if (selectedNewRingID != -1) Destroy(currentRing);
            if (ringOriginTower != null) ringOriginTower = null;
            StopPlacement();
        }

        if (ringOriginTower != null) ringOriginTower.RemoveRing();
        towerBehaviour.AddRing(currentRing, tower.transform.parent.Find("RingAnchor").gameObject);
        Debug.Log("Ring added to tower");

        StopPlacement();
    }

    private void StopPlacement()
    {
        inputManager.OnClicked -= AddRing;
        inputManager.OnExit -= StopPlacement;
        selectedNewRingID = -1;
        mouseIndicator.SetActive(false);
        
        if (currentRing != null) currentRing = null;
        if (ringOriginTower != null) ringOriginTower = null;
    }
}
