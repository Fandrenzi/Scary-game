using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
public class Player : MonoBehaviour
{

    public float WalkSpeed = 5.0f;
    public float SprintSpeed = 8.0f;
    public float mouseSensitivity = 2f;

    private float PlayerSpeed;
    private float yRot;
    private float xRot;
    private Rigidbody rb;

    public Transform PlayerCamera;

    public AudioClip WalkingSound;
    private AudioSource audiosource;

    private bool ismoving = false;
    void Start()
    {
        PlayerSpeed = WalkSpeed;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        rb.freezeRotation = true;

       
        audiosource = GetComponent<AudioSource>();
        audiosource.clip = WalkingSound;
    }

    // Update is called once per frame
    void Update()
    {
        yRot += Input.GetAxis("Mouse X") * mouseSensitivity;
        xRot -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(0f, yRot, 0f);
        PlayerCamera.localRotation = Quaternion.Euler(xRot, 0f, 0f);

        //float moveX = Input.GetAxis("Horizontal");
        //float moveZ = Input.GetAxis("Vertical");

        //Vector3 move = transform.right * moveX + transform.forward * moveZ;
        

        if (Input.GetKey(KeyCode.LeftShift))
        {
            PlayerSpeed = SprintSpeed;
        }
        else
        {
            PlayerSpeed = WalkSpeed;
        }
        //transform.Translate(move * PlayerSpeed * Time.deltaTime, Space.World);



    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * moveX + transform.forward * moveZ).normalized;

        rb.MovePosition(rb.position + move * PlayerSpeed * Time.fixedDeltaTime);
        
        ismoving = move.magnitude > 0f;

        if (ismoving)
        {
            if (!audiosource.isPlaying)
            audiosource.Play();
        }
        else
        {
            audiosource.Stop();
        }
    }
}