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

        // ��� �ִ� ������Ʈ�� ã�Ƽ� �־��� �ε��� ����
        int emptyIndex = -1;

        for (int i = 0; i < GetItem.Length; i++)
        {
            if (!GetItem[i].activeSelf) //GetItem ������Ʈ�� ��Ȱ��ȭ���̶�� 
            {
                emptyIndex = i;
                break;
            }
        }

        if (emptyIndex != -1) // ����ִ� ������ �ִٸ�
        {
            Debug.Log($"{item.itemName}��(��) ȹ���Ͽ����ϴ�");

            // ������Ʈ�� Ȱ��ȭ�ϰ� ������ �����մϴ�.
            GetItem[emptyIndex].SetActive(true); 
            ItemImage[emptyIndex].sprite = item.itemImage;
            GetItemText[emptyIndex].text = $"{item.itemDesc}�� ȹ���Ͽ����ϴ�";

            StartCoroutine(DeactivateObject(GetItem[emptyIndex])); //GetItem Index�� Item�� �ִٸ� 0.5f�ʵڿ� ��Ȱ��ȭ�� �����ϰ�
        }
        else
        {
            Debug.Log("��� �ִ� ������ �����ϴ�");
        }

    }
    private IEnumerator DeactivateObject(GameObject obj)
    {
        yield return new WaitForSeconds(0.5f);

        obj.SetActive(false);
    }

}
