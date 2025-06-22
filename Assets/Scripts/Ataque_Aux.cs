using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ataque_Aux : MonoBehaviour
{
    public float count;
    public float fuerzaExpulsion=50;
    public GameObject player;
    public float distanciaExpulsion = 10f; 
    public float duracionEmpuje = 0.5f;
    public Vida_enemy vida_Enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count+=Time.deltaTime;
        if (count>=1){
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other) {
        Rigidbody rb =other.GetComponent<Rigidbody>();
        if (other.CompareTag("Enemy")){
            vida_Enemy=other.GetComponent<Vida_enemy>();
            StartCoroutine(EmpujarEnemigo(other.transform));
            vida_Enemy.Daño(5);
        }
    }
    void OnTriggerStay(Collider other) {
         Rigidbody rb =other.GetComponent<Rigidbody>();
        if (other.CompareTag("Enemy")){
            vida_Enemy=other.GetComponent<Vida_enemy>();
            //vida_Enemy.Daño(5);
            StartCoroutine(EmpujarEnemigo(other.transform));
        }
    }
    IEnumerator EmpujarEnemigo(Transform enemigo)
    {
         if (enemigo == null){
            yield break; 
        }
        Vector3 direccion = (enemigo.position - player.transform.position).normalized;
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
