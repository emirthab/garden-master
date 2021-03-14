using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    #region Datas
    public int coin,star;
    #endregion
    private void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        coin = PlayerPrefs.GetInt("Coin");
        star = PlayerPrefs.GetInt("Star");
    }
    private void Update()
    {
        PlayerPrefs.SetInt("Coin",coin);
        PlayerPrefs.SetInt("Star",star);
        PlayerPrefs.Save();
    }
}
