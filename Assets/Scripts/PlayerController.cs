using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;

	private float movementX;
	private float movementY;

	int jumpTemp;
	public float jHeight = 200f;

	private Rigidbody rb;
	private int count;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		count = 0;

		SetCountText();

		winTextObject.SetActive(false);
	}

	void FixedUpdate()
	{
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);

		rb.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("PickUp"))
		{
			other.gameObject.SetActive(false);

			count = count + 1;

			SetCountText();
		}
	}

	void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>();

		movementX = v.x;
		movementY = v.y;
	}

	// Double Jump "Grounding" Method that checks ground collision
	// Based on unity code for double jump 
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			jumpTemp = 0;
		}
	}

	private void Update()
	{
		// Double Jump Code made using: Grepper if space is checked
		// Also made using stack overflow
		bool pressed = Input.GetKeyDown(KeyCode.Space);

		if (pressed)
		{
			if (jumpTemp < 2)
			{
				rb.AddForce(Vector3.up * jHeight);
				jumpTemp += 1;
			}
		}
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 12)
		{
			winTextObject.SetActive(true);
		}
	}
}
