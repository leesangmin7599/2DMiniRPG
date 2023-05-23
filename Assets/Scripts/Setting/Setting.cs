using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public GameObject SettingObject;

    public Button StartButton;
    public Button SettingButton;
    public Button SettingExitButton;

    public GameObject Fadeinout;
    public Image PanelImage;
    public AudioClip[] clip;
    // Start is called before the first frame update
    void Start()
    {
        SettingObject.SetActive(false);
        Fadeinout.SetActive(false);
        SettingButton.onClick.AddListener(settingbutton);
        SettingExitButton.onClick.AddListener(settingexitbutton);
        StartButton.onClick.AddListener(startbutton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void settingbutton()
    {
        SettingObject.SetActive(true);
        SoundManager.instance.SFXPlay("ClickSound", clip[0]);
    }
    void settingexitbutton()
    {
        SettingObject.SetActive(false);
        SoundManager.instance.SFXPlay("ClickSound", clip[0]);
    }
    void startbutton()
    {
        SoundManager.instance.SFXPlay("ClickSound", clip[0]);
        StartCoroutine(FadeIn());
        StartCoroutine(Tutorialgo());
        
    }
    IEnumerator FadeIn() // 화면 점점 어둡게
    {
        Fadeinout.SetActive(true);
        float faedCount = 0;
        while (faedCount <= 1.0f)
        {
            faedCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            PanelImage.color = new Color(0, 0, 0, faedCount);
        }
    }
    IEnumerator Tutorialgo()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("TutorialMap");
        //yield return new WaitForSeconds(0.5f);
    }
}
