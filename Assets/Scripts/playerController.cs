using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float sensitivity = 1;
    public float moveSpeed = .1f;
    public GameObject myCamera;
    public GameObject crosshairIndicator;

    private float mouseX;
    private float mouseY;
    private float vertical;
    private float horizontal;

    private float maxInteractableDistance = 3;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * maxInteractableDistance, Color.green);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var outRay, maxInteractableDistance, 1 << LayerMask.NameToLayer("Interactable")))
        {
            UpdateIndicator(true);
            if ( Input.GetButtonDown("Fire1") )
            {
                // print(outRay.collider.gameObject);
                if( outRay.transform.gameObject.GetComponent<Affectable>() )
                {
                    var affectedObject = outRay.transform.gameObject;
                    GetComponent<Equipment>().AffectObject(affectedObject);
                }
                else if( outRay.transform.gameObject.GetComponent<Equippable>() )
                {
                    var equippable = outRay.transform.gameObject;
                    GetComponent<Equipment>().EquipObject(equippable);
                }
            }
        }
        else
        {
            UpdateIndicator(false);
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * maxInteractableDistance, Color.red );
        } 
    }
    private void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;

        mouseX = Input.GetAxis("Mouse X");
        mouseY = -Input.GetAxis("Mouse Y");
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        UpdateCamera(mouseY);
        UpdateTurn(mouseX);
        if (!(Mathf.Abs(horizontal) <= .1f && Mathf.Abs(vertical) <= .1f))
        {
            ComplexMove(vertical, horizontal);
        }
    }
    void UpdateCamera(float mouseY)
    {
        myCamera.transform.localRotation = Quaternion.Euler(myCamera.transform.eulerAngles.x + mouseY * sensitivity, 0, 0 );
    }
    void UpdateTurn(float mouseX)
    {
        transform.rotation *= Quaternion.Euler(0, mouseX * sensitivity, 0);
    }

    void Move(float vertical, float horizontal)
    {
        transform.position += (transform.forward * vertical + transform.right * horizontal).normalized * moveSpeed * Time.deltaTime ;
    }

    void ComplexMove(float vertical, float horizontal)
    {
        // create a raycast to determine vertical direction (used for climbing hills)
        var movementVector = Vector3.zero;
        var feet = new Vector3(GetComponent<Collider>().bounds.center.x, GetComponent<Collider>().bounds.min.y, GetComponent<Collider>().bounds.center.z);
        var rayMagnitude = 3;
        var rayDistance = (GetComponent<CapsuleCollider>().bounds.max.y - GetComponent<CapsuleCollider>().bounds.min.y) * rayMagnitude;
        var rayOrigin = transform.position + (vertical * transform.forward + horizontal * transform.right).normalized * .5f;
        var rayDirection = transform.up * -1;

        if ( Physics.Raycast( rayOrigin, rayDirection, out var outRay,  rayDistance, ~(1 << LayerMask.NameToLayer("Player")) ) ) // Hit everyting except the player
        {
            movementVector = (outRay.point - feet).normalized;
        }
        else
        {
            Move(vertical, horizontal);
        }

        // Check Grounded. If not, prevent character from moving up (prevents weird accidental jumps)
        
        var groundedRayOrigin = feet - .1f * transform.up * -1;
        Debug.DrawRay(groundedRayOrigin, transform.up * -1 * .4f, Color.blue);

        if (!Physics.Raycast(groundedRayOrigin, transform.up * -1, out var outRay2, .4f, ~(1 << LayerMask.NameToLayer("Player")) ) )
        {
            float gravity = -2.68f;
            //movementVector = new Vector3(movementVector.x, movementVector.y > 0 ? 0 : movementVector.y, movementVector.z).normalized;
            movementVector = new Vector3(movementVector.x, gravity, movementVector.z).normalized;
        }
        else
        {
            print(outRay2.collider.gameObject);
        }
        
        transform.position += movementVector * moveSpeed * Time.deltaTime;
    }

    void UpdateIndicator(bool state)
    {
        if(crosshairIndicator.activeInHierarchy == state)
        {
            return;
        }
        else
        {
            crosshairIndicator.SetActive(state);
        }
    }
}
