using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandlerMobile : IInputHandler
{
    private PlayerItemsHolder _playerItemsHolder;
    private HUD _hud;

    public void Init(HUD hud,GameObject player) 
    {
        _playerItemsHolder = player.GetComponent<PlayerBase>().PlayerItemsHolder;
        _hud = hud;
        _hud.DropButtonPressed += DropItem;
    }

    public void DropItem()
    {
        _playerItemsHolder.DropItem();
    }

    public Vector2 GetLookVector()
    {
        return _hud.GetLookVectorAxis();
    }

    public Vector2 GetMovement()
    {
        return _hud.GetMovementVectorAxis();
    }
}
