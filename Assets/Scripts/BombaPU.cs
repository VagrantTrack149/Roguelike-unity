using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaPU : MonoBehaviour
{
    public GameObject bombaPrefab;
    public Animator anima;
    public GameObject player;
    public float count;
    public float offsetDistance = 2f;
    public Controles controles;
    void Start()
    {
        controles = GameObject.Find("Controles").GetComponent<Controles>();
    }
    void Update()
    { 
        Vector3 adjustedPosition=player.transform.position+player.transform.forward * offsetDistance;
        count+= Time.deltaTime;
        if (count >= 1)
        {
            if (Input.GetKeyDown(controles.Bomb))
            {
                Instantiate(bombaPrefab, adjustedPosition, player.transform.rotation);
                count = 0;
                anima.SetBool("Ataque", true);
            }
        }
        else
        {
            anima.SetBool("Ataque", false);
        }     
          
    }
}
