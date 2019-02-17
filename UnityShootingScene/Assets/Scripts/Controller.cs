using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{

    //Input
    protected Player Player;

    //Parameters
    protected const float RotationSpeed = 100;

    //Camera Controll
    public Vector3 CameraPivot;
    public float CameraDistance;
    protected float InputRotationX;
    protected float InputRotationY;

    protected Vector3 CharacterPivot;
    protected Vector3 LookDirection;

    // Use this for initialization
    void Start()
    {
        Player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Cursor.lockState = CursorLockMode.Locked;

        //input
        InputRotationX = InputRotationX + mouseInput.x * RotationSpeed * Time.deltaTime % 360f;
        InputRotationY = Mathf.Clamp(InputRotationY - mouseInput.y * RotationSpeed * Time.deltaTime, -88f, 88f);

        //left and forward
        var characterForward = Quaternion.AngleAxis(InputRotationX, Vector3.up) * Vector3.forward;
        var characterLeft = Quaternion.AngleAxis(InputRotationX + 90, Vector3.up) * Vector3.forward;

        //look and run direction
        var runDirection = characterForward * (Input.GetAxisRaw("Vertical") ) + characterLeft * (Input.GetAxisRaw("Horizontal"));
        LookDirection = Quaternion.AngleAxis(InputRotationY, characterLeft) * characterForward;


        //set player values
        Player.Input.RunX = runDirection.x;
        Player.Input.RunZ = runDirection.z;
        Player.Input.LookX = LookDirection.x;
        Player.Input.LookZ = LookDirection.z;
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Player.Input.SwitchToAK = true;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Player.Input.SwitchToPistol = true;
        Player.Input.Shoot = Input.GetMouseButton(0);

        RaycastHit hitInfo = new RaycastHit();
        gameObject.layer = Physics.IgnoreRaycastLayer;
        int j = Physics.DefaultRaycastLayers;
        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward, out hitInfo, Player.AllButIgnoreLayer))
            Player.Input.ShootTarget = hitInfo.point;
        else
            Player.Input.ShootTarget = Vector3.up * 1000f;
        gameObject.layer = 0;

        Debug.DrawLine(transform.position + Vector3.up * 1.5f, Player.Input.ShootTarget, Color.magenta);
        
        CharacterPivot = Quaternion.AngleAxis(InputRotationX, Vector3.up) * CameraPivot;
    }

    private void LateUpdate()
    {
        //set camera values
        Camera.main.transform.position = (transform.position + CharacterPivot) - LookDirection * CameraDistance;
        Camera.main.transform.rotation = Quaternion.LookRotation(LookDirection, Vector3.up);
    }
}