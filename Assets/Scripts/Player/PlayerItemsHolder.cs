using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerItemsHolder
{
    private Transform _holdPoint;

    public GameObject HoldingItem { get; private set; }

    private float _throwForce = 200f;

    public UnityAction PlayerGrabItem;
    public UnityAction PlayerDroppedItem;

    public void Init(Transform holdPoint) {
        _holdPoint = holdPoint;
    }

    public void GrabItem(GameObject item) 
    {
        if (HoldingItem == null) {
            HoldingItem = item;
            HoldingItem.layer = 7;
            HoldingItem.GetComponent<Rigidbody>().isKinematic = true;
            HoldingItem.transform.SetParent(_holdPoint);
            HoldingItem.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            PlayerGrabItem?.Invoke();
        }
    }

    public void DropItem() 
    {
        if (HoldingItem != null)
        {
            HoldingItem.layer = 6;
            HoldingItem.transform.SetParent(null);
            HoldingItem.GetComponent<Rigidbody>().isKinematic = false;
            HoldingItem.GetComponent<Rigidbody>().AddForce(HoldingItem.transform.forward * _throwForce);
            HoldingItem = null;

            PlayerDroppedItem?.Invoke();
        }
    }
}
