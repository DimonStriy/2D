using UnityEngine;
using System.Collections;

public class carController : MonoBehaviour {

	public float carSpeed;
	public float maxPos = 2.7f;

	Vector3 position;
	public uiManager ui;
	public AudioManager am;

	bool currntPlatformAndroid = false;
	Rigidbody2D rb;

	void Awake(){
		rb = GetComponent<Rigidbody2D>();

		#if UNITY_ANDROID
		currntPlatformAndroid = true;
		#else
		currntPlatformAndroid = false;
		#endif

		am.carSound.Play ();
	}

	// Use this for initialization
	void Start () {
		//ui = GetComponent<uiManager> ();
		position = transform.position;

		if (currntPlatformAndroid == true) {
			Debug.Log ("Android");
		} 
		else {
			Debug.Log ("Windows");
		}
	}
	
	// Update is called once per fra	me
	void Update () {
	
		if (currntPlatformAndroid == true) {
			//android cpecific code
		}
		else {
			position.x +=	Input.GetAxis ("Horizontal") * carSpeed * Time.deltaTime;
			position.x = Mathf.Clamp (position.x, -2.6f, 2.7f);
			transform.position = position;
		}

		position = transform.position;
		position.x = Mathf.Clamp (position.x, -2.6f, 2.7f);
		transform.position = position;
	}


	void OnCollisionEnter2D(Collision2D col) {
		
		if (col.gameObject.tag == "Enemy Car") {
			//Destroy (gameObject);
			gameObject.SetActive (false);
			ui.gameOverActivated ();
			am.carSound.Stop ();
		}
	}

		public void MoveLeft(){
		rb.velocity = new Vector2 (-carSpeed, 0);
			
}
		public void MoveRight(){
		rb.velocity = new Vector2 (carSpeed, 0);
			
}
		public void SetVelocityZero(){
		rb.velocity = Vector2.zero;
	}
}
	