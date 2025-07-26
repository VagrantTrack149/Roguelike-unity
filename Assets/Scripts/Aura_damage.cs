using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Aura_damage : MonoBehaviour
{
    public float fuerzaEmpuje = 5f;
    public Ataque_Enemy ataque_Enemy;
    public GameObject Player;
    public float distanciaExpulsion = 1f; 
    public float duracionEmpuje = 0.5f;
    public Vida_Jugador vida_Jugador;
    public float tiempo_dentro = 0;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        vida_Jugador = Player.GetComponent<Vida_Jugador>();
        ataque_Enemy=gameObject.GetComponent<Ataque_Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            //Debug.Log("" + other.gameObject.name);
            ataque_Enemy.Atacar(3);
            //Empujar aqui
            Empujar(other);
            StartCoroutine(EmpujarPlayer(other.transform));
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            //Debug.Log("" + other.gameObject.name);
            tiempo_dentro += Time.deltaTime;
            if (tiempo_dentro > 1f)
            {
                ataque_Enemy.Atacar(1);
                tiempo_dentro = 0;    
            }
            
        }
    }

    void Empujar(Collider jugador)
    {
        Rigidbody rb = jugador.GetComponent<Rigidbody>();
        Vector3 direccionEmpuje = (jugador.transform.position - transform.position).normalized;
        rb.AddForce(direccionEmpuje * fuerzaEmpuje, ForceMode.Impulse);
    }
    IEnumerator EmpujarPlayer(Transform enemigo)
    {
         if (enemigo == null){
            yield break; 
        }
        Vector3 direccion = (enemigo.position - transform.position).normalized;
        //Vector3 direccion = player.transform.position;

        Vector3 posicionFinal = enemigo.position + direccion * distanciaExpulsion;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracionEmpuje)
        {
            if (enemigo == null){
                yield break;
            }
            enemigo.position = Vector3.Lerp(enemigo.position, posicionFinal, tiempoTranscurrido/duracionEmpuje);
            tiempoTranscurrido += Time.deltaTime;
            yield return null; 
            
        }
    }
}
