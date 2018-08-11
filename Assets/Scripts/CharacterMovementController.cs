﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class CharacterMovementController : MonoBehaviour
{
	
	[HideInInspector] public bool facingRight = true;
	//[HideInInspector] public bool jump = false;
    public bool jump = false;

	//public GameObject frontEdgeDetection;
	//public GameObject backEdgeDetection;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 300f;

	public float floorDistance;

	private bool grounded = false;
	private Rigidbody2D rb2d;
	//private AudioSource jumpAudio;

	private RaycastHit2D hitBack;
	private RaycastHit2D hitFront;

    private BoxCollider2D boxCollider;
	//private Animator anim;

	void Awake ()
	{
		// Initializing component variables.
		//anim = GetComponent <Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		boxCollider = GetComponent<BoxCollider2D> ();
		//jumpAudio = GetComponent <AudioSource> ();
	}

	// Update is called once per frame
	void Update ()
	{
		hitFront = Physics2D.Raycast (transform.position,
		                              Vector2.down,
		                              1 << LayerMask.NameToLayer ("Ground"));
		hitBack = Physics2D.Raycast (transform.position,
		                              Vector2.down,
		                             1 << LayerMask.NameToLayer ("Ground"));
		
		grounded = (hitBack.distance < floorDistance || hitFront.distance < floorDistance) && hitFront && hitBack;


		if (hitBack)
		{
			//Debug.Log (string.Format ("Back: {0}, {1}", hitBack.collider.name, hitBack.distance));
		}
		if (hitFront)
		{
			//Debug.Log (string.Format ("Front: {0}, {1}", hitFront.collider.name, hitFront.distance));
		}

		if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.Space)) && grounded)
		{
			// Debug.Log (string.Format ("Jump, {0}", grounded));
			jump = true;
		}
		Quaternion rot = transform.rotation;

		if (transform.rotation != Quaternion.identity)
		{
			transform.rotation = Quaternion.identity;
		}
	}
	/*
	void OnDrawGizmos ()
	{
		Gizmos.DrawRay (transform.position, rb2d.velocity * 2);
	}*/

	void FixedUpdate ()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		//anim.SetBool ("IsWalking", h != 0);


		if (h * rb2d.velocity.x < maxSpeed)
		{
			rb2d.AddForce (Vector2.right * h * moveForce);
		}
		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
		{
			rb2d.velocity = new Vector2 (Mathf.Clamp (rb2d.velocity.x, -maxSpeed, maxSpeed), rb2d.velocity.y);
		}
		if (h > 0 && !facingRight)
		{
			Flip ();
		}
		if (h < 0 && facingRight)
		{
			Flip ();
		}
		if (jump)
		{
			//jumpAudio.Play ();
			//anim.SetTrigger ("Jump");
			rb2d.AddForce (new Vector2 (0f, jumpForce));
			jump = false;
		}
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
