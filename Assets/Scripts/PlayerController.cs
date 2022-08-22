using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionReference move;
    public InputActionReference look;

    public float moveSpeed = 1;
    public float lookSpeed = 1;
    public float maxPitchAngle = 80;

    public Transform cameraBoom;
    private Rigidbody rb;

    private float cameraPitch;

    private bool isMoving;
    private bool isLooking;
    private Vector3 moveDir;

    private RealtimeView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<RealtimeView>();

        if (view.isOwnedLocallySelf)
        {

            move.action.Enable();
            look.action.Enable();

            rb = GetComponent<Rigidbody>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (cameraBoom == null)
            {
                cameraBoom = GameObject.Find("CameraBoom").transform;
            }

            gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
            move.action.performed += MovePerformed;
            look.action.performed += LookPerformed;
        }
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        if (view.isOwnedLocallySelf)
        {
            move.action.performed -= MovePerformed;
            look.action.performed -= LookPerformed;
        }
    }

    private void LookPerformed(InputAction.CallbackContext obj)
    {
        Vector2 look = obj.ReadValue<Vector2>();

        if (look == Vector2.zero) return;



        cameraPitch = Mathf.Clamp(cameraPitch - (look.y * Time.deltaTime * lookSpeed), -maxPitchAngle, maxPitchAngle);
        cameraBoom.localRotation = Quaternion.Euler(new Vector3(cameraPitch, cameraBoom.localEulerAngles.y + (look.x * Time.deltaTime * lookSpeed * 1.5f), cameraBoom.localEulerAngles.z));
        //cameraBoom.Rotate(new Vector3(((Mathf.Repeat(cameraBoom.localRotation.x, 360) - 180)-cameraPitch) * Time.deltaTime , look.x * Time.deltaTime * lookSpeed * 1.5f, 0));

    }

    private void MovePerformed(InputAction.CallbackContext obj)
    {






    }

    // Update is called once per frame
    void Update()
    {
        if (view.isOwnedLocallySelf)
        {
            cameraBoom.transform.position = transform.position;

            moveDir = move.action.ReadValue<Vector3>();

            if (moveDir != Vector3.zero)
            {
                rb.AddForce(cameraBoom.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
            }
        }
    }
}
