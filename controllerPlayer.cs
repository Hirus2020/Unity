using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerPlayer : MonoBehaviour
{
    Rigidbody rb;
    Vector2 inputMov;
    Vector2 inputRot;
    public float velCamina = 10f;
    public float velCorre = 20f;
    public float sensibilidadMouse = 1;
    Transform cam;
    float rotX;
    public float fuerzaSalto = 300;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = transform.GetChild(0);
        rotX = cam.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        inputMov.x = Input.GetAxis("Horizontal");
        inputMov.y = Input.GetAxis("Vertical");

        inputRot.x = Input.GetAxis("Mouse X") * sensibilidadMouse;
        inputRot.y = Input.GetAxis("Mouse Y") * sensibilidadMouse; 

        if(Input.GetButtonDown("Jump")) rb.AddForce(0, fuerzaSalto, 0);
    }

    private void FixedUpdate() {
        float vel = Input.GetKey(KeyCode.LeftShift) ? velCorre : velCamina;
        rb.velocity = transform.forward * vel * inputMov.y //Adelante y Atras
                    + transform.right * vel * inputMov.x //Izq y Der
                    + new Vector3(0,rb.velocity.y,0);

        transform.rotation *= Quaternion.Euler(0, inputRot.x, 0); //Rotar horizontal

        rotX -= inputRot.y;
        rotX = Mathf.Clamp(rotX, -50, 50);
        cam.localRotation = Quaternion.Euler(rotX, 0, 0);
    }
}
