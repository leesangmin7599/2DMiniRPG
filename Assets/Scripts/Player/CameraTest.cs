using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    public GameObject testcamera;
    GameObject player;
    // Strt is called before the first frame update
    void Start()
    {
        testcamera.SetActive(false);
        
        player = GameObject.Find("Player");
        if (GameObject.Find("Player") && player.GetComponent<Player>().isTutorialInCityGoPortals == true)
        {
            testcamera.SetActive(true);
            StartCoroutine(CameraFalse());
            StartCoroutine(Stopcamera());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    IEnumerator Stopcamera()
    {
        yield return new WaitForSeconds(13.8f);
        Destroy(gameObject);
    }
    IEnumerator CameraFalse()
    {
        yield return new WaitForSeconds(1f);
        player.GetComponent<Player>().isTutorialInCityGoPortals = false;

    }
}
