namespace CornellTech.View
{

    using UnityEngine;

    /// <summary>
    /// Simple Animator.
    /// This class is responsible for animating the gameObject.
    /// </summary>
    public class SimpleAnimator : MonoBehaviour
    {

        private enum AnimationState { Default, MovingTowards, Rotating };

        private const float c_TargetRotationValue = 360.0f;

        private const float c_RotationTime = 3.0f;

        private const float c_RotationSpeed = c_TargetRotationValue / c_RotationTime;

        // TODO: Declare private properties here

        private AnimationState  currentState;
        private Vector3         targetPosition;
        
        private float           moveTowardsSpeed;   // Speed for moving towards.
        private float           startTime;          // Time when the movement started.
        private float           journeyLength;      // Total distance between the markers.
        private float           rotationLeft;       // Total rotation degree - current rotation degree

        ///////////////////////////////////////////////////////////////////////////
        //
        // Inherited from MonoBehaviour
        //
        // Read more about these events here: https://docs.unity3d.com/Manual/ExecutionOrder.html
        // 

        private void Awake()
        {
            //TODO: Intialise viriables and game state here.

            // Initialize a default state for current state.
            currentState = AnimationState.Default;
        }

        private void Update()
        {
            //TODO: Implement the movement and rotation here.
            
            // If current state is not default, move or rotate the object.
            if(currentState != AnimationState.Default)
            {
                if (GetPosition() == targetPosition)
                    currentState = AnimationState.Rotating;
                else
                    currentState = AnimationState.MovingTowards;

                switch (currentState)
                {
                    // Move the object to the target position at a speed of c_MoveTowardsSpeed.
                    case AnimationState.MovingTowards:

                        // Distance moved = time * speed.
                        float distCovered = (Time.time - startTime) * moveTowardsSpeed;

                        // Fraction of journey completed = current distance divided by total distance.
                        float fracJourney = distCovered / journeyLength;
                        
                        SetPosition(Vector3.Lerp(GetPosition(), targetPosition, fracJourney));

                        // alternative code: SetPosition(Vector3.MoveTowards(GetPosition(), targetPosition, Time.deltaTime * moveTowardsSpeed));
                        break;

                    // Rotate the object 360 degrees around its local Z axis once it arrives the target position at a speed of c_RotationSpeed.
                    case AnimationState.Rotating:
                        
                        float rotation = c_RotationSpeed * Time.deltaTime;
                        
                        if (rotationLeft > rotation)
                        {
                            rotationLeft -= rotation;
                        }
                        else
                        {
                            rotation        = rotationLeft;
                            rotationLeft    = 0;
                            currentState    = AnimationState.Default;
                        }

                        transform.Rotate(0, 0, rotation);
                        break;
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////
        //
        // SimpleAnimator Methods
        //

        public void StartAnimation(SimpleAnimationData animationData)
        {
            Debug.Log("MoveTowards@SimpleAnimator: " + animationData.p_EndPosition.ToString());
            //TODO: Trigger the animation.

            // Button is not interactable in the middle of an active animation.
            if (currentState == AnimationState.Default)
            {
                targetPosition      = animationData.p_EndPosition;
                moveTowardsSpeed    = animationData.p_Speed;
                rotationLeft        = c_TargetRotationValue;

                currentState        = AnimationState.MovingTowards;                     // Set moving state for current state.
                startTime           = Time.time;                                        // Keep a note of the time the movement started.            
                journeyLength       = Vector3.Distance(GetPosition(), targetPosition);  // Calculate the journey length.
            }
        }

        ////////////////////////////////////////
        //
        // Utility Methods

        private Vector3 GetPosition()
        {
            return transform.localPosition;
        }

        private void SetPosition(Vector3 p)
        {
            transform.localPosition = p;
        }
    }
}