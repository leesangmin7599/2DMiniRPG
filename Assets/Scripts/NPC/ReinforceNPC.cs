using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class ReinforceNPC : MonoBehaviour
{
    public Image InteractionImage;

  public TextMeshProUGUI InteractionText;

    public GameObject Interaction;
    private GameObject player;
    


    // Start is called before the first frame update
    void Start()
    {
        Interaction.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       InteractionText.text = ("GÅ° ÀÔ·Â");
    }
   

}
