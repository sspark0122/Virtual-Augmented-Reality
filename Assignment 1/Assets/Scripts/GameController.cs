namespace ARVRAssignments
{
    using UnityEngine;
    using UnityEngine.UI;

    public class GameController : MonoBehaviour
    {

        private const float m_MoveSpeed = 0.001f;

        private readonly string r_TouchScreenMessage = "Touch the screen to display my 3D NetID.";

        private readonly string r_MoveNetID3DMessage = "Move your finger on the screen to move my 3D NetID around.";

        [SerializeField]
        public Text m_SystemMessageTextHolder;

        private Vector3 m_NetID3DInitialPstn;

        private TextMesh m_NetID3DTextHolder;

        private void Start()
        {
            m_NetID3DInitialPstn = new Vector3(-0.462f, 0f, -0.411f);
            m_NetID3DTextHolder = null;
            m_SystemMessageTextHolder.text = r_TouchScreenMessage;
        }

        private void FixedUpdate()
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                if (m_NetID3DTextHolder == null)
                {
                    _SpawnPrefab();
                }
                //TODO: Implement the mini task 5 here.

                // Initialize ground plane
                Plane plane = new Plane(Vector3.up, Vector3.up * 0);

                // Returns a ray going from camera through a screen point
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // True when the ray intersects the collider, otherwise false
                // Distance about where the collider was hit
                float distance; 
                if (plane.Raycast(ray, out distance))
                {
                    // Set the position of the text holder.
                    m_NetID3DTextHolder.transform.position = ray.GetPoint(distance);
                }
            }
        }

        private void _SpawnPrefab()
        {
            //TODO: Implement the mini task 4 here.
            m_SystemMessageTextHolder.text = r_MoveNetID3DMessage;

            // Spawn a 3DNetID gameObject from the prefab after the first screen touch input
            GameObject go = Instantiate(Resources.Load("3DNetID")) as GameObject;
            m_NetID3DTextHolder = go.GetComponent<TextMesh>();
        }
    }
}