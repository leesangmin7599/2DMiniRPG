using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaxHpText : MonoBehaviour
{
    TextMeshProUGUI damageTextMesh;
    // Start is called before the first frame update
    void Start()
    {
        damageTextMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
       damageTextMesh.text = PlayerManager.Instance.maxHp.ToString();
    }
}
