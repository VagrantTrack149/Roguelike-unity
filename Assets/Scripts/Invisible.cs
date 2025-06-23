using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : MonoBehaviour
{
    public float invisibleDuration = 2f; 
    public float cooldownDuration = 6f; 
    public float time = 0;
    public bool isInvisible = false;
    public bool inCooldown = false;
    public GameObject gg;
    public Renderer render;
    public Material originalMaterial; //material original
    public Material invisibleMaterial; //material a cambiar
    public Controles controles;
    
    void Start()
    {
        controles = GameObject.Find("Controles").GetComponent<Controles>();
        render = gg.GetComponent<Renderer>();
        originalMaterial = render.material;
    }

    void Update()
    {
        if (Input.GetKeyDown(controles.Invisible) && !isInvisible && !inCooldown)
        {
            StartInvisibility();
        }
        
        if (isInvisible)
        {
            time += Time.deltaTime;
            if (time >= invisibleDuration)
            {
                EndInvisibility();
                StartCooldown();
            }
        }
        
        if (inCooldown)
        {
            time += Time.deltaTime;
            if (time >= cooldownDuration)
            {
                inCooldown = false;
                time = 0;
            }
        }
    }
    
    void StartInvisibility()
    {
        gameObject.tag = "Poseido";
        if (render != null && invisibleMaterial != null)
        {
            render.material = invisibleMaterial; 
        }
        isInvisible = true;
        time = 0;
    }
    
    void EndInvisibility()
    {
        gameObject.tag = "Player";
        if (render != null && originalMaterial != null)
        {
            render.material = originalMaterial;
        }
        isInvisible = false;
        time = 0;
    }
    
    void StartCooldown()
    {
        inCooldown = true;
        time = 0;
    }
}
