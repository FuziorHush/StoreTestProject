using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsTrigger : MonoBehaviour
{
    [SerializeField] private Text _text;
    private int _itemsNeed = 4;
    private int _items = 0;

    private void Awake()
    {
        _text.text = $"{_items}/{_itemsNeed}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && other.gameObject.GetComponent<Item>()) {
            Destroy(other.gameObject);
            _items++;
            _text.text = $"{_items}/{_itemsNeed}";
        }
    }
}
