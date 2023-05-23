using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName ="New Item/item")]
public class Item : ScriptableObject
{
    public string itemName; // 아이템 이름
    [TextArea]
    public string itemDesc; // 아이템의 설명
    [TextArea]
    public string itemExplanation; // 아이템 설명 2
    public ItemType itemtype;
    public Sprite itemImage; // 아이템 이미지 Sprite
    public GameObject itemPrefab; // 아이템 프리팹

    public string weaponType; // 무기유형 ( 사용할지 안할지 잘 모르겠음 )
    internal ItemType itemType;

    public enum ItemType
    {
        EquipmentWeapon, // 무기 아이템 인지
        EquipmentPitching,
        Head, // 투구인지
        Used, // 소모품d인지
        Ingredient, // 재료 인지
        ETC // 기타 아이템 인지
    }
   


}
