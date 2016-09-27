using UnityEngine;
using System.Collections;

public class Player_move : MonoBehaviour
{
    public float rotationspeed;
    Quaternion targetrotation;
    public Rigidbody rb;
    float forwardInput, turnInput;

    public camera_controler cam;
    public float speed = 1;
    float dir;
    float horizontal;
    float vertical;

    
	void Start ()
    {
        targetrotation = transform.rotation;
        forwardInput = turnInput = 0;
	}

    void Update()
    {
        GetInput();
        Turn();
        Orbit(transform, cam.transform, dir, speed);
    }

    void FixedUpdate()
    {
        Run();
    }

    void GetInput()
    {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    void Run()
    {
        rb.velocity = transform.forward * forwardInput * speed;
    }

    void Turn()
    {
        targetrotation *= Quaternion.AngleAxis(rotationspeed * turnInput * Time.deltaTime, Vector3.up);
        transform.rotation = targetrotation;
    }

    void Orbit(Transform root, Transform cam, float dirOut, float speedOut)
    {
        Vector3 rootDir = root.forward;

        Vector3 moveKeyDir = new Vector3(horizontal, 0, vertical);

        speedOut = moveKeyDir.sqrMagnitude;

        //get camera rotation
        Vector3 cameraDirection = cam.forward;
        cameraDirection.y = 0.0f; // kill y
        Quaternion referentislShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);

        //convert movekeys to worldspace cords
        Vector3 moveDir = referentislShift * moveKeyDir;
        Vector3 axisSign = Vector3.Cross(moveDir, rootDir);

        float angleRootToMove = Vector3.Angle(rootDir, moveDir) * (axisSign.y >= 0 ? -1f : 1f);

        angleRootToMove /= 180f;

        dirOut = angleRootToMove * speed;

    }


}
