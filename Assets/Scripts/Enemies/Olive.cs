﻿using UnityEngine;

public class Olive : Enemy {

	[SerializeField]
	private float _rotateSpeed = 1.0f;

	private Rigidbody2D _rigidBody = null;

	protected override void Awake() {
		base.Awake();
		_rigidBody = this.GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate() {
		float speed = _rigidBody.velocity.sqrMagnitude;
		float direction = GetDirection(_rigidBody.velocity);
		this.transform.RotateAround(this.transform.position, Vector3.forward, _rotateSpeed * speed * Time.deltaTime * direction);
	}

	private float GetDirection(Vector3 velocity) {
		float dotLeft = Vector3.Dot(velocity, Vector3.left);
		float dotRight = Vector3.Dot(velocity, Vector3.right);
		return (dotLeft > dotRight) ? 1.0f : -1.0f;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		AttackObject(other.gameObject);
	}
}
