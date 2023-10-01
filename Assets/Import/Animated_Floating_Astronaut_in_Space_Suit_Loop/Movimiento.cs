using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    private CharacterController characterController;

    public new Transform camera;
    public float speed = 4;
    public float gravity = -9.8f;
    public float jumpHeight = 2.0f; // altura del salto, puedes ajustar este valor
    private Vector3 velocity; // velocidad actual del personaje

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        camera = GameObject.Find("Main Camera").transform;

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical= Input.GetAxis("Vertical");
        Vector3 movement = Vector3.zero;
        

        if (horizontal != 0 || vertical != 0)
        {
            Vector3 forward = camera.forward;
            forward.y = 0;
            forward.Normalize();
            Vector3 right = camera.right;
            right.y = 0;
            right.Normalize();

            Vector3 direction = forward * vertical + right * horizontal;
            direction.Normalize();
            movement = direction * speed * Time.deltaTime;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-direction), 0.15f);
           
        }
        // lógica de salto
        if (characterController.isGrounded && Input.GetButtonDown("Jump")) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(movement + velocity * Time.deltaTime);


    }
}
