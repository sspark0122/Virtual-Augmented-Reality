namespace CornellTech.View
{
    using UnityEngine;

    public class SimpleAnimationData
    {

        public Vector3 p_EndPosition;
        public float p_Speed;

        public SimpleAnimationData(Vector3 endPosition, float speed)
        {
            p_EndPosition = endPosition;
            p_Speed = speed;
        }

        public float GetMovementDistance(Vector3 startPosition)
        {
            return Vector3.Distance(startPosition, p_EndPosition);
        }
    }

}