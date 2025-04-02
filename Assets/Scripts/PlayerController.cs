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
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
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
