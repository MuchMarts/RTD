using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{

    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private ObjectDatabase towerObjectDatabase;
    private int selectedTowerID = -1;

    private void Start()
    {
        StopPlacement();
    }


    public void StartPlacement(int towerID)
    {
        StopPlacement();
        selectedTowerID = towerID;
        if (selectedTowerID < 0)
        {
            Debug.LogError("Invalid tower ID");
            return;
        }
        TowerObject selectedTower = towerObjectDatabase.towerObject.Find(x => x.ID == towerID);
        if (selectedTower == null)
        {
            Debug.LogError("Tower ID not found in database");
            return;
        }

        mouseIndicator.SetActive(true);
        cellIndicator.SetActive(true);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;

    }

    private void PlaceStructure() 
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }

        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        if (!inputManager.IsPlacementValid(grid.GetCellCenterWorld(gridPos)))
        {
            Debug.Log("Invalid placement");
            return;
        }

        Debug.Log("Placing tower at " + gridPos);
        GameObject tower = Instantiate(towerObjectDatabase.towerObject[selectedTowerID].Prefab);
        tower.transform.position = grid.CellToWorld(gridPos);

    }

    public void StopPlacement()
    {
        selectedTowerID = -1;
        mouseIndicator.SetActive(false);
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    private void Update()
    {
        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        mouseIndicator.transform.position = mousePos;
        cellIndicator.transform.position = grid.CellToWorld(gridPos);
    }

}
