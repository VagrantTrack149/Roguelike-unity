using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vida_enemy : MonoBehaviour
{
    public int MaxVida=10;
    public int VidaActual;
    public Animator anima;


    // Start is called before the first frame update
    void Start()
    {
        VidaActual = MaxVida;
        anima=gameObject.transform.GetChild(0).GetComponent<Animator>();
        
    }

    public void Daño(int damage){
        VidaActual=VidaActual-damage;
        Debug.Log(VidaActual);
        if (VidaActual <= 0)
        {  
            Destroy(gameObject);   
        }
    }
    public void muerte_funcion(){
        StartCoroutine(muerte_animacion());
    }
    public IEnumerator muerte_animacion(){
        anima.SetBool("Vida", true);
        Debug.Log("Muerto");
        yield return new WaitForSeconds(1.7f);
        Daño(MaxVida);
    }
}
