using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallControl : MonoBehaviour
{
    public static BallControl instance;

    [SerializeField] private LineRenderer lineRenderer;

    private Rigidbody rb;
    private Vector3 startPos, endPos;
    private bool canShoot = false, ballIsStatic = true;
    private Vector3 direction;

    [SerializeField] private float collisionCooldown = 1f;
    private float lastCollisionTime = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Initialize the LineRenderer
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 0; // No trail initially
        }
    }

    void Update()
    {
        // Check if the ball is stationary
        if (rb.velocity == Vector3.zero && !ballIsStatic)
        {
            ballIsStatic = true;
            rb.angularVelocity = Vector3.zero;

            // Stop the LineRenderer trail
            if (lineRenderer != null)
            {
                lineRenderer.positionCount = 0;
            }
        }
        else if (!ballIsStatic)
        {
            // Update the LineRenderer trail while the ball is moving
            UpdateLineRenderer();
        }
    }

    private void FixedUpdate()
    {
        if (canShoot)
        {
            LevelManager.instance.ShotTaken();
            canShoot = false;
            ballIsStatic = false;
            direction = startPos - endPos;
            startPos = endPos = Vector3.zero;
        }
    }

    private void UpdateLineRenderer()
    {
        if (lineRenderer != null)
        {
            // Add the current ball position to the trail
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GolfStick"))
        {
            // Check if enough time has passed since the last collision
            if (Time.time - lastCollisionTime >= collisionCooldown)
            {
                // A valid hit
                lastCollisionTime = Time.time;
                canShoot = true;
                SoundManager.instance.PlaySound(FxTypes.BALL_HIT);
                // Start the LineRenderer trail when the ball is hit
                if (lineRenderer != null)
                {
                    lineRenderer.positionCount = 1;
                    lineRenderer.SetPosition(0, transform.position);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Hole")
        {
            LevelManager.instance.LevelComplete();
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }

#endif
}
