using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUD : MonoBehaviour
{
    [SerializeField] private Joystick _movementJoystick;
    [SerializeField] private Joystick _lookJoystick;
    [SerializeField] private Button _dropButton;

    public UnityAction DropButtonPressed;

    private void Awake()
    {
        _dropButton.onClick.AddListener(delegate { DropButtonPressed?.Invoke(); });
        _dropButton.gameObject.SetActive(false);
    }

    public void Init(PlayerBase playerBase)
    {
        playerBase.PlayerItemsHolder.PlayerGrabItem += ShowDropButton;
        playerBase.PlayerItemsHolder.PlayerDroppedItem += HideDropButton;
    }

    public Vector2 GetMovementVectorAxis()
    {
        return new Vector2(_movementJoystick.Horizontal, _movementJoystick.Vertical);
    }

    public Vector2 GetLookVectorAxis()
    {
        return new Vector2(_lookJoystick.Horizontal, _lookJoystick.Vertical);
    }

    private void ShowDropButton() {
        _dropButton.gameObject.SetActive(true);
    }

    private void HideDropButton()
    {
        _dropButton.gameObject.SetActive(false);
    }
}
