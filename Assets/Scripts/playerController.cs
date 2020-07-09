using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float sensitivity = 1;
    public float moveSpeed = .1f;
    public GameObject myCamera;

    private float mouseX;
    private float mouseY;
    private float vertical;
    private float horizontal;

    private float maxInteractableDistance = 3;

    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if( Input.GetButtonDown("Fire1") )
        {
            // Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * maxInteractableDistance, Color.green);

            if ( Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var outRay, maxInteractableDistance, 1 << LayerMask.NameToLayer("Interactable") ) )
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
                    GetComponent<Equipment>().EquipObject(outRay.transform.gameObject);
                }
            }
        }
        /* else
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * maxInteractableDistance, Color.red );
        } */
    }
    private void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;

        mouseX = Input.GetAxis("Mouse X");
        mouseY = -Input.GetAxis("Mouse Y");
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        UpdateCamera(mouseY);
        UpdateTurn(mouseX);
        Move(vertical, horizontal);
    }
    void UpdateCamera(float mouseY)
    {
        myCamera.transform.localRotation = Quaternion.Euler(myCamera.transform.eulerAngles.x + mouseY * sensitivity, 0 , 0 );
    }
    void UpdateTurn(float mouseX)
    {
        transform.rotation *= Quaternion.Euler(0, mouseX * sensitivity, 0);
    }

    void Move(float vertical, float horizontal)
    {
        transform.position += transform.forward * vertical * moveSpeed * Time.deltaTime ;
        transform.position += transform.right * horizontal * moveSpeed * Time.deltaTime ;
    }
}
