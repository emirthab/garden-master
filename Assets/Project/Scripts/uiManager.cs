using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class uiManager : MonoBehaviour
{
    #region Paneller
    public GameObject panel_settings,panel_levelmaps,panel_main;
    #endregion
    public Text text_coin;
    private DontDestroy dds;

    public void buttons(string name)
    {
        if (name == "btn_upgrade")
        {
            Debug.Log("UPGRADE BTN");
        }
        else if (name == "btn_levelmap")
        {
            Debug.Log("LEVELMAP");
            panel_main.SetActive(false);
            panel_levelmaps.SetActive(true);
            panel_settings.SetActive(false);
        }
        else if (name == "btn_settings")
        {
            Debug.Log("SETTİNGS");
            panel_main.SetActive(false);
            panel_levelmaps.SetActive(false);
            panel_settings.SetActive(true);
        }
        else if (name == "btn_noads")
        {
            Debug.Log("NO_ADS");
        }
        else if (name == "btn_start")
        {
            Debug.Log("START");
            SceneManager.LoadSceneAsync(1);
        }
        else if (name == "btn_gomain")
        {
            Debug.Log("GO MAİN");
            panel_levelmaps.SetActive(false);
            panel_settings.SetActive(false);
            panel_main.SetActive(true);
        }
        else
            Debug.Log("Hiçbişi olmadı");
    }
    void Start()
    {
        dds = GameObject.FindGameObjectWithTag("ddsobj").GetComponent<DontDestroy>();
    }
    
   
    void Update()
    {
        text_coin.text = "Coin:"+dds.coin;
    }
}
