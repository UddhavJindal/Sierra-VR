using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Rigidbody))]
public class Climbing : MonoBehaviour
{
    [Header("Refrences")]
    public ClimberHand rightHand;
    public ClimberHand leftHand;
    public Rigidbody body;

    [Header("VR Input")]
    public SteamVR_Action_Boolean toggleGripBTN;
    public ConfigurableJoint climbHandle;

    private bool isClimbing;
    private ClimberHand activeHand;

    public void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        updateHand(rightHand);
        updateHand(leftHand);
        if (isClimbing)
        {
            climbHandle.targetPosition = activeHand.transform.localPosition;
        }
    }

    void updateHand(ClimberHand hand)
    {
        if (isClimbing && hand == activeHand)
        {
            if (toggleGripBTN.GetStateUp(hand.hand))
            {
                climbHandle.connectedBody = null;
                isClimbing = false;
                body.useGravity = true;
                Debug.Log("Gravity off");
            }
        }
        else
        {
            if (toggleGripBTN.GetStateDown(hand.hand) || hand.isGrabbing)
            {
                hand.isGrabbing = true;
                if (hand.touchCount > 0)
                {
                    activeHand = hand;
                    isClimbing = true;
                    climbHandle.transform.position = hand.transform.position;
                    body.useGravity = false;
                    climbHandle.connectedBody = body;
                    hand.isGrabbing = false;
                    Debug.Log("Gravity on");
                }
            }
        }
    }
}