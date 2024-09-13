using System.Collections;
using UnityEngine;
using DG.Tweening;

public class DynamicPlatform : MonoBehaviour
{
    public Transform[] waypoints;  // Waypoints for the platform to move to
    public float moveDuration = 2f; // Duration for moving between waypoints
    public float delayAtWaypoint = 1f; // Delay at each waypoint
    public bool loopMovement = true; // Loop or back and forth movement
    public Axis rotateAxis = Axis.None; // Axis to rotate around
    public float rotationSpeed = 45f; // Rotation speed in degrees per second
    public Ease moveEaseType = Ease.Linear; // Type of easing for movement
    public bool useLocalPosition = false; // Use local position for movement

    private int currentWaypointIndex = 0;
    private bool forward = true; // Direction for back and forth movement

    public enum Axis { None, X, Y, Z }

    private void Start()
    {
        MoveToNextWaypoint();
    }

    private void Update()
    {
        RotatePlatform();
    }

    private void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 targetPosition = useLocalPosition ? targetWaypoint.localPosition : targetWaypoint.position;

        // Move to the next waypoint
        transform.DOMove(targetPosition, moveDuration)
            .SetEase(moveEaseType)
            .OnComplete(() =>
            {
                StartCoroutine(WaitAtWaypoint());
            });
    }

    private IEnumerator WaitAtWaypoint()
    {
        yield return new WaitForSeconds(delayAtWaypoint);
        UpdateWaypointIndex();
        MoveToNextWaypoint();
    }

    private void UpdateWaypointIndex()
    {
        if (loopMovement)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            if (forward)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex -= 2;
                    forward = false;
                }
            }
            else
            {
                currentWaypointIndex--;
                if (currentWaypointIndex < 0)
                {
                    currentWaypointIndex = 1;
                    forward = true;
                }
            }
        }
    }

    private void RotatePlatform()
    {
        if (rotateAxis == Axis.None) return;

        Vector3 rotationVector = Vector3.zero;
        switch (rotateAxis)
        {
            case Axis.X:
                rotationVector = Vector3.right;
                break;
            case Axis.Y:
                rotationVector = Vector3.up;
                break;
            case Axis.Z:
                rotationVector = Vector3.forward;
                break;
        }

        transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != null)
            {
                Gizmos.DrawSphere(waypoints[i].position, 0.2f);

                if (i < waypoints.Length - 1)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
                else if (loopMovement)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
                }
            }
        }
    }
}
