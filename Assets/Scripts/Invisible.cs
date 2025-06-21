using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : MonoBehaviour
{
    public float time=0;
    public bool f= false;
    public GameObject gg;
    public Renderer render;
    public Material mat; //material original
    public Material mat1; //material a cambiar
    public Color col;
    public Controles controles;
    // Start is called before the first frame update
    void Start()
    {
        controles = GameObject.Find("Controles").GetComponent<Controles>();
        render=gg.GetComponent<Renderer>();
        mat=render.material;
        //col=mat.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(controles.Invisible) && !f){
            gameObject.tag="Poseido";
            //col.a=0f;
            if (render != null && mat1 != null){
                render.material = mat1; 
            }
            f=true;
        }
        if (f){
            time+=Time.deltaTime;
            if (time>=2){
                gameObject.tag ="Player";
                //col.a = 1f;
                if (render != null && mat != null){
                    render.material = mat; 
                }
                f=false;    
                time=0;
            }       
        }
    }
}
