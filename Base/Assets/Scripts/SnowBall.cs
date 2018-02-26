﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour {


	[Tooltip ("How fast is the snowball moving")]
	public float speed = 2;
	[Tooltip ("After how many seconds is the snowball destroyed")]
	public float lifeTime = 3;
	[Tooltip ("The direction the snowball travels")]
	public Vector2 direction;
	[SerializeField] LayerMask shieldLayers = 0;
	public Collider2D check = null; // 

	void Start ()
	{
		direction.Normalize ();
		StartCoroutine (KillAfterSeconds (lifeTime));
	}


	void Update ()
	{
		
		if (check != null) {
			if (check.IsTouchingLayers (shieldLayers)) {
				direction.x = 1;
				print ("touch");
				Destroy (gameObject);
			}
			transform.position += new Vector3 (direction.x, 0, 0) * speed * Time.deltaTime;
		}
	}
		

	void OnCollisionStay2D(Collision2D col)
	{
		if (col.collider.CompareTag("Player"))
		{
			Player player = col.transform.root.GetComponentInChildren<Player>();
			player.Die();
			Destroy (gameObject);
		}
	}

	/// <summary>
	/// Destroys the projectile after seconds. This is a coroutine that needs be started using StartCoroutine().
	/// </summary>
	IEnumerator KillAfterSeconds (float seconds)
	{
		yield return new WaitForSeconds (seconds);
		Destroy (gameObject);
	}
}
