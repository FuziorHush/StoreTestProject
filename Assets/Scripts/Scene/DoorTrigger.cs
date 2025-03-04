using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Door _door;
    private int _targetLayer;

    private void Awake()
    {
        _targetLayer = LayerMask.NameToLayer("Entity");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _targetLayer)
        {
            _door.OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _targetLayer)
        {
            _door.CloseDoor();
        }
    }
}
