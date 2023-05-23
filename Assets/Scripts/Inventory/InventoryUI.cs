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

    // ���Ե�
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
        if (Item.ItemType.EquipmentWeapon != _item.itemtype) // ȹ���� ������ ��� �ƴ϶��
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName) //�������� ȹ���ߴµ� ���Ծȿ� �Ȱ��� �������� �ִٸ�
                    {
                        slots[i].SetSlotCount(_count); // ������ ����
                        return;
                    }
                }
            }
        }
        if (Item.ItemType.EquipmentPitching != _item.itemtype) // ȹ���� ������ ��� �ƴ϶��
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName) //�������� ȹ���ߴµ� ���Ծȿ� �Ȱ��� �������� �ִٸ�
                    {
                        slots[i].SetSlotCount(_count); // ������ ����
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
