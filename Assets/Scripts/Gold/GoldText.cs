using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GoldText : MonoBehaviour
{
    public int gold;
    public TextMeshPro goldText;
   
    // Start is called before the first frame update
    void Start()
    {
        goldText = GetComponent<TextMeshPro>();
        goldText.text = ( "+" +Gold.Instance.rnd + "G");
        goldText.color = Color.yellow;
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * 1 * Time.deltaTime);
    }
}
