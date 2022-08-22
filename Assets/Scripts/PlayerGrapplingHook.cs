using Normal.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrapplingHook : MonoBehaviour
{
    private RealtimeView view;

    public InputActionReference fireAction;
    public InputActionReference mousePosAction;

    public float hookRetractForce = 5;

    private Rigidbody rb;

    public GrapplingData grapplingData;

    [SerializeField]
    private Transform dynamicTarget;
    [SerializeField]
    private Vector3 dynamicTargetOffset;

    private SpringJoint joint;

    private bool inRange = false;

    public float maxRange = 20;

    private UnityEngine.UI.Image reticule;

    public bool retractToDistance = false;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<RealtimeView>();

        if (view.isOwnedLocallySelf)
        {
            fireAction.action.Enable();
            mousePosAction.action.Enable();
            rb = gameObject.GetComponent<Rigidbody>();

            joint = GetComponent<SpringJoint>();

            reticule = GameObject.Find("Reticule").GetComponent<UnityEngine.UI.Image>();
        }

        fireAction.action.started += FireActionStarted;
        fireAction.action.canceled += FireActionEnded;

    }

    private void OnEnable()
    {

    }


    private void OnDisable()
    {
        fireAction.action.performed -= FireActionStarted;
        fireAction.action.canceled -= FireActionEnded;

    }


    private void FireActionStarted(InputAction.CallbackContext obj)
    {
        if (!grapplingData.Model.hookExtended)
        {
            if (inRange)
            {
                FireHook();
            }
        }
    }


    private void FireActionEnded(InputAction.CallbackContext obj)
    {
        if (grapplingData.Model != null)
        {
            if (grapplingData.Model.hookExtended)
            {
                RetractHook();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (view.isOwnedLocallySelf)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePosAction.action.ReadValue<Vector2>()), out hit, maxRange))
            {
                inRange = true;
                reticule.color = Color.white;
            }
            else
            {
                inRange = false;
                reticule.color = Color.red;
            }

            if (retractToDistance)
            {
                if (Vector3.Distance(transform.position, grapplingData.Model.hookPosition) < joint.maxDistance)
                {
                    joint.maxDistance = Vector3.Distance(transform.position, grapplingData.Model.hookPosition);
                }
            }

        }

        if (grapplingData.Model != null)
        {
            if (view.isOwnedLocallySelf)
            {
                if (dynamicTarget != null)
                {
                    grapplingData.Model.hookPosition = /*dynamicTarget.transform.position + */dynamicTarget.TransformPoint(dynamicTargetOffset);
                }
            }

            if (grapplingData.Model.hookExtended)
            {
                if (view.isOwnedLocallySelf)
                {
                    rb.AddForce((grapplingData.Model.hookPosition - transform.position).normalized * hookRetractForce);
                }
                grapplingData.line.SetPosition(0, transform.position);
            }

        }

    }

    private void FireHook()
    {
        if (view.isOwnedLocallySelf)
        {

            if (grapplingData.Model != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(mousePosAction.action.ReadValue<Vector2>());
                Debug.DrawRay(ray.origin, ray.direction * maxRange, Color.red);
                if (Physics.Raycast(ray, out RaycastHit hit, maxRange))
                {
                    if (hit.rigidbody != null)
                    {
                        dynamicTarget = hit.transform;
                        dynamicTargetOffset = dynamicTarget.InverseTransformPoint(hit.point);

                        grapplingData.Model.hookPosition = /*dynamicTarget.transform.position + */dynamicTarget.TransformPoint(dynamicTargetOffset);

                        joint.connectedBody = hit.rigidbody;
                        joint.connectedAnchor = dynamicTargetOffset;
                        //joint.targetPosition = dynamicTarget.TransformPoint(dynamicTargetOffset);
                        joint.maxDistance = Vector3.Distance(transform.position, dynamicTarget.TransformPoint(dynamicTargetOffset));
                        Debug.DrawLine(transform.position, dynamicTarget.TransformPoint(dynamicTargetOffset), Color.yellow, 1f);
                    }
                    else
                    {
                        grapplingData.Model.hookPosition = hit.point;
                        joint.connectedAnchor = hit.point;
                        joint.maxDistance = Vector3.Distance(transform.position, hit.point);
                        Debug.DrawLine(transform.position, hit.point, Color.yellow, 1f);
                    }
                    joint.spring = 100;

                    grapplingData.Model.hookExtended = true;



                }
            }
        }
    }

    private void RetractHook()
    {
        if (view.isOwnedLocallySelf)
        {

            if (grapplingData.Model != null)
            {
                grapplingData.Model.hookExtended = false;
            }
            dynamicTarget = null;
            joint.anchor = new Vector3();
            joint.connectedBody = null;
            joint.spring = 0;
            joint.connectedBody = null;
            joint.connectedAnchor = new Vector3(); ;
        }
    }

    private void OnDrawGizmos()
    {
        if (grapplingData.Model != null)
        {
            if (grapplingData.Model.hookExtended)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, grapplingData.Model.hookPosition);
            }
        }
    }

    public void BreakHook()
    {
        RetractHook();
    }
}
