using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsDetection : IUpdateContext
{
    public float _detectionDistance = 2f;
    private LayerMask _itemsDetectionSphereLM;
    private LayerMask _itemsDetectionRaycastLM;
    private Transform _cameraTransform;

    private List<GameObject> _detectedItems = new List<GameObject>();
    private bool _detectItems;

    public void Init(PlayerItemsHolder playerItemsHolder, LayerMask itemsDetectionSphereLM, LayerMask itemsDetectionRaycastLM, Transform cameraTransform) 
    {
        _itemsDetectionSphereLM = itemsDetectionSphereLM;
        _itemsDetectionRaycastLM = itemsDetectionRaycastLM;
        _cameraTransform = cameraTransform;
        playerItemsHolder.PlayerGrabItem += OnPlayerGrabbedItem;
        playerItemsHolder.PlayerDroppedItem += OnPlayerDroppedItem;
        _detectItems = true;
    }

    public void OnUpdate() 
    {
        if (_detectItems)
        {
            DetectObjects();
            CheckDetectedObjects();
        }
    }

    private void DetectObjects()
    {
        Collider[] itemsNearby = Physics.OverlapSphere(_cameraTransform.position, _detectionDistance, _itemsDetectionSphereLM);

        for (int i = 0; i < itemsNearby.Length; i++)
        {
            if (_detectedItems.Contains(itemsNearby[i].gameObject))
                continue;

            Vector3 directionToTarget = (itemsNearby[i].transform.position - _cameraTransform.position).normalized;
            Ray detectionRay = new Ray(_cameraTransform.position, directionToTarget);
            RaycastHit hit;

            if (Physics.Raycast(detectionRay, out hit, float.MaxValue, _itemsDetectionRaycastLM))
            {
                if (hit.collider.gameObject.layer == 6)
                {
                    _detectedItems.Add(itemsNearby[i].gameObject);
                    itemsNearby[i].GetComponent<IInteractable>().OnDetect();
                }
            }
        }
    }

    private void CheckDetectedObjects()
    {
        for (int i = 0; i < _detectedItems.Count; i++)
        {
            if (_detectedItems[i] == null)
            {
                _detectedItems.RemoveAt(i);
                continue;
            }

            Vector3 directionToTarget = (_detectedItems[i].transform.position - _cameraTransform.position).normalized;
            Ray detectionRay = new Ray(_cameraTransform.position, directionToTarget);
            RaycastHit hit;

            bool hitsRaycast = false;
            if (Physics.Raycast(detectionRay, out hit, float.MaxValue, _itemsDetectionRaycastLM))
            {
                hitsRaycast = hit.collider.gameObject.layer == 6;
            }

            if (Vector3.Distance(_cameraTransform.position, _detectedItems[i].transform.position) > _detectionDistance + 0.3f || !hitsRaycast)
            {
                _detectedItems[i].GetComponent<IInteractable>().OnUndetect();
                _detectedItems.RemoveAt(i);
            }
        }
    }

    private void OnPlayerGrabbedItem()
    {
        _detectItems = false;
        foreach(GameObject item in _detectedItems)
        {
            item.GetComponent<IInteractable>().OnUndetect();
        }
        _detectedItems = new List<GameObject>();
    }

    private void OnPlayerDroppedItem()
    {
        _detectItems = true;
    }
}
