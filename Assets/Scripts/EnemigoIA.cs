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
    public int vel_caminar=3;
    public int vel_correr=10;
    public GameObject target;
    public NavMeshAgent agente;
    public float distancia_ataque;
    public float radio_vision=8;
    public float radio_vision_visto=16;
    public bool visto=false;
    public Ataque_Enemy Ataque_Enemy;
    public float lastAttackTime;
    public float attackCooldown = 1f;

    void Start()
    {
        Ataque_Enemy = gameObject.GetComponent<Ataque_Enemy>();
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

            if (Vector3.Distance(transform.position, target.transform.position) <= 3)
            {
                Debug.Log("Cerca");
                agente.isStopped = true;
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    Debug.Log("Ataque");
                    Ataque_Enemy.Atacar();
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                agente.isStopped = false;
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
                break;
            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                transform.Translate(Vector3.forward * caminar * Time.deltaTime);

                if (target != null && Vector3.Distance(transform.position, target.transform.position) <= 3)
                {
                    Debug.Log("Cerca (during wander)");
                    visto = true;
                }
                break;
        }
    }
}