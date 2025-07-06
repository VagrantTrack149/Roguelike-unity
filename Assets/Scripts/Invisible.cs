using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : MonoBehaviour
{
    public Animator anima;
    public float invisibleDuration = 3f; 
    public float cooldownDuration = 6f; 
    public float time = 0;
    public bool isInvisible = false;
    public bool inCooldown = false;
    public bool isAnimationPlaying = false;
    public GameObject gg;
    public Controles controles;
    
    private List<Material[]> originalMaterials = new List<Material[]>();
    private List<Renderer> childRenderers = new List<Renderer>();
    private Coroutine currentCoroutine;
    
    void Start()
    {
        controles = GameObject.Find("Controles").GetComponent<Controles>();
        GetAllRenderersInChildren();
    }

    void GetAllRenderersInChildren()
    {
        childRenderers.Clear();
        originalMaterials.Clear();
        
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);
        
        foreach (Renderer renderer in renderers)
        {
            childRenderers.Add(renderer);
            
            Material[] matsCopy = new Material[renderer.materials.Length];
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                matsCopy[i] = new Material(renderer.materials[i]);
            }
            originalMaterials.Add(matsCopy);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(controles.Invisible) && !isInvisible && !inCooldown && !isAnimationPlaying)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(InvisibilityRoutine());
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
    
    IEnumerator InvisibilityRoutine()
    {
        // Iniciar invisibilidad
        StartInvisibility();
        anima.SetBool("Invisible", true);
        isAnimationPlaying = true;
        
        // Esperar duración de invisibilidad
        yield return new WaitForSeconds(invisibleDuration);
        
        // Terminar invisibilidad
        EndInvisibility();
        
        // Esperar a que termine la animación
        while (anima.GetCurrentAnimatorStateInfo(0).IsName("InvisibleAnimation")) // Reemplaza "InvisibleAnimation" con el nombre de tu animación
        {
            yield return null;
        }
        
        anima.SetBool("Invisible", false);
        isAnimationPlaying = false;
        
        // Iniciar cooldown
        StartCooldown();
    }
    
    void StartInvisibility()
    {
        gameObject.tag = "Poseido";
        
        for (int i = 0; i < childRenderers.Count; i++)
        {
            Renderer renderer = childRenderers[i];
            Material[] mats = renderer.materials;
            
            for (int j = 0; j < mats.Length; j++)
            {
                if (mats[j].HasProperty("_Color"))
                {
                    Color originalColor = mats[j].color;
                    Color newColor = originalColor;
                    
                    newColor.a = 0.3f;
                    newColor.r *= 0.5f;
                    newColor.g *= 0.5f;
                    newColor.b *= 0.5f;
                    
                    mats[j].color = newColor;
                }
            }
        }
        
        isInvisible = true;
        time = 0;
    }
    
    void EndInvisibility()
    {
        gameObject.tag = "Player";
        
        for (int i = 0; i < childRenderers.Count; i++)
        {
            Renderer renderer = childRenderers[i];
            Material[] originalMats = originalMaterials[i];
            Material[] currentMats = renderer.materials;
            
            if (originalMats.Length == currentMats.Length)
            {
                for (int j = 0; j < currentMats.Length; j++)
                {
                    if (currentMats[j].HasProperty("_Color") && originalMats[j].HasProperty("_Color"))
                    {
                        currentMats[j].color = originalMats[j].color;
                    }
                }
                
                renderer.materials = currentMats;
            }
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
