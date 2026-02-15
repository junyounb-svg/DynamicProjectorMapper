using UnityEngine;

/// <summary>
/// Attach this script to the Sound object for motion-tracking pickup/dropoff interaction.
/// - When HMT comes within pickup proximity of Sound (at Point A), Sound attaches to HMT.
/// - When Sound (on HMT) comes within dropoff proximity of Point B, Sound attaches to Point B.
/// </summary>
public class SoundPickupDropoff : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The motion-tracked object (hand/controller) that picks up and carries Sound")]
    public Transform HMT;

    [Tooltip("Drop-off destination point")]
    public Transform PointB;

    [Header("Proximity Settings")]
    [Tooltip("Distance within which HMT can pick up Sound from Point A")]
    public float pickupProximity = 0.1f;

    [Tooltip("Distance within which Sound can be dropped off at Point B")]
    public float dropoffProximity = 0.1f;

    [Header("Debug")]
    [SerializeField] private bool isAttachedToHMT;

    private void Update()
    {
        if (HMT == null || PointB == null) return;

        if (isAttachedToHMT)
        {
            // Check if we're close enough to Point B to drop off
            float distanceToPointB = Vector3.Distance(transform.position, PointB.position);
            if (distanceToPointB <= dropoffProximity)
            {
                DropOffAtPointB();
            }
        }
        else
        {
            // Check if HMT is close enough to pick up Sound
            float distanceToHMT = Vector3.Distance(transform.position, HMT.position);
            if (distanceToHMT <= pickupProximity)
            {
                AttachToHMT();
            }
        }
    }

    private void AttachToHMT()
    {
        transform.SetParent(HMT);
        isAttachedToHMT = true;
    }

    private void DropOffAtPointB()
    {
        transform.SetParent(PointB);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        isAttachedToHMT = false;
    }
}
