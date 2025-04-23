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
    public float vel_caminar=3;
    public float vel_correr=5;
    public GameObject target;
    public NavMeshAgent agente;
    public float distancia_ataque;
    public float radio_vision=8;

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
        if (Vector3.Distance(transform.position, target.transform.position)>radio_vision){
            Comportamiendo_ene();
        }else{
            var lookplayer= target.transform.position-transform.position;
            lookplayer.y=0;
            var rotation = Quaternion.LookRotation(lookplayer);
            agente.enabled=true;
            agente.SetDestination(target.transform.position);

        }
    }

    public void Comportamiendo_ene(){
        agente.enabled=false;
        crono+=Time.deltaTime;
        if (crono>=3){
            rutina=Random.Range(0, 2);
            crono=0;
        }
        switch (rutina)
        {
            case 0:
                Debug.Log("Quieto");
                break;
            case 1:
                grado=Random.Range(0, 360);
                angulo=Quaternion.Euler(0, grado,0);
                rutina++;
                break;
            case 2:
                transform.rotation=Quaternion.RotateTowards(transform.rotation,angulo,0.5f);
                transform.Translate(Vector3.forward*vel_caminar*Time.deltaTime);

                break;
        }

    }
}
