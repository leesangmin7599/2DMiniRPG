using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeManager : MonoBehaviour
{
    private GameObject player; // Player 오브젝트를 저장할 변수
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        GameObject spawnPoint = GameObject.Find("FirstMapStartPoint");

        if(spawnPoint != null)
        {
            Vector3 SpawnPoints = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
            player.transform.position = SpawnPoints;
        }
        else
        {
            Debug.LogError("SpawnPoint not found in new scene.");
        }
    }
    void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy()
    {
    }
}
