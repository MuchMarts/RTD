using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class InputManager : MonoBehaviour
{

    [SerializeField]
    private Camera playerCamera;

    private Vector3 lastPosition;

    [SerializeField]
    private LayerMask placementLayerMask;
    [SerializeField]
    private LayerMask towerLayerMask;

    public event Action OnClicked, OnExit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnExit?.Invoke();
        }
    }

    public bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public bool IsPointerOverTower(Vector3 position)
    {
        Collider[] intersecting = Physics.OverlapSphere(position, 0.01f, towerLayerMask);
        if (intersecting.Length > 0) return true;
        return false;
    }

    public bool IsPlacementValid(Vector3 position)
    {
        Collider[] intersecting = Physics.OverlapSphere(position, 0.01f, towerLayerMask);
        if (intersecting.Length == 0) return true;
        return false;  
    }

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = playerCamera.nearClipPlane;
        Ray ray = playerCamera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, placementLayerMask))
        {
            lastPosition = hit.point;
        }

        return lastPosition;
    }
    
}
