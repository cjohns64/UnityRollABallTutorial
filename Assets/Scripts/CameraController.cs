using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private Rigidbody rb; // Rigidbody for player
    // Angular speed of camera rotation in radians per sec.
    public float speed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.transform.position;
        rb = player.GetComponent<Rigidbody>();
    }

    // LateUpdate is called once per frame after all Update functions have been completed.
    void LateUpdate()
    {
        if (player != null)
        {
            // update camera's location
            transform.position = player.transform.position + offset;

            // update camera's rotation
            // adapted from https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Rigidbody-linearVelocity.html
            // Determine which direction to rotate towards
            Vector3 targetDirection = rb.linearVelocity - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = speed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(new Vector3 (transform.forward.x, 0.0f, transform.forward.z), targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
