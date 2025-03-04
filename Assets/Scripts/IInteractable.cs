using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnDetect();
    void OnUndetect();
    void Interact(GameObject source);
}
