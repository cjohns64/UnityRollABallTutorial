using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; // player Rigidbody
    private float movementX; // player x movement component
    private float movementY; // player y movement component
    private int count; // PickUp count
    private int jumps; // number of jumps the player has left
    // private Vector3 start_location;
    private float jump_wait_time = 0.2f; // min delay between jumps
    private float jump_timer = 0.0f; // timer used to check the jump delay

    public int maxCount; // number of PickUps to win
    public float speed = 0; // selectable speed multiplier
    public TextMeshProUGUI countText; // text object for score count
    public GameObject winTextObject; // text object for win message
    public GameObject player_camera; // camera for player view, used to determine forward direction
    public int reset_y = -10; // vertical distance below which will trigger a game over
    public float jump_height = 5.0f; // force of a jump
    public int num_jumps = 2; // max number of jumps

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        count = 0;
        winTextObject.SetActive(false);
        rb = GetComponent <Rigidbody>();
        // start_location = transform.position;
        SetCountText();
        jumps = num_jumps;
    }

    private void FixedUpdate() 
    {
        // update timer
        jump_timer += Time.deltaTime;

        // only jump if:
        // 1) jump key is pressed,
        // 2) at least 1 jump is remaining,
        // 3) it has been at least jump_wait_time since the last jump
        if (Input.GetButton("Jump") && jumps > 0 && jump_timer > jump_wait_time)
        {
            // add a upwards force in the global up direction
            rb.AddForce(Vector3.up * jump_height, ForceMode.Impulse);
            // one less jump
            jumps--;
            // reset timer
            jump_timer = 0.0f;
        }

        // get the camera forward and right directions so that the player movement can be reletive to the camera
        Vector3 camera_forward = player_camera.transform.forward;
        Vector3 camera_right = player_camera.transform.right;
        // forward/back direction
        if (movementY < 0)
        {
            camera_forward = -camera_forward;
        }
        else if (movementY == 0)
        {
            camera_forward = Vector3.zero;
        }

        // left/right direction
        if (movementX < 0)
        {
            camera_right = -camera_right;
        }
        else if (movementX == 0)
        {
            camera_right = Vector3.zero;
        }
        // rotate to camera
        Vector3 movement = new Vector3 (camera_forward.x + camera_right.x, 0.0f, camera_forward.z + camera_right.z);
        // player movement, pressing "forward" will move the player in the direction the camera is looking, ect.
        rb.AddForce(movement.normalized * speed);
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y;
    }

    void SetCountText() 
    {
        countText.text =  "Count: " + count.ToString();
        if (count >= maxCount)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < reset_y)
        {
            // gameObject.transform.position = start_location;
            EndGame();
        }
        
    }

    private void EndGame()
    {
        // Destroy the current object
        Destroy(gameObject); 
        // Update the winText to display "You Lose!"
        winTextObject.gameObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
    }

    private void OnCollisionEnter (Collision collision)
    {
        // Debug.Log("Collision Enter, Tag was " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EndGame();
        }
        // reset jumps on contact with anything 
        // only ground, boxes, and pickups are in the scene.
        if (jumps < num_jumps)
        {
            jumps = num_jumps;
        }
    }
}
