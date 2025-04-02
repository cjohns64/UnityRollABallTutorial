using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; // player Rigidbody
    private float movementX; // player x movement component
    private float movementY; // player y movement component
    private int count; // PickUp count
    public int maxCount; // number of PickUps to win
    public float speed = 0; // selectable speed multiplier
    public TextMeshProUGUI countText; // text object for score count
    public GameObject winTextObject; // text object for win message
    public GameObject player_camera; // camera for player view, used to determine forward direction

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        count = 0;
        winTextObject.SetActive(false);
        rb = GetComponent <Rigidbody>();
        SetCountText();
    }

    private void FixedUpdate() 
    {
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
        
    }


    private void OnCollisionEnter (Collision collision)
    {
       if (collision.gameObject.CompareTag("Enemy"))
       {
           // Destroy the current object
           Destroy(gameObject); 
           // Update the winText to display "You Lose!"
           winTextObject.gameObject.SetActive(true);
           winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
       }
    }
}
