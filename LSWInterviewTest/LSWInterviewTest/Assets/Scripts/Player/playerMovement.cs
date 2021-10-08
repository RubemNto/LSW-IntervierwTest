using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Vector2 dir = Vector2.zero;
    public Vector2 Dir { get { return dir; } set { dir = value; } }
    private Rigidbody2D rb;
    public float speed = 100f;
    public bool movable = true;
    public LayerMask Interactable;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        if (movable == true)
            rb.velocity = dir.normalized * speed * Time.fixedDeltaTime;
        else
            rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Balcony"))
        {
            Debug.Log("Interact");
            other.gameObject.GetComponent<Interactable>().interact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Balcony"))
        {
            //Tell player what button to Press
            other.gameObject.GetComponent<Interactable>().interact = false;
        }
    }
}
