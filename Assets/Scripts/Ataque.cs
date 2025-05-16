using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    public GameObject arma;
    public GameObject player;
    public float offsetDistance = 0.8f;
    public float count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 adjustedPosition=player.transform.position+player.transform.forward * offsetDistance;
        count+= Time.deltaTime;
        if (count>=1){
            if (Input.GetKeyDown(KeyCode.R)){
                GameObject armaGen=Instantiate(arma, adjustedPosition, player.transform.rotation);
                armaGen.transform.parent=player.transform;
                count=0;
            }
        }
    }
}