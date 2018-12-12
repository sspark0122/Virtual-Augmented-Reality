namespace ARVRAssignments
{
    using UnityEngine;
    using Vuforia;
    using System;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class GameController : MonoBehaviour
    {
        public const float r_LineDrawSpeed = 0.5f;

        public enum GameState
        {
            AddingMarkers,
            AnimatingLineDraw,
            ReadyToSpawnAstronaut,
            ReadyToHitAstronaut
        };

        public enum GameTag
        {
            Astronaut
        };

        [SerializeField]
        public ContentPositioningBehaviour m_ContentPositioningBhvr;

        [SerializeField]
        public AnchorInputListenerBehaviour m_AnchorInputLstnrBhvr;

        [SerializeField]
        public LineRenderer m_LineRenderer;

        [SerializeField]
        public TextMesh m_DistanceTextHldr;
        
        private GameState m_nowState;

        //TODO: Declare the class members here.
        [SerializeField]
        public Button m_ResetButton;

        public AudioSource m_AudioSource;
        public AudioClip astronaut_audio;
        public AudioClip cube_audio;

        private GameObject start_marker;
        private GameObject end_marker;
        private GameObject astronaut;
        private GameObject cube;

        private Vector3 middle_position;

        private float fraction = 0;
        private float distance = 0;
        private int num_Click;
        
        private void Start()
        {
            m_nowState = GameState.AddingMarkers;
            //TODO: Initalise the class members and event listeners.

            start_marker = null;
            end_marker = null;
            num_Click = 0;

            // Initialize a reset button
            m_ResetButton.onClick.AddListener(ResetScene);
            m_ResetButton.GetComponentInChildren<Text>().text = "Clear";

            // Initialize an Audio Source
            m_AudioSource.GetComponent<AudioSource>();

            // Add a listener for ContentPositioningBehaviour
            m_ContentPositioningBhvr.OnContentPlaced.AddListener(x => SpawnNewMarker(x));
        }

        private void SpawnNewMarker(GameObject newMarker)
        {
            //TODO: Implement mini task 2 and part of mini task 3 here.
            
            // First Click
            if (num_Click == 0)
            {
                start_marker = newMarker;
            }

            // Second Click
            if(num_Click == 1)
            {
                end_marker = newMarker;
                m_AnchorInputLstnrBhvr.enabled = false;

                // Get a distance between two markers
                distance = (float)Math.Round(Vector3.Distance(start_marker.transform.position, end_marker.transform.position) * 100f) / 100f;

                // Get the middle point of the two markers
                middle_position = ((start_marker.transform.position + end_marker.transform.position) * 0.5f);

                // Set GameState to Line Draw
                m_nowState = GameState.AnimatingLineDraw;

                // Remove the marker placing listener after placing two markers
                m_ContentPositioningBhvr.OnContentPlaced.RemoveAllListeners();
            }

            num_Click++;
        }

        private void Update()
        {
            if (m_nowState == GameState.AnimatingLineDraw)
            {
                //TODO: Implement mini task 3 and 4 here.
                
                if (fraction < 1)
                {
                    // Animate a line drawn between two markers
                    fraction += Time.deltaTime * r_LineDrawSpeed;
                    var distanceBtwTwoMarkers = Vector3.Lerp(start_marker.transform.position, end_marker.transform.position, fraction);

                    m_LineRenderer.SetPosition(0, start_marker.transform.position);
                    m_LineRenderer.SetPosition(1, distanceBtwTwoMarkers);
                }
                else
                {
                    // Display the distance in meters between two markers
                    m_DistanceTextHldr.transform.position = middle_position;
                    m_DistanceTextHldr.text = distance.ToString() + "M";

                    // Set nowState to ReadyToSpawnAstronaut
                    m_nowState = GameState.ReadyToSpawnAstronaut;
                }
            }
            else
            {
                //TODO: Implement mini task 5 and 6 here.

                if (m_nowState == GameState.ReadyToSpawnAstronaut && Input.GetMouseButtonDown(0))
                {
                    // Spawn an astronaut prefab at the middle of the two markers after a tap on the screen
                    astronaut = Instantiate(Resources.Load("Astronaut")) as GameObject;
                    astronaut.transform.position = middle_position;

                    // Add sound-effects when placing the astronaut on the ground
                    m_AudioSource.clip = astronaut_audio;
                    m_AudioSource.Play();

                    // Set nowState to ReadyToHitAstronaut
                    m_nowState = GameState.ReadyToHitAstronaut;
                }

                if (m_nowState == GameState.ReadyToHitAstronaut && Input.GetMouseButtonDown(0))
                {
                    // Returns a ray going from camera through a screen point
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    
                    // Casts the ray and get the game object hit
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        // If the astronaut is selected, spawn a rigid cube prefab above the astronaut’s head 
                        if (hit.collider.gameObject.name == "Astronaut")
                        {
                            float gap = 0.001f;
                            Vector3 cube_position = middle_position;
                            cube_position.y = astronaut.transform.localScale.y + gap;
                            cube = Instantiate(Resources.Load("Cube")) as GameObject;
                            cube.transform.position = cube_position;
                        }
                    }
                }
            }
        }

        // A screen button to clear the scene (reset) of virtual content
        private void ResetScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // This function is called when this collider/rigidbody has begun touching another rigidbody/collider.
        private void OnCollisionEnter(Collision col)
        {
            // Add sound-effects when the cubes hit the astronaut
            if(col.collider.gameObject.name == "Astronaut")
            {
                m_AudioSource.clip = cube_audio;
                m_AudioSource.Play();
            }
        }
    }
}