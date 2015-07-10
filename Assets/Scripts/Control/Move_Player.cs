using UnityEngine;
using System.Collections;

public class Move_Player : MonoBehaviour {
	public float moveSpeed = 6f;
	private Animator anim;
	private CircleCollider2D coll;

	void Start(){
		anim = GetComponent<Animator>();
	
		if (Config.CMODE45)
			transform.Rotate (new Vector3 (0, 0, 45));

		transform.Rotate (new Vector3 (-25, 0, 0));
		AddBoxCollider ();
	}

	void Update(){
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		GetComponent<Rigidbody2D>().velocity = new Vector2 (h, v);
		if (Config.CMODE45)
			GetComponent<Rigidbody2D>().velocity = Rotate45 (GetComponent<Rigidbody2D>().velocity);

		GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * moveSpeed;

		anim.SetFloat("hSpeed", h);
		anim.SetFloat("vSpeed", v);
	}

	void AddBoxCollider(){
		BoxCollider2D bCol = gameObject.AddComponent<BoxCollider2D> ();
		bCol.offset = new Vector2 (0, 0);
		bCol.size = new Vector2 (0.9f, 0.9f);
	}

	Vector2 Rotate45(Vector2 vec2){
		float cs45 = Mathf.Sqrt (2);
		return new Vector2 (cs45 * vec2.x - cs45 * vec2.y, cs45 * vec2.x + cs45 * vec2.y);
	}
}
