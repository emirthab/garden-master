using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerController : MonoBehaviour

{
    private GameObject hitObj;
    public GameObject makasimg;
    private Rigidbody makas;
    #region makas
    public float SpeedVal;
    private float speed;
    private Transform refOfCenter;
    private Vector3 mouseFirstPos, mouseLastPos, mouseDeltaPos;
    #endregion

    #region makasrotation
    private Vector3 v_diff;
    private float atan2;
    #endregion

    #region particle
    private GameObject particlePrefab;
    private float particleSize;// 0.7272f yaprağın %72.72 sine eşit olacak 
    #endregion
    [Header("Zorlanma Mekaniği")]
    public float fallAccelerator = 0.02f;//0.02
    public float fallTime = 0.06f;//0.6 ideal
    private GameObject Tree;
    private LeafEffects leafEffects;
    [Header("Yaprak Titreme Mekaniği")]
    public bool isVibrateActive;
    public float
                  normalX, normalY, normalZ = 4, normalTime = 2,
                  targetX, targetY, targetZ = 4, targetTime = 2,
                  treeX = 0.002f, treeY, treeZ, treeTime = 1;

    /* Information
     * fallTime=0.6f
     * fallAcclerator=0.02f
     * Normal-Z=4
     * Normal Time=2
     * Target-Z=4
     * Target Time=2
     * Tree-X=0.002f
     * Tree Time=1
     * Makas Transform Inf
     * Position->370,-796,0
     * Rotation->0,0,0
     * Scale->20,20,20
     * MakasImg Transform Inf
     * Position->0,0,0
     * Rotation->0,0,-150 [IMPORTANT]
     * Scale->3,788208,2,164813,0
     * */
    private void OnTriggerEnter(Collider col)
    {
        hitObj = col.transform.gameObject.GetComponent<Transform>().gameObject;

        switch (hitObj.tag)
        {
            case "normalleaf":
                drop_leaf();
                break;
            
            case "targetleaf":
                leafEffects = hitObj.GetComponent<LeafEffects>();
                vibrateLeaves();//TİTREŞİM KISMI
                leafEffects.time += fallAccelerator;
                if (leafEffects.time > fallTime)
                {
                    drop_leaf();
                }
                break;
                 }
        
    }

    void Start()
    {
        makas = GetComponent<Rigidbody>();
        speed = SpeedVal;
        refOfCenter = GameObject.FindGameObjectWithTag("refOfCenter").GetComponent<Transform>();
        Tree = GameObject.FindGameObjectWithTag("TreePrefab");
        particlePrefab = GameObject.FindGameObjectWithTag("leafParticle");
    }


    void Update()
    { 
        v_diff = (refOfCenter.position - transform.position);
        atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg);
       if (Input.GetMouseButton(0))
        {
            mouseLastPos = Input.mousePosition;
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                mouseDeltaPos = mouseLastPos - mouseFirstPos;
                transform.position += mouseDeltaPos * Time.deltaTime * speed;
            }

        }
        else
            mouseFirstPos = Input.mousePosition;
    }
    public void vibrateLeaves()
    {
        if (isVibrateActive)
        {
            iTween.ShakePosition(Tree, new Vector3(treeX, treeY, treeZ), treeTime);
            iTween.ShakeRotation(Tree, new Vector3(normalX, normalY, normalZ), normalTime); 
        }
    }
   public void drop_leaf()
    {
        GameObject particle = Instantiate(particlePrefab,hitObj.transform.position, hitObj.transform.rotation);
        ParticleSystem ps = particle.GetComponent<ParticleSystem>();
        particleSize = Tree.transform.localScale.y * 0.6f;//Particle ın boyut 
        //0.4 0.4 0.001 de gravityModifier ->0.2
        //0.6 0.6(A) 0.001 de gravityModifier->0.3(X) 
        //0.2xA/0.4=0.3(X)
        ps.gameObject.transform.localScale = new Vector3(particleSize, particleSize, 0.001f);
        float lastParticleSize = Tree.transform.localScale.y;
        particle.transform.position = hitObj.transform.position;
        ps.gravityModifier =  ps.gravityModifier+(ps.gravityModifier*particleSize/ (lastParticleSize*1.2f));
        ps.startColor = hitObj.transform.gameObject.GetComponent<SpriteRenderer>().color;
        ps.Play();      
        Destroy(hitObj.transform.gameObject);
    }

}
