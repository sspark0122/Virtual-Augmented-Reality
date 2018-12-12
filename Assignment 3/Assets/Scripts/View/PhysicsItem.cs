using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace CornellTech.View
{
	/// <summary>
	/// This class represents a physics item, an item which follows the laws of physics and interacts with the Valve interaction system.
	/// </summary>
	public class PhysicsItem : MonoBehaviour
	{
		//Readonly/const
		protected readonly float SCALE_MULTIPLIER = 1f;
		
		/////Protected/////
		//References
		protected Hand activeHand;
		protected Rigidbody rigidbody;
		//Primitives
		protected bool isAnimating;

		Vector3 origin;
		Vector3 startPosition;
		bool onFloor = false;
		float startTime;
		float speed = 1.0f;

		///////////////////////////////////////////////////////////////////////////
		//
		// Inherited from MonoBehaviour
		//
		
		protected void Awake ()
		{

		}
		
		protected void Start ()
		{	
			AddPhysics ();
			MakeThrowable ();
		}
		
		protected void Update ()
		{	
			UpdateScale ();

			if (onFloor == true) 
			{
				float distanceCovered = (Time.time - startTime) * speed;
				transform.position = Vector3.Lerp (startPosition, origin, distanceCovered);

				if (distanceCovered >= 1) 
				{
					onFloor = false;

					// Enable the rigidbody
					SetRigidBodyEnabled(false);
					isAnimating = false;
				}
			}
		}
		
		///////////////////////////////////////////////////////////////////////////
		//
		// PhysicsItem Functions
		//

		protected void UpdateScale()
		{
			if (activeHand != null) {
				//TODO: Fill

				// Get y-axis from Touchpad
				float yPosition = SteamVR_Input._default.inActions.ScaleItem.GetAxisDelta (activeHand.handType).y;
				float yScale = yPosition * SCALE_MULTIPLIER;
				Vector3 newScale = new Vector3 (yScale, yScale, yScale);

				if(this.transform.localScale.x < 0.3f)
				{
					// Restrict the minimum localScale
					this.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
				}
				else
				{
					// Otherwise, increase the localScale
					this.transform.localScale += newScale;
				}

//				// Get Y asis of the assigned input source
//				// First touch: down -> scroll: state -> untouch: up
//				bool isUserTouching = SteamVR_Input._default.inActions.ScaleTouch.GetStateDown(activeHand.handType);
//
//				Debug.Log ("User Touch Status: " + isUserTouching);
//
//				if (isUserTouching == false) {
//					float yPosition = SteamVR_Input._default.inActions.ScaleItem.GetAxisDelta (activeHand.handType).y;
//					float yScale = yPosition * SCALE_MULTIPLIER;
//					Vector3 newScale = new Vector3 (yScale, yScale, yScale);
//
//					this.transform.localScale += newScale;
//				}
			}
		}

		protected void AddPhysics ()
		{
			//TODO: Fill

			// Add a Rigidbody component to the GameObject the script is attached to, 
			// and assigns the return value to the golobal 'rigidbody' variable
			rigidbody = gameObject.AddComponent<Rigidbody>() as Rigidbody;

			// For each child of the GameObject
			foreach(Transform childTrans in gameObject.transform) 
			{
				MeshFilter mf = childTrans.gameObject.GetComponent<MeshFilter>() as MeshFilter;

				// Only if it has a MeshFilter
				if(mf.mesh)
				{
					// Adds a MeshCollider to each child of the GameObject the script is attached to
					MeshCollider mc = childTrans.gameObject.AddComponent<MeshCollider>();

					// 'convex' is set to true on each MeshCollider
					mc.convex = true;
					// mc.sharedMesh = mf.mesh;
				}
			}
		}

		protected void MakeThrowable ()
		{
			//TODO: Fill

			// Adds an 'Interactable' component to the GameObject the script is attached to
			gameObject.AddComponent<Interactable>();

			// Adds a 'Velocity Estimator' component to the GameObject the script is attached to
			gameObject.AddComponent<VelocityEstimator>();

			// Adds a 'Throwable' component to the GameObject the script is attached to
			Throwable t = gameObject.AddComponent<Throwable>();
			t.releaseVelocityStyle = ReleaseStyle.ShortEstimation;
			t.restoreOriginalParent = true;
		}

		protected void SetRigidBodyEnabled (bool value)
		{
			rigidbody.useGravity = !value;
			rigidbody.isKinematic = value;
		}

		protected IEnumerator ReturnToOrigin ()
		{
			isAnimating = true;

			//TODO: Fill

			// Get the original spqwn position
			origin = GameObject.FindGameObjectWithTag ("Respawn").transform.position;

			// Waits three seconds
			yield return new WaitForSeconds(3);

			// Set true to onFloor to animate the object
			onFloor = true;
			startTime = Time.time;
			startPosition = this.transform.position;

			// Disables the rigidbody
			SetRigidBodyEnabled(true);

//			this.transform.SetPositionAndRotation (origin, Quaternion.identity);
//			SetRigidBodyEnabled(true);
//			isAnimating = false;
		}
		
		////////////////////////////////////////
		//
		// Event Functions

		//Called with SendMessage from Valve.VR.InteractionSystem.Hand
		protected void OnAttachedToHand (Hand hand)
		{
			activeHand = hand;
		}

		//Called with SendMessage from Valve.VR.InteractionSystem.HAnd
		protected void OnDetachedFromHand (Hand hand)
		{
			activeHand = null;
		}
			
		protected void OnCollisionEnter (Collision collision)
		{
			//The tag of the GameObject with the collider we hit.
			string colliderTag = collision.collider.gameObject.tag;

			if (colliderTag == "Platform" || colliderTag == "Floor")
			{
				if (!isAnimating)
					StartCoroutine (ReturnToOrigin ());
			}
		}

	}
}