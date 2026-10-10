using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
	private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();    
		}

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized *  speed * Time.fixedDeltaTime;
        rb.velocity = velocity;
		//animator.SetBool("IsmovingUp", false);
		//animator.SetBool("IsMovingLeft", false);
		//animator.SetBool("IsMovingDown", false);
		//animator.SetBool("IsmovingRight", false);
		
		// if (Input.GetKeyDown(KeyCode.W)){
			// animator.SetBool("IsmovingUp", true);
			
		// } 
		// if (Input.GetKeyDown(KeyCode.A)){
			// animator.SetBool("IsMovingLeft", true);
			
		// } 
		// if (Input.GetKeyDown(KeyCode.S)){
			// animator.SetBool("IsMovingDown", true);
			
		// } 
		// if (Input.GetKeyDown(KeyCode.D)){
			// animator.SetBool("IsmovingRight", true);
			
		// } 
		
		animator.SetFloat("MoveX",rb.velocity.x);
		animator.SetFloat("MoveY",rb.velocity.y);
	}

    public void stopMove() {
        rb.velocity = Vector2.zero;
    }
}
