using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour {

    public Camera attachedCamera;
    [Header("Orbit")]
    public float xSpeed = 120f, ySpeed = 120f;
    public float yMinLimit = -20f, yMaxLimit = 80f;
    [Header("Collision")]
    public bool cameraCollision = true; // Is Camera Collision enabled?
    public bool ignoreTriggers = true; // Will the spherecast ignore triggers?
    public float castRadius = 0.3f; // Radius of sphere being spherecasted
    public float castDistance = 1000f; // Distance of the spherecast
    public LayerMask hitLayers; // Layers that casting will hit

    private float originalDistance; // Record starting distance of camera
    private float distance; // Current distance of camera
    private float x, y; // X and Y Mouse Rotation

    // Use this for initialization

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attachedCamera.transform.position, castRadius);
    }

    void Start ()
    {
        // Set original distance
        originalDistance = Vector3.Distance(transform.position, attachedCamera.transform.position);
        // Set X and Y degrees to current camera rotation
        x = transform.eulerAngles.y;
        y = transform.eulerAngles.x;
    }

    // Update is called once per frame
    #region Update
    void Update ()
    {
	    // Is RMB pressed?
        if (Input.GetMouseButton(1))
        {
            // Disable Cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // Orbit with Input
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

            // Restricting the Y Limits
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            // Rotate the transform using Euler angles
            transform.rotation = Quaternion.Euler(y, x, 0);
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
	}
    #endregion

    void FixedUpdate ()
    {
        // Set distance to original distance
        distance = originalDistance;
        // Change distance to what we hit
        // IS camera collision enabled?
        if (cameraCollision)
        {
            // Create a ray starting from orbit position and going in the direction of the camera
            Ray camRay = new Ray(transform.position, -transform.forward);
            RaycastHit hit; //Store hit information after the cast is complete
            // Shoot a sphere behind the camera
            if (Physics.SphereCast(camRay,  // Ray in the direction of camera
                                   castRadius, // How wide the sphere of the spherecast is
                                   out hit, // The hit information the cast collected
                                   castDistance, // How far the cast goes
                                   hitLayers, // What layers we're allowed to hit
                                   ignoreTriggers ? // If statement, ignore triggers?
                                   QueryTriggerInteraction.Ignore // Ignore it!
                                   : // Else
                                   QueryTriggerInteraction.Collide)) // Don't ignore it                                  
            {
                // Set distance to distance of hit
                distance = hit.distance;
            }
        }

        // Apply distance to camera
        attachedCamera.transform.position = transform.position - transform.forward * distance;
    }
}
