using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {

    private Rigidbody2D myrigitbody2D;

    public float MoveSpeed, JumpHeight;
    private float moveVelocity;
    public bool canMove;


    // Use this for initialization
    void Start () {
        myrigitbody2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update ()
    {
        if(!canMove)
        {
            return;
        }

        moveVelocity = MoveSpeed * Input.GetAxisRaw("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);


        if (GetComponent<Rigidbody2D>().velocity.x > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);    
    }

    
}
