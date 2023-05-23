using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlot : MonoBehaviour, IEndDragHandler, IDropHandler
{
    static public QuickSlot instance;

    //public GameObject slot;
    //public GameObject slot2;
    public GameObject quickslot;
    public GameObject quickslot2;
    public Slot[] slots;


    public Item item;
    public int itemCount; // 획득한 아이템의 갯수
    public Image itemImage;
    
    [SerializeField]
    private Text text_Count; // 획득한 아이템의 갯수를 나타내기위해 만든 변수
    [SerializeField]
    private GameObject CountImage;

    public AudioClip[] clip;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    void Update()
    {
        TryNumberKey();
    }
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        if (item.itemtype != Item.ItemType.EquipmentWeapon)
        {
            CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            CountImage.SetActive(false);
        }
        SetColor(1);
    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();
        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }
    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);
        text_Count.text = "0";
        CountImage.SetActive(false);
    }

    public void TryNumberKey()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("g");
            if (quickslot.GetComponent<QuickSlot>().item != null)
            {
                if(quickslot.GetComponent<QuickSlot>().item.itemName == "HP" && PlayerManager.Instance.currentHp < 100)
                {
                    Debug.Log("Hp");
                    PlayerManager.Instance.currentHp += 30;
                    SoundManager.instance.SFXPlay("HPPotion", clip[0]);
                    if (PlayerManager.Instance.currentHp >= PlayerManager.Instance.maxHp)
                    {
                        PlayerManager.Instance.currentHp = PlayerManager.Instance.maxHp;
                    }
                    quickslot.GetComponent<QuickSlot>().SetSlotCount(-1);
                    for (int i = 0; i < slots.Length; i++)
                    {
                        if (slots[i].GetComponent<Slot>().item != null)
                        {
                            if (slots[i].GetComponent<Slot>().item.itemName == "HP")
                            {
                                slots[i].SetSlotCount(-1);
                                if (slots[i].item == null)
                                {
                                    return;
                                }
                            }
                        }
                    }
                    if (quickslot.GetComponent<QuickSlot>().item == null)
                    {
                        return;
                    }
                }
                if (quickslot.GetComponent<QuickSlot>().item.itemName == "Mana" && PlayerManager.Instance.currentMana < 100)
                {
                    Debug.Log("Mana");
                    PlayerManager.Instance.currentMana += 30;
                    SoundManager.instance.SFXPlay("HPPotion", clip[0]);
                    if (PlayerManager.Instance.currentMana >= PlayerManager.Instance.MaxMana)
                    {
                        PlayerManager.Instance.currentMana = PlayerManager.Instance.MaxMana;
                    }
                    quickslot.GetComponent<QuickSlot>().SetSlotCount(-1);
                    for (int i = 0; i < slots.Length; i++)
                    {
                        if (slots[i].GetComponent<Slot>().item != null)
                        {
                            if (slots[i].GetComponent<Slot>().item.itemName == "Mana")
                            {
                                slots[i].SetSlotCount(-1);
                                if (slots[i].item == null)
                                {
                                    return;
                                }
                            }
                        }
                    }
                    if (quickslot.GetComponent<QuickSlot>().item == null)
                    {
                        return;
                    }
                }
                
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (quickslot2.GetComponent<QuickSlot>().item != null)
            {
                if (quickslot2.GetComponent<QuickSlot>().item.itemName == "HP" && PlayerManager.Instance.currentHp < 100)
                {
                    Debug.Log("2 Hp");
                    PlayerManager.Instance.currentHp += 30;
                    SoundManager.instance.SFXPlay("HPPotion", clip[0]);
                    if (PlayerManager.Instance.currentHp >= PlayerManager.Instance.maxHp)
                    {
                        PlayerManager.Instance.currentHp = PlayerManager.Instance.maxHp;
                    }
                    quickslot2.GetComponent<QuickSlot>().SetSlotCount(-1);
                    for(int i = 0; i < slots.Length; i++)
                    {
                        if(slots[i].GetComponent<Slot>().item != null)
                        {
                            if (slots[i].GetComponent<Slot>().item.itemName == "HP")
                            {
                                slots[i].SetSlotCount(-1);
                                if (slots[i].item == null)
                                {
                                    return;
                                }
                            }
                        }
                    }
                        
                    if (quickslot2.GetComponent<QuickSlot>().item == null)
                    {
                        return;
                    }
                }
                if (quickslot2.GetComponent<QuickSlot>().item.itemName == "Mana" && PlayerManager.Instance.currentMana < 100)
                {
                    Debug.Log("2 Mana");
                    PlayerManager.Instance.currentMana += 30;
                    SoundManager.instance.SFXPlay("HPPotion", clip[0]);
                    if (PlayerManager.Instance.currentMana >= PlayerManager.Instance.MaxMana)
                    {
                        PlayerManager.Instance.currentMana = PlayerManager.Instance.MaxMana;
                    }
                    quickslot2.GetComponent<QuickSlot>().SetSlotCount(-1);
                    for (int i = 0; i < slots.Length; i++)
                    {
                        if (slots[i].GetComponent<Slot>().item != null)
                        {
                            if (slots[i].GetComponent<Slot>().item.itemName == "Mana")
                            {
                                slots[i].SetSlotCount(-1);
                                if (slots[i].item == null)
                                {
                                    return;
                                }
                            }
                        }
                    }
                    if (quickslot2.GetComponent<QuickSlot>().item == null)
                    {
                        return;
                    }
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null) //인벤토리에 뭔가 있을때만 사용
        {
            ChangeSlot();
            Debug.Log("QuickSlotOnDropEnd");
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null) //인벤토리에 뭔가 있을때만 사용
        {
            ChangeSlot();
            Debug.Log("QuickSlotOnDrop");
        }
    }
    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }
}