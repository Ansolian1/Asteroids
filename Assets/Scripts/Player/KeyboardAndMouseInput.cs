using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardAndMouseInput : MonoBehaviour
{
    private PlayerShip playerShip;
    private Camera playerCamera;
    private void Start()
    {
        playerShip = GetComponent<PlayerShip>();
        playerCamera = Camera.main;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetMouseButton(1))
        {
            playerShip.AddAcceleration();
        }

        float angle = GetMouseAngle();

        if (Mathf.Abs(angle - transform.rotation.y) < 0.5)
        {
            return;
        }

        if (angle > transform.rotation.y)
        {
            playerShip.RotateRight();
        }
        else
        {
            playerShip.RotateLeft();
        }
    }

    private float GetMouseAngle()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = playerCamera.ScreenToWorldPoint(mouse);
        Vector3 relative = transform.InverseTransformPoint(mouseWorld);
        return Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            playerShip.Shoot();
        }
    }
}
