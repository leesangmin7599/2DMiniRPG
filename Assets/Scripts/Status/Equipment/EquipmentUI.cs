using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    [SerializeField]
    private GameObject EquipmentBase;
    [SerializeField]
    private GameObject equipmentsparent;

    public Equipment[] equipments;
    public Equipment equpment;

    // Start is called before the first frame update
    void Start()
    {
        equipments = equipmentsparent.GetComponentsInChildren<Equipment>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AcquireItem(Item _item)
    {
        if(Item.ItemType.EquipmentWeapon == _item.itemtype)
        {
            if (equipments[9] != null)
            {
                equipments[9].AddItem(_item);
                Debug.Log("¹«±âÀåÂø");
            }
        }
    }
    public void AcquireItem2(Item _itme2)
    {
        if (Item.ItemType.EquipmentPitching == _itme2.itemtype)
        {
            if (equipments[0] != null)
            {
                equipments[0].AddItem(_itme2);
                PlayerManager.Instance.Defence = 3;
                Debug.Log("Åõ±¸ÀåÂø");
            }
        }
    }
    
}
