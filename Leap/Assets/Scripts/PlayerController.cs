using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 touchpadInput;
    public SteamVR_Action_Boolean JumpAction;
    public SteamVR_Input_Sources MovementHand;
    //public float jumpHeight;
    public Transform cameraTransform;
    private CapsuleCollider capsuleCollider;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        Vector3 movementDir = Player.instance.hmdTransform.TransformDirection(new Vector3(touchpadInput.axis.x, 0, touchpadInput.axis.y));
        transform.position += Vector3.ProjectOnPlane(Time.deltaTime * movementDir* 2.0f, Vector3.up);

        float distanceFromFloor = Vector3.Dot(cameraTransform.localPosition, Vector3.up);
        capsuleCollider.height = Mathf.Max(capsuleCollider.radius, distanceFromFloor);

        Rigidbody RBody = GetComponent<Rigidbody>();
        Vector3 velocity = new Vector3(0, 0, 0);

        /*velocity = movementDir;
        if (JumpAction.GetStateDown(MovementHand))
        {
            float jumpSpeed = Mathf.Sqrt(2 * jumpHeight * 9.81f);
            RBody.AddForce(0, jumpSpeed, 0, ForceMode.VelocityChange);
            Debug.Log("Jumping");
        }*/
        //RBody.AddForce(velocity.x- RBody.velocity.x, 0, velocity.z - RBody.velocity.z, ForceMode.VelocityChange);

        capsuleCollider.center = cameraTransform.localPosition - 0.5f * distanceFromFloor * Vector3.up; 
    }
}
