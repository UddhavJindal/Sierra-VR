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
    public Transform player;

    [Header("VR Input")]
    public SteamVR_Action_Boolean toggleGripBTN;
    public ConfigurableJoint joint;

    [Header("Variables")]
    public float speed;
    public float upTime;

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
            joint.targetPosition = activeHand.transform.localPosition;
        }
    }

    void updateHand(ClimberHand hand)
    {
        if (isClimbing && hand == activeHand)
        {
            if (toggleGripBTN.GetStateUp(hand.hand))
            {
                float newposY = transform.position.y;
                //player.transform.position -= (Vector3.down / speed) * Time.deltatime;
                newposY = Mathf.Lerp(player.transform.position.y, player.transform.position.y - speed, upTime * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, newposY, transform.position.z);
                Debug.Log("Down");
            }
        }
        else
        {
            if (toggleGripBTN.GetStateDown(hand.hand) || hand.isGrabbing)
            {
                hand.isGrabbing = true;
                if (hand.touchCount > 0)
                {
                    float newposY = transform.position.y;
                    //player.transform.position += (Vector3.up / speed) * Time.deltatime;
                    newposY = Mathf.Lerp(player.transform.position.y, player.transform.position.y + speed, upTime * Time.deltaTime);
                    transform.position = new Vector3(transform.position.x, newposY, transform.position.z);
                    Debug.Log("Up");
                }
            }
        }
    }
}