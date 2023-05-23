using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCamera : MonoBehaviour
{
    public GameObject bosscamera;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
        bosscamera.SetActive(false);
        player = GameObject.Find("Player");
        

    }
    IEnumerator EndBoss()
    {
        yield return new WaitForSeconds(1f);
        bosscamera.SetActive(true);
        
        yield return new WaitForSeconds(2.5f);
        Destroy(bosscamera);
        player.GetComponent<Player>().BossDieGoldObject.SetActive(true);
        Destroy(gameObject);
    }
            
    // Update is called once per frame
    void Update()
    {
        if (BossManager.Instance.currentHp <= 0)
        {
            
            StartCoroutine(EndBoss());
            Debug.Log("BossDie");
        }
    }
}
