using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoIA : MonoBehaviour
{
    public int rutina;
    public float crono;
    public Quaternion angulo;
    public float grado;
    public int vel_caminar = 3;
    public int vel_correr = 10;
    public GameObject target;
    public NavMeshAgent agente;
    public float distancia_ataque;
    public float radio_vision = 8;
    public float radio_vision_visto = 16;
    public bool visto = false;
    public Ataque_Enemy Ataque_Enemy;
    public float lastAttackTime;
    public float attackCooldown = 3f;
    public Animator anima;
    public bool aparecer_ataque = false;
    public bool recuperar = false;
    public tipo_enemy tipo;
    public float distanciaExpulsion = 1f; 
    public float duracionEmpuje = 0.5f;
    void Start()
    {
        Ataque_Enemy = gameObject.GetComponent<Ataque_Enemy>();
        anima.SetBool("Walk", false);
        anima.SetBool("Atack", false);
        anima.SetBool("Run", false);

    }

    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (target == null)
        {
            Comportamiendo_ene(3, vel_caminar);
        }
        else
        {
            mirar_player();
        }
    }

    public void mirar_player()
    {
        if (visto)
        {
            if (Vector3.Distance(transform.position, target.transform.position) > radio_vision_visto)
            {
                visto = false;
                Comportamiendo_ene(3, vel_caminar);
                return;
            }
        }

        if (Vector3.Distance(transform.position, target.transform.position) > radio_vision)
        {
            visto = false;
            Comportamiendo_ene(3, vel_caminar);

        }
        else
        {
            visto = true;
            var lookplayer = target.transform.position - transform.position;
            lookplayer.y = 0;
            var rotation = Quaternion.LookRotation(lookplayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);

            agente.enabled = true;
            agente.SetDestination(target.transform.position);
            if (agente.enabled == true)
            {
                anima.SetBool("Run", true);
            }
            int distancia=5;
            if (tipo.tipo == 1)
            {
                distancia = 4;
            }
            if (tipo.tipo == 2)
            {
                distancia = 6;
            }
            if (Vector3.Distance(transform.position, target.transform.position) <= distancia)
            {
                vel_caminar = 0;
                vel_correr = 0;
                Debug.Log("Cerca");
                agente.isStopped = true;
                agente.enabled = false;
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    recuperar = false;
                    Debug.Log("Ataque");
                    //subir velocidad de animación de ataque si tipo.tipo = 1
                    // añadir
                    anima.SetBool("Atack", true);
                    anima.SetBool("Walk", false);
                    anima.SetBool("Run", false);
                    lastAttackTime = Time.time;


                    if (tipo.tipo == 1)
                    {
                        Ataque_Enemy.Atacar(3);
                        StartCoroutine(EmpujarPlayer(target.transform));

                    }
                    if (tipo.tipo == 2)
                    {
                        aparecer_ataque = true;
                    }
                }

            }
            else
            {
                aparecer_ataque = false;
                vel_caminar = 3;
                vel_correr = 10;
                anima.SetBool("Atack", false);
                agente.isStopped = false;
                agente.enabled = true;
                anima.SetBool("Run", false);
                anima.SetBool("Walk", false);
                recuperar = true;
            }
        }
    }

    public void Comportamiendo_ene(int tiempo, int caminar)
    {
        agente.enabled = false;
        crono += Time.deltaTime;
        if (crono >= tiempo)
        {
            rutina = Random.Range(0, 2);
            crono = 0;
        }
        switch (rutina)
        {
            case 0:
                break;
            case 1:
                grado = Random.Range(0, 360);
                angulo = Quaternion.Euler(0, grado, 0);
                rutina++;
                anima.SetBool("Run", false);
                anima.SetBool("Walk", false);
                break;
            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                transform.Translate(Vector3.forward * caminar * Time.deltaTime);
                if (visto)
                {
                    anima.SetBool("Run", true);
                }
                else
                {
                    anima.SetBool("Walk", true);
                }
                if (target != null && Vector3.Distance(transform.position, target.transform.position) <= 3)
                {
                    Debug.Log("Cerca");
                    visto = true;
                }
                break;
        }
    }

    public void empujar(Transform player)
    {
        //Vector3 direccion= (player.position-transform.position).normalized;

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