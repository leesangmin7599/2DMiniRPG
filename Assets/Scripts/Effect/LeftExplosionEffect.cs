using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftExplosionEffect : MonoBehaviour
{
    Rigidbody2D rid;
    // Start is called before the first frame update
    void Start()
    {
        rid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * -1 * 10 * Time.deltaTime);
        
    }
}
