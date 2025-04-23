using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;    
    public Vector3 offset= new Vector3(3, 28,-18);
    // Start is called before the first frame update
    private void LateUpdate() {
        transform.position=target.position+offset;    
    }
}
