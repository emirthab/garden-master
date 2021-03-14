using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafTrigger : MonoBehaviour
{
    
    public PlayerController pc;
    public Rigidbody ScissorsRb;
    
    private GameObject hitObj;
    public float fallAccelerator = 0.02f;//0.02
    public float fallTime = 0.06f;//0.6 ideal
    private LeafEffects leafEffects;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        hitObj = col.transform.gameObject.GetComponent<Transform>().gameObject;
        if (hitObj.CompareTag("normalleaf"))
        {
            pc.drop_leaf();
        }
        else if (hitObj.CompareTag("targetleaf"))
        {

            /*leafEffects = hitObj.GetComponent<LeafEffects>();
            pc.vibrateLeaves();
            leafEffects.time += fallAccelerator;
            if (leafEffects.time > fallTime)
            {
                pc.drop_leaf();
            }*/
        }
    }

}
