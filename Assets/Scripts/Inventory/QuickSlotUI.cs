using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotUI : MonoBehaviour
{
    [SerializeField]
    private GameObject QuickSlotBase;
    [SerializeField]
    private GameObject quickslotsparent;

    private QuickSlot[] quickslot;

    // Start is called before the first frame update
    void Start()
    {
        quickslot = quickslotsparent.GetComponentsInChildren<QuickSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        if(Item.ItemType.EquipmentWeapon != _item.itemtype)
        {
            for(int i = 0; i < quickslot.Length; i++)
            {
                if(quickslot[i].item != null)
                {
                    if(quickslot[i].item.itemName == _item.itemName)
                    {
                        quickslot[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }
        for(int i =0; i<quickslot.Length; i++)
        {
            if(quickslot[i].item == null)
            {
                quickslot[i].AddItem(_item, _count);
                return;
            }
        }
    }
}

