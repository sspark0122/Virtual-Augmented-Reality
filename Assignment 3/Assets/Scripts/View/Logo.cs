using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CornellTech.View
{
	/// <summary>
	/// This class manages the Cornell Tech logo in the scene.
	/// </summary>
	public class Logo : MonoBehaviour
	{
		//Readonly/const
		protected readonly int COLLISION_LIMIT = 5;
		
		/////Protected/////
		//References
		protected LogoPiece[] pieces;
		//Primitives

		int count = 0;

		///////////////////////////////////////////////////////////////////////////
		//
		// Inherited from MonoBehaviour
		//
		
		protected void Awake ()
		{
			pieces = GetComponentsInChildren<LogoPiece> ();
			for (int i = 0; i < pieces.Length; i++)
			{
				pieces [i].CollisionEnteredAction += OnCollisionEntered;
			}
		}
		
		protected void Start ()
		{	

		}
		
		protected void Update ()
		{	

		}
		
		///////////////////////////////////////////////////////////////////////////
		//
		// Logo Functions
		//

		protected void FallApart()
		{
			//TODO: Fill

			// Call AddRigidbody if the piece element in the array has the the 'ShouldFall' property set to true.
			for (int i=0; i<pieces.Length; i++) 
			{
				if (pieces[i].ShouldFall == true) 
				{
					pieces[i].AddRigidbody();
				}
			}
		}
		
		////////////////////////////////////////
		//
		// Event Functions

		protected void OnCollisionEntered()
		{
			//TODO: Fill
			count++;
			Debug.Log("Collision count: " + count);

			// After onCollisionEntered is called five times, call FallApart
			if (count == 5) 
			{
				FallApart ();
			}
		}

	}
}