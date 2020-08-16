using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignTransform : MonoBehaviour
{
	public TransformVariable transformVariable;

	private void Awake()
	{
		transformVariable.value = this.transform;
		Destroy(this);
	}

}