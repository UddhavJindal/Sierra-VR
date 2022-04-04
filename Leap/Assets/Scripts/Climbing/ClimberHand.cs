using UnityEngine;
using Valve.VR;

public class ClimberHand : MonoBehaviour
{
    [Header("Refrences")]
    public SteamVR_Input_Sources hand;

    [Header("Variables")]
    public int touchCount;
    public bool isGrabbing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Climbale"))
        {
            touchCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climbale"))
        {
            touchCount--;
        }
    }
}