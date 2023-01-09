using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grappler : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;

    // Start is called before the first frame update
    private void Start()
    {
        _distanceJoint.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _lineRenderer.SetPosition(0, mousePos);
            _lineRenderer.SetPosition(1, transform.position);
            _distanceJoint.connectedAnchor = mousePos;
            _distanceJoint.enabled = true;
            _lineRenderer.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _distanceJoint.enabled = false;
            _lineRenderer.enabled = false;
        }
        if (_distanceJoint.enabled) 
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
    }
}