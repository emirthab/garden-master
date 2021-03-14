using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject[] objects;
    public GameObject upgradeButton, levelMapPanel;
    public int objectPriority = 0;
    public Animator levelMapMenuAnimator;
    
    [Header("In Game")] 
    public GameObject[] normal_leaves, start_normal_leaves /* TARGET LEAF: */, target_leaves, start_target_leaves;
    public GameObject CompleteMenu;
    public float total_leaf, final_leaf_value, cutted_leaf, total_target_leaf, cutted_target_leaf, final_targetleaf_value, final_normalleaf_value;
    public Slider progressBar, completeMenuProgressBar;
    public Animator PanelKarartma, CompleteMenuAnimator;
    public GameObject playingSectionPnl;
    void Start()
    {
        ingame_start();
        mainmenu_start();
    }
    
    private void Update()
    {
        mainmenu_update();
        ingame_update();
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    // BEGIN: MainMenu

    private void mainmenu_update()
    {
        // Update() içerisine gelen, fakat MainMenu scene'i için geçerli olan fonksiyon.
        if (SceneManager.GetActiveScene().buildIndex > 0) return;
    }

    private void mainmenu_start()
    {
        // Start() içerisine gelen, fakat MainMenu scene'i için geçerli olan fonksiyon.
        if (SceneManager.GetActiveScene().buildIndex > 0) return;
    }
    public void buttons_event(string name)
    {
        switch (name)
        {
            case "btn_start":
                Debug.Log("Start btn");
                SceneManager.LoadSceneAsync(1,LoadSceneMode.Single);
                break;
            
            case "btn_upgrade":
                if (objectPriority >= objects.Length)
                {
                    upgradeButton.SetActive(false);
                    return;
                }

                objects[objectPriority].SetActive(true);

                objectPriority++;
                break;
            
            case "btn_levelmap":
                levelMapMenuAnimator.SetBool("playAnim", true);
                break;
            
            
            case "btn_star":
                break;
            case "btn_backmain":
                Debug.Log("mainmenu back btn");
                //POYRAZ BURAYA PLAY ANİMİN TAM TERSİNİ YAPACAK
                break;
        }
        
    }
    
   
    
    // END: MainMenu
    
    
    
    
    
    // BEGIN: CompleteMenu

    public void cm_buttons(string name)
    {
        switch (name)
        {
            case "btn_nothanks":
                SceneManager.LoadScene(0, LoadSceneMode.Single);
                break;
        }
    }
    
    // END: CompleteMenu
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    // BEGIN: InGame

    private void ingame_update()
    {
        // Update() içerisine gelen, fakat MainMenu harici gerçekleşen oyun içi (ingame) fonksiyonu.
        if (SceneManager.GetActiveScene().buildIndex <= 0) return;
        
        normal_leaves = GameObject.FindGameObjectsWithTag("normalleaf");
        target_leaves = GameObject.FindGameObjectsWithTag("targetleaf");
        count_leaf_value();
        if(playingSectionPnl==null)
            playingSectionPnl = GameObject.FindGameObjectWithTag("PlayingSection");

        if (final_normalleaf_value >= 100)//Progress 100 oldugunda 
        {
            PanelKarartma.SetBool("playAnim", true);
            CompleteMenuAnimator.SetBool("playAnim", true);
            completeMenuProgressBar.value = progressBar.value;
            if (playingSectionPnl.activeSelf)
                playingSectionPnl.SetActive(false);
        }
    }

    private void ingame_start()
    {
        // Start() içerisine gelen, fakat MainMenu harici gerçekleşen oyun içi (ingame) fonksiyonu.
        if (SceneManager.GetActiveScene().buildIndex <= 0) return;
        
        start_normal_leaves = GameObject.FindGameObjectsWithTag("normalleaf");
        start_target_leaves = GameObject.FindGameObjectsWithTag("targetleaf");
        playingSectionPnl = GameObject.FindGameObjectWithTag("PlayingSection");
        total_leaf = start_normal_leaves.Length;
        total_target_leaf = start_target_leaves.Length;
    }
    
    public void count_leaf_value()
    {
        // Normal Leaf
        cutted_leaf = total_leaf - normal_leaves.Length;
        final_normalleaf_value = (cutted_leaf / total_leaf) * 100;
        final_leaf_value = final_normalleaf_value;


        // Target Leaf
        cutted_target_leaf = total_target_leaf - target_leaves.Length;
        final_targetleaf_value = (cutted_target_leaf / total_target_leaf) * 100;
        
        
        
        final_leaf_value -= final_targetleaf_value;
        progressBar.value = final_leaf_value / 100;

    }
    
    // END: InGame
    
    
}
