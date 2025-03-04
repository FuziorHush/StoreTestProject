using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerItemsDetection PlayerItemsDetection { get; private set; }
    public PlayerItemsHolder PlayerItemsHolder { get; private set; }
    [Zenject.Inject] IInputHandler _inputHandler;
    [Zenject.Inject] HUD _HUD;

    private List<IUpdateContext> _updateContexts = new List<IUpdateContext>();

    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _itemsHoldPoint;

    [SerializeField] private LayerMask _groundLM;
    [SerializeField] private LayerMask _itemsDetectionSphereLM;
    [SerializeField] private LayerMask _itemsDetectionRaycastLM;

    public void Init()
    {
        PlayerMovement = new PlayerMovement();
        _updateContexts.Add(PlayerMovement);

        PlayerItemsDetection = new PlayerItemsDetection();
        _updateContexts.Add(PlayerItemsDetection);

        PlayerItemsHolder = new PlayerItemsHolder();

        _inputHandler.Init(_HUD, gameObject);
        PlayerMovement.Init(_inputHandler, transform, _camera, GetComponent<CharacterController>(), _groundLM);
        PlayerItemsDetection.Init(PlayerItemsHolder, _itemsDetectionSphereLM, _itemsDetectionRaycastLM, _camera);
        PlayerItemsHolder.Init(_itemsHoldPoint);

    }



    void Update()
    {
        for (int i = 0; i < _updateContexts.Count; i++)
        {
            _updateContexts[i].OnUpdate();
        }
    }
}
