using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vida_Jugador : MonoBehaviour
{
    public int MaxVida=10;
    public int VidaActual;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        VidaActual = MaxVida;
        Player=GameObject.Find("Player");
    }

    public void Daño(int damage){
        VidaActual=VidaActual-damage;
        Debug.Log(VidaActual);
        if (VidaActual <= 0)
        {
            Debug.Log("Muerto");
            PlayerMovement PlayMode = Player.GetComponent<PlayerMovement>();
            PlayMode.HandleRespawn();
        }
    }
}
