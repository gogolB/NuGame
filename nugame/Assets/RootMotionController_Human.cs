using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class RootMotionController_Human : MonoBehaviour {

	void OnAnimatorMove()
	{
		Animator animator = GetComponent<Animator>(); 
		
		if (animator)
		{

		}
	}
}
