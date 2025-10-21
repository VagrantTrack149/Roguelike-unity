using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float distanciaExpulsion = 10f; 
    public float duracionEmpuje = 0.5f;
    public Vida_enemy vida_Enemy;
    
    // Estados del enemigo
    private enum Estado { Patrulla, Sorprendido, Persiguiendo, Morir }
    private Estado estadoActual = Estado.Patrulla;
    private bool animacionMirarCompletada = false;
    public GameObject simbolo;
    //public Animator interrogacion;
    // Start is called before the first frame update

    void Start()
    {
        if (simbolo !=null)
        {
            
            Debug.Log("adios");
            simbolo.SetActive(false);  
        }
        
        //interrogacion.SetBool("Act", false);
        Ataque_Enemy = gameObject.GetComponent<Ataque_Enemy>();
        anima.SetBool("Caminar", false);
        anima.SetBool("Mirar", false);
        anima.SetBool("T_Mirar", false);
        anima.SetBool("Vida", false);
        vida_Enemy = gameObject.GetComponent<Vida_enemy>();
        agente = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (target == null)
        {
            EstadoPatrulla();
            return;
        }

        // Máquina de estados
        switch (estadoActual)
        {
            case Estado.Patrulla:
                EstadoPatrulla();
                //Debug.Log("Patrulla");
                break;
            case Estado.Sorprendido:
                EstadoSorprendido();
                //Debug.Log("Sorprendido");
                break;
            case Estado.Persiguiendo:
                EstadoPersiguiendo();
                //Debug.Log("Perseguir");
                break;
            case Estado.Morir:
                EstadoMorir();
                break;
            default:
                EstadoPatrulla();
                break;
        }
    }

    private void EstadoPatrulla()
    {
        agente.speed = vel_caminar;
        anima.SetBool("Caminar", true);
        anima.SetBool("Mirar", false);
        anima.SetBool("T_Mirar", false);

        // Comportamiento de patrulla aleatoria
        crono += Time.deltaTime;
        if (crono >= 3f)
        {
            rutina = Random.Range(0, 2);
            crono = 0;
        }

        switch (rutina)
        {
            case 0:
                // Esperar en lugar
                if (visto)
                {
                    estadoActual = Estado.Sorprendido;
                    animacionMirarCompletada = false;
                }
                break;
            case 1:
                // Caminar en dirección aleatoria
                grado = Random.Range(0, 360);
                angulo = Quaternion.Euler(0, grado, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                transform.Translate(Vector3.forward * vel_caminar * Time.deltaTime);
                break;
        }

        // Verificar si ve al jugador
        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= radio_vision)
        {
            estadoActual = Estado.Sorprendido;
            visto = true;
            animacionMirarCompletada = false;
        }
    }

    private void EstadoSorprendido()
    {
        if (simbolo !=null)
        {
            Debug.Log("Hola");
            simbolo.SetActive(true);
        }
        
        //interrogacion.SetBool("Act", true);
        agente.isStopped = true;
        anima.SetBool("Caminar", false);
        anima.SetBool("Mirar", true);
        anima.SetBool("T_Mirar", false);

        // Mirar hacia el jugador
        var lookPlayer = target.transform.position - transform.position;
        lookPlayer.y = 0;
        var rotation = Quaternion.LookRotation(lookPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);

        // Esperar a que termine la animación de "Mirar" antes de pasar a perseguir
        if (!animacionMirarCompletada)
        {
            // Usar un temporizador simple para simular el fin de la animación
            StartCoroutine(EsperarAnimacionMirar());
            
        }
    }

    private IEnumerator EsperarAnimacionMirar()
    {
        // Esperar el tiempo aproximado de la animación Mirar
        yield return new WaitForSeconds(1f);
        
        animacionMirarCompletada = true;
        estadoActual = Estado.Persiguiendo;
        agente.speed = vel_correr;
        if (agente.isActiveAndEnabled && agente.isOnNavMesh)
        {
            agente.isStopped = false;
        }
        if (simbolo)
        {
            simbolo.SetActive(false);
        }
    }

    private void EstadoPersiguiendo()
    {
        anima.SetBool("Caminar", false);
        anima.SetBool("Mirar", false);
        anima.SetBool("T_Mirar", true);

        // Perseguir al jugador
        agente.SetDestination(target.transform.position);

        // Verificar si el jugador se alejó demasiado
        if (Vector3.Distance(transform.position, target.transform.position) > radio_vision_visto)
        {
            estadoActual = Estado.Patrulla;
            visto = false;
            return;
        }

        // Determinar distancia de ataque según el tipo
        float distanciaAtaque = (float)(tipo.tipo == 1 ? 3 : 2);

        // Verificar si está en rango para atacar
        if (Vector3.Distance(transform.position, target.transform.position) <= distanciaAtaque)
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                estadoActual = Estado.Patrulla;
                StartCoroutine(EjecutarAtaque());
            }
        }
    }

    private IEnumerator EjecutarAtaque()
    {
        agente.isStopped = true;
        
        // Mantener la animación T_Mirar durante el ataque
        anima.SetBool("T_Mirar", true);

        // Ejecutar el ataque según el tipo
        if (tipo.tipo == 1 || tipo.tipo == 2)
        {
            Ataque_Enemy.Atacar(3);
            StartCoroutine(EmpujarPlayer(target.transform));
        }
        else if (tipo.tipo == 3)
        {
            aparecer_ataque = true;
            Ataque_Enemy.Atacar(3);
        }

        lastAttackTime = Time.time;

        // Esperar un momento para que se complete la animación de ataque
        yield return new WaitForSeconds(0.2f);

        // Pasar a estado de recuperación
        estadoActual = Estado.Patrulla;
    }


    IEnumerator EmpujarPlayer(Transform player)
    {
        if (player == null) yield break;
        //Debug.Log(player.position);
        float Y= player.position.y;
        Vector3 direccion = (player.position - transform.position).normalized;
        Vector3 posicionFinal = player.position + direccion * distanciaExpulsion;
        posicionFinal.y=Y;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracionEmpuje)
        {
            if (player == null) yield break;
            
            player.position = Vector3.Lerp(player.position, posicionFinal, tiempoTranscurrido / duracionEmpuje);
            tiempoTranscurrido += Time.deltaTime;
            RaycastHit hit;
            if (Physics.Raycast(player.position, direccion, out hit, (posicionFinal - player.position).magnitude))
            {
                if (hit.collider.gameObject != gameObject) 
                {
                    posicionFinal = hit.point - direccion * 0.2f; 
                }
            }
            yield return null;
        }
    }
    private void EstadoMorir(){
        // Verificar si está muerto
        if (vida_Enemy.VidaActual <= 0)
        {
            
            agente.isStopped = true;
            vida_Enemy.muerte_funcion();
        }
    }
}