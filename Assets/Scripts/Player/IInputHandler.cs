using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInputHandler
{
    void Init(HUD hud, GameObject player);

    Vector2 GetMovement();
    Vector2 GetLookVector();
    void DropItem();
}
