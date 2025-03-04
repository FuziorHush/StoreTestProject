using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Zenject.Inject] private PlayerBase _playerBase;
    [Zenject.Inject] private HUD _HUD;

    void Start()
    {
        _playerBase.Init();
        _HUD.Init(_playerBase);
    }
}
