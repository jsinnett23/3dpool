using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Transform cueBall;

    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 offset;
    [SerializeField] float downAngle;
    [SerializeField] float power;

    GameManager gameManager;


    private float horizontalInput;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            if (ball.GetComponent<Ball>().IsCueBall())
            {
                cueBall = ball.transform;
                break;
            }
        }
        ResetCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (cueBall != null)
        {
            horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.RotateAround(cueBall.position, Vector3.up, horizontalInput);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetCamera();  
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 hitDirection = transform.forward;
            hitDirection = new Vector3(hitDirection.x, 0, hitDirection.z).normalized;
            
            cueBall.gameObject.GetComponent<Rigidbody>().AddForce(hitDirection * power, ForceMode.Impulse);
        }
    }

    private void ResetCamera()
    {
        transform.position = cueBall.position + offset;
        transform.LookAt(cueBall.position);
        transform.localEulerAngles = new Vector3(downAngle, transform.localEulerAngles.y, 0);

    }
   
}
