using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CornellTech.View
{
	/// <summary>
	/// This class manages the physics items.
	/// </summary>
	public class PhysicsItemManager : MonoBehaviour
	{
		//Serialized
		[SerializeField]
		protected Transform	itemSpawnAnchorTransform;


		///////////////////////////////////////////////////////////////////////////
		//
		// Inherited from MonoBehaviour
		//
		
		protected void Awake ()
		{

		}
		
		protected void Start ()
		{	

		}
		
		protected void Update ()
		{	

		}
		
		///////////////////////////////////////////////////////////////////////////
		//
		// PhysicsItemManager Functions
		//

		public void SetupGameObject(GameObject gameObject)
		{
			//TODO: Fill

			// Add a 'PhysicsItem' component to the 'gameObject' parameter
			gameObject.AddComponent<PhysicsItem>();

			// Set the parents of the 'gameObject' parameter's transform to the PhysicsItemManager's transform
			Transform parentObj = gameObject.GetComponentInParent<Transform>() as Transform;
			parentObj = this.transform;
		
			// Set the 'gameObject' parameter's transform position to the position of the 'itemSpawnAnchorTransform'
			gameObject.transform.position = itemSpawnAnchorTransform.position;
		}
	}
}