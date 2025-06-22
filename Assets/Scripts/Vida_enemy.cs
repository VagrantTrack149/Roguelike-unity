using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vida_enemy : MonoBehaviour
{
    public int MaxVida=10;
    public int VidaActual;

    // Start is called before the first frame update
    void Start()
    {
        VidaActual = MaxVida;
    }

    public void Daño(int damage){
        VidaActual=VidaActual-damage;
        Debug.Log(VidaActual);
        if (VidaActual <= 0)
        {
           Destroy(gameObject); 
        }
    }
}
