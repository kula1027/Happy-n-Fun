using UnityEngine;
using System.Collections;

public class Helper {
	public static Vector2 V3toV2(Vector3 vec3){
		return new Vector2(vec3.x, vec3.y);
	}

	public static Vector2 RoundVec2(Vector2 vec2){
		return new Vector2(Mathf.Round(vec2.x), Mathf.Round(vec2.y));
	}
}
