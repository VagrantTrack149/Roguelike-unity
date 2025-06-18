using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque_Enemy : MonoBehaviour
{
    public int damage = 3;
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
    public void Atacar()
    {
        Vida_Jugador Vida = Player.GetComponent<Vida_Jugador>();
        Vida.Daño(damage);
    }
}
