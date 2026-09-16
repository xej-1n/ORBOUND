using UnityEngine;

public class FlipperController : MonoBehaviour
{
    public string inputName; 
    public float hitStrength = 10000f;

    private HingeJoint2D hinge;
    private JointMotor2D motor;

    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        hinge.useMotor = true;
        motor = hinge.motor;
    }

    void Update()
    {
     
        if (Input.GetKey(inputName)) motor.motorSpeed = -hitStrength;
        else motor.motorSpeed = hitStrength; 

        motor.maxMotorTorque = 10000f;
        hinge.motor = motor;
    }
}