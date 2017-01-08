using UnityEngine;
using System.Collections;

public class MoveOnClick : MonoBehaviour {

    public float moveSpeed;
    private Vector3 target;
    public GameObject player;
    

	// Use this for initialization
	void Start () {

        target = transform.position;
        

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //target = Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);

    }
}
