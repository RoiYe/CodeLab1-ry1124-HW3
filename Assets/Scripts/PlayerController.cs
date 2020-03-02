using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float force = 5; //public var for force amount
    Rigidbody2D rb; //var for the Rigidbody2D
    public Vector3 scaleChange = new Vector3(0.1f, 0.1f, 0);
    public Transform field;
    public Text loseText;

    //static variable means the value is the same for all the objects of this class type and the class itself
    public static PlayerController instance; //this static var will hold the Singleton

    private void Awake()
    {
        if (instance == null) //instance hasn't been set yet
        {
            instance = this; //set instance to this object
            DontDestroyOnLoad(gameObject); //Dont Destroy this object when you load a new scene
        } else { //if the instance is already set to an object
            Destroy(gameObject); //destroy this new object, so there is only ever one
        }
    }

    // Start is called before the first frame update
    void Start() //setup
    {
        Debug.Log("Hello, World!"); //print "Hello, World!" to console

        rb = GetComponent<Rigidbody2D>(); //get the Rigidbody2D  off of this gameObject
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		float dist = Vector2.Distance(field.position, transform.position);
		if (dist + transform.localScale.x / 2 > 5)
		{
			Debug.Log("out of boarder");
			loseText.text = "You Lose!";
			loseText.gameObject.SetActive(true);

		}
		else
		{
			float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb.AddForce(movement * force);
		}
	}

	//Change player size when collision
	private void OnCollisionEnter2D(Collision2D collision)
	{
		transform.localScale += scaleChange;
	}
}
