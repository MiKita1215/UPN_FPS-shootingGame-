using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public float speed = 5f;
    public float mouseSensitivity = 100f;

    private float verticalRotation = 0f;
    private Animator animator;

    void Start()
    {
        if (photonView.IsMine)
        {
            Cursor.lockState = CursorLockMode.Locked;
            animator = GetComponentInChildren<Animator>();
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.parent.Rotate(Vector3.up * mouseX);

        float moveForward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float moveSideways = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.parent.Translate(Vector3.forward * moveForward);
        transform.parent.Translate(Vector3.right * moveSideways);

        // Update the Animator with speed
        float movementSpeed = new Vector3(moveForward, 0, moveSideways).magnitude;
        animator.SetFloat("Speed", movementSpeed);
    }

    public void Shoot()
    {
        animator.SetTrigger("Shoot");
    }
}
