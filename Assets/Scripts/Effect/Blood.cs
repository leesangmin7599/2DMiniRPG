using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(effectEnd());
    }
    IEnumerator effectEnd()
    {
        yield return new WaitForSeconds(0.65f);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
