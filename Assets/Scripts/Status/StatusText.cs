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
        Damage.text = ("공격력 : " + PlayerManager.Instance.AttackDamage);
        Defence.text = ("방어력 : " + PlayerManager.Instance.Defence);
        CiriticalDamage.text = ("크리티컬 데미지 : " + PlayerManager.Instance.ciriticlaDamage);
        CiriticalPersent.text = ("크리티컬 확률 : " + PlayerManager.Instance.ciriticalPer);
    }
}
