using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Handles all the inputs
    /// </summary>

    [SerializeField]
    float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private float thrusterForce = 1000f;

    [Header("Spring Settings:")]
    [SerializeField]
    [System.Obsolete]
    private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 20;
    [SerializeField]
    private float jointMaxForce = 40f;

    private PlayerMotor motor;
    private ConfigurableJoint joint;

    [System.Obsolete]
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(jointSpring);
    }

    [System.Obsolete]
    void Update()
    {
        //Calculate movement
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        motor.Move(_velocity);

        //Calculate rotations
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        motor.Rotate(_rotation);

        //Calculate Camera rotation for aiming
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * lookSensitivity;

        motor.RotateCamera(_cameraRotationX);

        //Calculate Thruster force for jumping
        Vector3 _thrusterForce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);
        } else
        {
            SetJointSettings(jointSpring);
        }

        motor.ThrusterForce(_thrusterForce);
    }

    [System.Obsolete]
    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive { 
            mode = jointMode,
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }
}
