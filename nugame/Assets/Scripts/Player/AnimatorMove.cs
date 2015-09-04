using UnityEngine;
using System.Collections;

public class AnimatorMove : MonoBehaviour {

	void OnAnimatorMove() {
		Animator animator = GetComponent<Animator>();
		if (animator) {
			Vector3 newPosition = transform.parent.position;
			newPosition.z = this.transform.position.z;
			newPosition.x = this.transform.position.x;
			transform.parent.position = newPosition;
		}
	}
}
