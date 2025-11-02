using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{

    public float WalkSpeed = 5.0f;
    public float SprintSpeed = 8.0f;
    public float mouseSensitivity = 2f;

    private float PlayerSpeed;
    private float yRot;
    private float xRot;
    private Rigidbody rb;

    void Start()
    {
        PlayerSpeed = WalkSpeed;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        yRot += Input.GetAxis("Mouse X") * mouseSensitivity;
        xRot -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.localEulerAngles = new Vector3(xRot, yRot, 0f);
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        

        if (Input.GetKey(KeyCode.LeftShift))
        {
            PlayerSpeed = SprintSpeed;
        }
        else
        {
            PlayerSpeed = WalkSpeed;
        }
        transform.Translate(move * PlayerSpeed * Time.deltaTime, Space.World);
    }
}