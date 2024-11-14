using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer rope;
    [SerializeField] private Camera cam;
    
    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    
    // Start is called before the first frame update
    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Start Grapple
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0; // Ensure the point is in 2D plane

            Vector2 direction = (mouseWorldPos - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, grappleLength, grappleLayer);

            if (hit.collider != null)
            {
                grapplePoint = hit.point;
                joint.connectedAnchor = grapplePoint;
                joint.distance = Vector2.Distance(transform.position, grapplePoint);
                joint.enabled = true;

                rope.enabled = true;
                rope.SetPosition(0, grapplePoint);  // Anchor end point
                rope.SetPosition(1, transform.position); // Player's position
            }
        }

        // End Grapple
        if (Input.GetMouseButtonUp(0))
        {
            joint.enabled = false;
            rope.enabled = false;
        }

        // Update Rope's Player End Position
        if (rope.enabled)
        {
            rope.SetPosition(1, transform.position);
        }
    }
}
