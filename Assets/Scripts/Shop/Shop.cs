using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    //public Slot[] slots;
    //[SerializeField]
    //private GameObject SlotsParent;

    public GameObject ItemBuy_Base;
    public GameObject ItemReuslt_Base;

    public InventoryUI inven;

    public QuickSlotUI quickslotui;
    public Slot[] slots;
    public QuickSlot[] quickslots;

    public Item HPPotion;
    public Item MPPotion;
    public Item Jwerl;
    public Item JwerlBlue;

    public Button HPButton;
    public Button MPButton;
    public Button JwerlButton;
    public Button JwerlBlueButton;
    public Button ItemBuyYesButton;
    public Button ItemBuyNoButton;
    public Button ItemCountUpButton;
    public Button ItemCountDownButton;

    public Text ItemCountText;
    public Text ItemBuyText;
    public Text GetGold;
    public Text ResultText;
    public int ItemCount;

    public bool ishppotion = false;
    public bool ismppotion = false;
    public bool isJwerl = false;
    public bool isJwerlBlue = false;
    public int Potionexpense;
    public int Jwerlexpense2;
    public int Jwerlexpense3;

    public AudioClip[] clip;
    // Start is called before the first frame update
    void Start()
    {
        //slots = SlotsParent.GetComponentsInChildren<Slot>();
        ItemBuy_Base.SetActive(false);
        ItemReuslt_Base.SetActive(false);
        HPButton.onClick.AddListener(HpButton);
        MPButton.onClick.AddListener(MpButton);
        JwerlButton.onClick.AddListener(Jwerlbutton);
        JwerlBlueButton.onClick.AddListener(JwerlBludbutton);
        ItemBuyYesButton.onClick.AddListener(ItembuyyesButton);
        ItemBuyNoButton.onClick.AddListener(ItembuynoButton);
        ItemCountUpButton.onClick.AddListener(ItemcountupButton);
        ItemCountDownButton.onClick.AddListener(ItemcountdownButton);
    }

    // Update is called once per frame
    void Update()
    {
        ItemCountText.text = ItemCount.ToString();
        ItembuyText();
        GetGold.text = PlayerManager.Instance.gold.ToString();
    }
    void HpButton()
    {
        Debug.Log("포션을 구매하시겠습니까?");
        ItemBuy_Base.SetActive(true);
        SoundManager.instance.SFXPlay("ClickSound", clip[1]);
        ishppotion = true;
    }
    void MpButton()
    {
        Debug.Log("마나포션을 구매하시겠습니까?");
        ItemBuy_Base.SetActive(true);
        SoundManager.instance.SFXPlay("ClickSound", clip[1]);
        ismppotion = true;
    }
    void Jwerlbutton()
    {
        ItemBuy_Base.SetActive(true);
        SoundManager.instance.SFXPlay("ClickSound", clip[1]);
        isJwerl = true;
    }
    void JwerlBludbutton()
    {
        ItemBuy_Base.SetActive(true);
        SoundManager.instance.SFXPlay("ClickSound", clip[1]);
        isJwerlBlue = true;
    }
    void ItembuyyesButton()
    {
        if(HPPotion != null && ishppotion && PlayerManager.Instance.gold >= Potionexpense)
        {
            PlayerManager.Instance.gold -= Potionexpense;
            ItemReuslt_Base.SetActive(true);
            ResultText.text = "체력포션을 구매 하였습니다";
            inven.AcquireItem(HPPotion, ItemCount);
            quickslotui.AcquireItem(HPPotion, ItemCount);
            SoundManager.instance.SFXPlay("HPPotion", clip[0]);
            Debug.Log("HP");
            ItemCount = 0;
            Potionexpense = 0;
            ItemBuy_Base.SetActive(false);
            StartCoroutine(Potionend());
            StartCoroutine(result());
        }
        else if(MPPotion != null && ismppotion && PlayerManager.Instance.gold >= Potionexpense)
        {
            PlayerManager.Instance.gold -= Potionexpense;
            inven.AcquireItem(MPPotion, ItemCount);
            quickslotui.AcquireItem(MPPotion, ItemCount);
            ItemReuslt_Base.SetActive(true);
            ResultText.text = "마나포션을 구매 하였습니다";
            SoundManager.instance.SFXPlay("HPPotion", clip[0]);
            Debug.Log("mp");
            ItemCount = 0;
            Potionexpense = 0;
            ItemBuy_Base.SetActive(false);
            StartCoroutine(Potionend());
            StartCoroutine(result());
        }
        else if(Jwerl != null && isJwerl && PlayerManager.Instance.gold >= Jwerlexpense2)
        {
            PlayerManager.Instance.gold -= Jwerlexpense2;
            inven.AcquireItem(Jwerl, ItemCount);
            ItemReuslt_Base.SetActive(true);
            ResultText.text = "무기 강화보석을 구매 하였습니다";
            SoundManager.instance.SFXPlay("HPPotion", clip[0]);
            ItemCount = 0;
            Jwerlexpense2 = 0;
            Jwerlexpense3 = 0;
            ItemBuy_Base.SetActive(false);
            StartCoroutine(Potionend());
            StartCoroutine(result());
        }
        else if(JwerlBlue != null && isJwerlBlue && PlayerManager.Instance.gold >= Jwerlexpense3)
        {
            PlayerManager.Instance.gold -= Jwerlexpense3;
            inven.AcquireItem(JwerlBlue, ItemCount);
            ItemReuslt_Base.SetActive(true);
            ResultText.text = "방어구 강화 보석을 구매 하였습니다";
            SoundManager.instance.SFXPlay("HPPotion", clip[0]);
            ItemCount = 0;
            Jwerlexpense2 = 0;
            Jwerlexpense3 = 0;
            ItemBuy_Base.SetActive(false);
            StartCoroutine(Potionend());
            StartCoroutine(result());
        }
        else
        {
            ItemReuslt_Base.SetActive(true);
            ResultText.text = "골드가 부족합니다";
            ItemCount = 0;
            ItemBuy_Base.SetActive(false);
            StartCoroutine(Potionend());
            StartCoroutine(result());
        }
    }
    void ItembuynoButton()
    {
        ItemBuy_Base.SetActive(false);
        ItemReuslt_Base.SetActive(true);
        SoundManager.instance.SFXPlay("ClickSound", clip[1]);
        ResultText.text = "구매를 취소하였습니다";
        ItemCount = 0;
        StartCoroutine(Potionend());
        StartCoroutine(result());
    }
    IEnumerator Potionend()
    {
        yield return new WaitForSeconds(0.1f);
        ismppotion = false;
        ishppotion = false;
        isJwerl = false;
        isJwerlBlue = false;
    }
    IEnumerator result()
    {
        yield return new WaitForSeconds(0.7f);
        ItemReuslt_Base.SetActive(false);
    }
    
    void ItemcountupButton()
    {
        SoundManager.instance.SFXPlay("ClickSound", clip[1]);
        ItemCount++;
        Debug.Log("치ㅑ차");
        if(ishppotion || ismppotion)
        {
            Potionexpense += 50;
        }
        if(isJwerl)
        {
            Jwerlexpense2 += 100;
        }
        if(isJwerlBlue)
        {
            Jwerlexpense3 += 100;
        }
        
    }
    void ItemcountdownButton()
    {
        SoundManager.instance.SFXPlay("ClickSound", clip[1]);
        ItemCount--;
        if (ishppotion || ismppotion)
        {
            Potionexpense -= 50;
        }
        if (isJwerl)
        {
            Jwerlexpense2 -= 100;
        }
        if (isJwerlBlue)
        {
            Jwerlexpense3 -= 100;
        }
    }
    void ItembuyText()
    {
        if (ishppotion)
        {
            ItemBuyText.text = HPPotion.itemDesc + ItemCount + " 개 구매 하시겠습니까?";
        }
        if (ismppotion)
        {
            ItemBuyText.text = MPPotion.itemDesc + ItemCount + " 개 구매 하시겠습니까?";
        }
        if(isJwerl)
        {
            ItemBuyText.text = Jwerl.itemDesc + ItemCount + " 개 구매 하시겠습니까?";
        }
        if(isJwerlBlue)
        {
            ItemBuyText.text = JwerlBlue.itemDesc + ItemCount + " 개 구매 하시겠습니까?";
        }
    }
    void Resulttext()
    {

    }
}
