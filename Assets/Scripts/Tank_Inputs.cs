using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Inputs : MonoBehaviour
{

    public Camera camera1;

    public Vector3 reticlePosition;
    public Vector3 ReticlePosition
    {
        get { return reticlePosition; }
    }

    private Vector3 reticleNormal;
    public Vector3 ReticleNormal
    {
        get { return reticleNormal; }
    }

    private float forwardInput;
    public float ForwardInput
    {
        get { return forwardInput; }
    }

    private float rotationInput;
    public float RotationInput
    {
        get { return rotationInput; }
    }
    // Update is called once per frame
    void Update()
    {
        if(camera1)
        {
            HandleInputs();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(reticlePosition, 0.5f);
    }

    protected virtual void HandleInputs()
    {
        Ray screenRay = camera1.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(screenRay, out hit))
        {
            reticlePosition = hit.point;
            reticleNormal = hit.normal;
        }

        forwardInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");
    }
}
