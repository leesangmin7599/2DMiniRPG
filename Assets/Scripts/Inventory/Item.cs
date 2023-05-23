using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName ="New Item/item")]
public class Item : ScriptableObject
{
    public string itemName; // ������ �̸�
    [TextArea]
    public string itemDesc; // �������� ����
    [TextArea]
    public string itemExplanation; // ������ ���� 2
    public ItemType itemtype;
    public Sprite itemImage; // ������ �̹��� Sprite
    public GameObject itemPrefab; // ������ ������

    public string weaponType; // �������� ( ������� ������ �� �𸣰��� )
    internal ItemType itemType;

    public enum ItemType
    {
        EquipmentWeapon, // ���� ������ ����
        EquipmentPitching,
        Head, // ��������
        Used, // �Ҹ�ǰd����
        Ingredient, // ��� ����
        ETC // ��Ÿ ������ ����
    }
   


}
