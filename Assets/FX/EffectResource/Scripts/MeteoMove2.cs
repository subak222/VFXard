using UnityEngine;
using System.Collections;

public class MeteoMove2 : MonoBehaviour {

	public float speed;
	public GameObject boomEffect;
	public float remainTime = 1f;
	bool _grounded = false;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if(_grounded) return;

		transform.position -= transform.forward * Time.deltaTime * speed;

		if (transform.position.y < 0.1)
		{
			transform.position = new Vector3(transform.position.x , 0.01f, transform.position.z);

			if (boomEffect != null)
			{
				Destroy(Instantiate(boomEffect, transform.position, boomEffect.transform.rotation), 5f);
			}

			Destroy(gameObject, remainTime);

			_grounded = true;
		}
	}
}