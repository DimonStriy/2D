using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour {

    public Animator myAnimator;

    [SerializeField]
    protected int health;

    public bool IsDead;

    [SerializeField]
    protected Transform knifePos;

    [SerializeField]
    protected float movementSpeed;

    protected bool facingRight;

    [SerializeField]
    private GameObject knifePrefab;

    private bool attack;

    public bool TakingDamage;

    // Use this for initialization
    public virtual void Start () {
        facingRight = true;
        myAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract IEnumerator TakeDamage();

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    public virtual void ThrowKnife(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<Knife>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<Knife>().Initialize(Vector2.left);
        }
    }


    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Knife")
        {
            StartCoroutine(TakeDamage());
        }
    }
}
