using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class StatusText : MonoBehaviour
{
    public TextMeshProUGUI Damage;
    public TextMeshProUGUI CiriticalDamage;
    public TextMeshProUGUI CiriticalPersent;
    public TextMeshProUGUI Defence;
    
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Damage.text = ("���ݷ� : " + PlayerManager.Instance.AttackDamage);
        Defence.text = ("���� : " + PlayerManager.Instance.Defence);
        CiriticalDamage.text = ("ũ��Ƽ�� ������ : " + PlayerManager.Instance.ciriticlaDamage);
        CiriticalPersent.text = ("ũ��Ƽ�� Ȯ�� : " + PlayerManager.Instance.ciriticalPer);
    }
}
