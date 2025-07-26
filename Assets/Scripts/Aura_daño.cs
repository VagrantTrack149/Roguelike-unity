using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Aura_daño : MonoBehaviour
{
    public float fuerzaEmpuje = 5f;
    public Ataque_Enemy ataque_Enemy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ataque_Enemy.Atacar(3);
            //Empujar aqui
            Empujar(other);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ataque_Enemy.Atacar(1);
        }
    }

    void Empujar(Collider jugador)
    {
        Rigidbody rb = jugador.GetComponent<Rigidbody>();
        Vector3 direccionEmpuje = (jugador.transform.position - transform.position).normalized;
        rb.AddForce(direccionEmpuje * fuerzaEmpuje, ForceMode.Impulse);
    }
}
