using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class Item : MonoBehaviour, IInteractable
{
    private Outline _outline;
    private bool _detected;

    [Zenject.Inject] PlayerBase _playerBase;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    private void Start()
    {
        _outline.enabled = false;
    }

    public void OnMouseDown()
    {
        if (_detected) {
            Interact(_playerBase.gameObject);
        }
    }

    public void Interact(GameObject source)
    {
        if (source.TryGetComponent(out PlayerBase playerBase)) 
        {
            playerBase.PlayerItemsHolder.GrabItem(gameObject);
            _detected = false;
            _outline.enabled = false;
        }
    }

    public void OnDetect()
    {
        _detected = true;
        _outline.enabled = true;
    }

    public void OnUndetect()
    {
        _detected = false;
        _outline.enabled = false;
    }
}
