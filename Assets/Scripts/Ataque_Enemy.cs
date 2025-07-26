using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque_Enemy : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player=GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Atacar(int damage)
    {
        if (Player != null)
        {
            Vida_Jugador Vida = Player.GetComponent<Vida_Jugador>();
            Vida.Daño(damage);    
        }
        
    }
}
