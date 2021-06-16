using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ENUMS;

public abstract class ICollectable :MonoBehaviour
{
    [SerializeField]protected CollectableType type;
    [SerializeField]protected int amount;

    public CollectableType Type=> type;
    public int Amount => amount;
    public abstract void OnCollect(Inventory inv);
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Inventory>(out var inv))
        {
            OnCollect(inv);
        }
    }
}
