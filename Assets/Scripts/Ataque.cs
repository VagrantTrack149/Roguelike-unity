using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    public GameObject arma;
    public GameObject player;
    public float offsetDistance = 1.2f;
    public float count;
    public Controles controles;
    public Animator anima;

    // Start is called before the first frame update
    void Start()
    {
        controles = GameObject.Find("Controles").GetComponent<Controles>();
        player =GameObject.Find("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 adjustedPosition=player.transform.position+player.transform.forward * offsetDistance;
        count+= Time.deltaTime;
        if (count >= 1)
        {
            if (Input.GetKeyDown(controles.Attack) && player.tag != "Enemy")
            {
                anima.SetBool("Ataque", true);
                GameObject armaGen = Instantiate(arma, adjustedPosition, player.transform.rotation);
                armaGen.transform.parent = player.transform;
                count = 0;
            }
        }
        else
        {
            anima.SetBool("Ataque", false);
        }
    }
}