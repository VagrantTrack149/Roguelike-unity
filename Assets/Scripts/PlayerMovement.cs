using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator anima;
    public CharacterController controller;
    public Transform cam;
    public float speed = 10;
    public float turnSmoothTime=0.2f;
    public float turnSmoothVelocity=0.1f;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float gravity = -9.81f;
    private Vector3 velocity;
    public bool muerte=false;
    public GameObject respawn;
    private Rigidbody rb;
    public float count;
    public Vida_Jugador Vida;
    public bool Movement = true;
    public bool canSprint = true;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        Vida=GetComponent<Vida_Jugador>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Movement){
            controller.enabled = true;
            //speed = 10;
            muerte = false;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
            if (direction.magnitude >= 0.01f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
                anima.SetBool("Caminar", true);
                anima.SetBool("Invisible", false);
                anima.SetBool("Ataque_rap", false);
                anima.SetBool("Ataque", false);
                
                /*count += Time.deltaTime;
                if (count >= 2)
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        speed = 20;
                    }
                    if (Input.GetKeyUp(KeyCode.LeftShift))
                    {
                        speed = 10;
                    }
                }
                else
                {
                    speed = 10;
                }*/
                //Debug.Log();
                if (Input.GetKey(KeyCode.LeftShift) && canSprint)
                {
                    count += Time.deltaTime;
                    if (count <= 3)
                    {
                        speed = 20;
                    }
                    else
                    {
                        speed = 10;
                        canSprint = false;
                    }
                }
                else
                {
                    if (Input.GetKeyUp(KeyCode.LeftShift))
                    {
                        count = 0;
                    }
                    speed = 10;
                    if (!canSprint)
                    {
                        count += Time.deltaTime;
                        if (count >= 6)
                        {
                            count = 0;
                            canSprint = true;
                        }
                    }
                }
            }
            else
            {
                anima.SetBool("Caminar", false);
            }
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            if (muerte)
            {

                HandleRespawn();
                return;
            }
        }
        else
        {
            speed = 0f;
            velocity = Vector3.zero;
            controller.enabled = false;
            transform.position = transform.position;
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            anima.SetBool("Caminar", false);
            Debug.Log("Bloqueado");
        }

    }

    public void HandleRespawn()
    {
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        speed = 0f;
        velocity = Vector3.zero;
        controller.enabled = false;
        transform.position = respawn.transform.position;
        controller.enabled = true;
        speed = 10;
        muerte = false;
        Vida.VidaActual = 10;
    }
}

