using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GetItemScrollView : MonoBehaviour
{
    public GameObject[] GetItem;
    public Image[] ItemImage;
    public TextMeshProUGUI[] GetItemText;
    public Item item;

    void Start()
    {
        for (int i = 0; i < GetItem.Length; i++)
        {
            GetItem[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void AddShowItem(Item _item)
    {
        item = _item;

        // 비어 있는 오브젝트를 찾아서 넣어줄 인덱스 변수
        int emptyIndex = -1;

        for (int i = 0; i < GetItem.Length; i++)
        {
            if (!GetItem[i].activeSelf) //GetItem 오브젝트가 비활성화중이라면 
            {
                emptyIndex = i;
                break;
            }
        }

        if (emptyIndex != -1) // 비어있는 슬롯이 있다면
        {
            Debug.Log($"{item.itemName}을(를) 획득하였습니다");

            // 오브젝트를 활성화하고 정보를 설정합니다.
            GetItem[emptyIndex].SetActive(true); 
            ItemImage[emptyIndex].sprite = item.itemImage;
            GetItemText[emptyIndex].text = $"{item.itemDesc}을 획득하였습니다";

            StartCoroutine(DeactivateObject(GetItem[emptyIndex])); //GetItem Index에 Item이 있다면 0.5f초뒤에 비활성화를 시행하고
        }
        else
        {
            Debug.Log("비어 있는 슬롯이 없습니다");
        }

    }
    private IEnumerator DeactivateObject(GameObject obj)
    {
        yield return new WaitForSeconds(0.5f);

        obj.SetActive(false);
    }

}
