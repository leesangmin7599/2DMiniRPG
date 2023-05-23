using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private GameObject InventoryBase;
    [SerializeField]
    private GameObject SlotsParent;

    // 슬롯들
    private Slot[] slots;

   public TextMeshProUGUI goldtext;
    // Start is called before the first frame update
    void Start()
    {
        slots = SlotsParent.GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        goldtext.text = (("")+PlayerManager.Instance.gold);
    } 
    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.EquipmentWeapon != _item.itemtype) // 획득한 물건이 장비가 아니라면
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName) //아이템을 획득했는데 슬롯안에 똑같은 아이템이 있다면
                    {
                        slots[i].SetSlotCount(_count); // 갯수를 증가
                        return;
                    }
                }
            }
        }
        if (Item.ItemType.EquipmentPitching != _item.itemtype) // 획득한 물건이 장비가 아니라면
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName) //아이템을 획득했는데 슬롯안에 똑같은 아이템이 있다면
                    {
                        slots[i].SetSlotCount(_count); // 갯수를 증가
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
