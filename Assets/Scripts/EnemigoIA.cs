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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mirar_player();
    }
    public void mirar_player(){
        if (visto){
            if (Vector3.Distance(transform.position, target.transform.position)>radio_vision_visto){
                Comportamiendo_ene(1,vel_correr);
            }
        }
        if (Vector3.Distance(transform.position, target.transform.position)>radio_vision){
            Comportamiendo_ene(3,vel_caminar);
        }else{
            visto=true;
            var lookplayer= target.transform.position-transform.position;
            lookplayer.y=0;
            var rotation = Quaternion.LookRotation(lookplayer);
            agente.enabled=true;
            agente.SetDestination(target.transform.position);

        }
    }

    public void Comportamiendo_ene(int tiempo, int caminar){
        agente.enabled=false;
        crono+=Time.deltaTime;
        if (crono>=tiempo){
            rutina=Random.Range(0, 2);
            crono=0;
        }
        switch (rutina)
        {
            case 0:
                //Debug.Log("Quieto");
                break;
            case 1:
                grado=Random.Range(0, 360);
                angulo=Quaternion.Euler(0, grado,0);
                rutina++;
                break;
            case 2:
                transform.rotation=Quaternion.RotateTowards(transform.rotation,angulo,0.5f);
                transform.Translate(Vector3.forward*caminar*Time.deltaTime);

                break;
        }

    }
}
