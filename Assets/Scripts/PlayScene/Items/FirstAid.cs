using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : ICollectable
{
    public override void OnCollect(Inventory inv)
    {
        inv.FirstAidAbsorbed(Amount);
        gameObject.SetActive(false);
    }
}
