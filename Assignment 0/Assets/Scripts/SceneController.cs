namespace CornellTech.Controller
{
    using UnityEngine;
    using CornellTech.View;
    using UnityEngine.UI;

    /// <summary>
    /// Scene controller.
    /// This class is responsible for managing the scene, including communicating with the SimpleAnimator.
    /// </summary>
    public class SceneController : MonoBehaviour
    {

        private const float c_MoveTowardsSpeed = 3.0f;

        private const float c_PositionBound = 3.0f;

        private string m_myNetId;

        private SimpleAnimator m_TextAnimator;

        // Serialized Fields (https://docs.unity3d.com/ScriptReference/SerializeField.html)
        // A serialized field which allows us to connect the SimpleAnimator in our scene to the value on this component in the scene.
        [SerializeField]
        private TextMesh m_TextMesh;

        [SerializeField]
        private Button m_SetTargetPstnBttn;

        ///////////////////////////////////////////////////////////////////////////
        //
        // Inherited from MonoBehaviour
        //
        // Read more about these events here: https://docs.unity3d.com/Manual/ExecutionOrder.html
        // 

        protected void Awake()
        {
            m_myNetId = "sp2528";    //TODO: Replace TA's NetID with your own ID.
        }

        protected void Start()
        {
            //TODO: Set up references and add event listener here.

            m_TextMesh.text = m_myNetId;
            m_TextAnimator  = m_TextMesh.GetComponent<SimpleAnimator>();
            Button btn      = m_SetTargetPstnBttn.GetComponent<Button>();
            
            btn.onClick.AddListener(MoveTowardsOnClick);
        }

        ///////////////////////////////////////////////////////////////////////////
        //
        // SceneController Methods
        //

        protected void MoveTowardsOnClick()
        {
            //TODO: Generate the target position animation data and trigger m_TextAnimator.

            m_TextAnimator.StartAnimation(new SimpleAnimationData(GetRandomVector3(c_PositionBound), c_MoveTowardsSpeed));
        }

        ////////////////////////////////////////
        //
        // Utility Methods

        protected Vector3 GetRandomVector3(float range)
        {
            return new Vector3(UnityEngine.Random.Range(-range, range), UnityEngine.Random.Range(-range, range), UnityEngine.Random.Range(-range, range));
        }

    }
}