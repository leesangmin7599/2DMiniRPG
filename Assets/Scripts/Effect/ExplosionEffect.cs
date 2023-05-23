using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
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
        transform.Translate(transform.right * 10 * Time.deltaTime);
        Debug.Log("Right");
    }
}
