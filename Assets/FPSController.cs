using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    CharacterController characterController;
    public bool useFootsteps = true;
    public bool isWalking = true;
    public bool isRunning = true;
    [Header("Opciones de personaje")]
    public float walkSpeed = 6.0f;
    public float runSpeed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public AudioSource footstepAudioSource = default;
    public AudioClip[] woodClips = default;
    public AudioClip[] grassClips = default;
    public float footstepTimer = 1.0f;
    [Header("Opciones de camara")]
    public Camera cam;
    public float mouseHorizontal = 3.0f;
    public float mouseVertical = 2.0f;



    public float minRotation = -65.0f;
    public float maxRotation = 60.0f;
    float h_mouse, v_mouse;


    private Vector3 move = Vector3.zero;
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        h_mouse = mouseHorizontal * Input.GetAxis("Mouse X");
        v_mouse += mouseVertical * Input.GetAxis("Mouse Y");

        v_mouse = Mathf.Clamp(v_mouse, minRotation, maxRotation);


        cam.transform.localEulerAngles = new Vector3(-v_mouse, 0, 0);

        transform.Rotate(0, h_mouse, 0);


        if (characterController.isGrounded)
        {
            move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

            if (Input.GetKey(KeyCode.LeftShift))
            {
                move = transform.TransformDirection(move) * runSpeed;
                isWalking = false;
                isRunning = true;
            }
            else
            {
                move = transform.TransformDirection(move) * walkSpeed;
                isWalking = true;
                isRunning = false;
            }

            if (Input.GetKey(KeyCode.Space))

                move.y = jumpSpeed;
        }
        move.y -= gravity * Time.deltaTime;

        characterController.Move(move * Time.deltaTime);

        if (useFootsteps) Handle_Footsteps();
    }
    private void Handle_Footsteps()
    {
        if (!characterController.isGrounded) return;
        if (Input.GetAxis("Horizontal") ==0 && Input.GetAxis("Vertical") == 0) return;  //como hago?

        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0)
        {
            if(Physics.Raycast(cam.transform.position, Vector3.down, out RaycastHit hit, 3))
            {
                switch(hit.collider.tag)
                {
                    case "Footsteps/Wood":
                        footstepAudioSource.PlayOneShot(woodClips[Random.Range(0, woodClips.Length - 1)]);
                        break;
                    case "Footsteps/Grass":
                        footstepAudioSource.PlayOneShot(grassClips[Random.Range(0, grassClips.Length - 1)]);
                        break;
                    default:
                        footstepAudioSource.PlayOneShot(woodClips[Random.Range(0, woodClips.Length - 1)]);
                        break;
                }
            }
            if (isWalking) footstepTimer = 0.6f; 
            else footstepTimer = 0.35f;
        }
    }
}