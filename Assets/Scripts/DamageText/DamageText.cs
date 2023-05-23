using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageText : MonoBehaviour
{
    public int damage;
    public TextMeshPro damageTextMesh;

    // Start is called before the first frame update
    void Start()
    {
        damageTextMesh = GetComponent<TextMeshPro>();
        damageTextMesh.text = damage.ToString();
        if(PlayerManager.Instance.isCiritical == true)
        {
            damageTextMesh.color = Color.yellow;
            damageTextMesh.fontSize = 3;
        }
        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * 1 * Time.deltaTime);
    }
}
