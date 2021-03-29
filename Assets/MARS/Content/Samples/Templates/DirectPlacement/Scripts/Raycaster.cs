using Unity.MARS.MARSUtils;
using UnityEngine;

namespace Unity.MARS.Content
{
    /// <summary>
    /// Raycasts against colliders and specialized placement objects
    /// Allows objects to be placed in the given location
    /// </summary>
    public class Raycaster : MonoBehaviour
    {
        static RaycastHit[] s_RaycastResults = new RaycastHit[10];

        [SerializeField]
        [Tooltip("True drives this raycaster internally via mouse/tap events. Set to false to drive this raycaster via a parent transform and events.")]
        bool m_ScreenMode = true;

        [SerializeField]
        [Tooltip("Gameobject that is set to the hit location of the raycast. Disabled when no target is available.")]
        GameObject m_Cursor;

        [SerializeField]
        [Tooltip("The object to place.")]
        GameObject m_ObjectToPlace;

        [SerializeField]
        [Tooltip("Collider mask for raycast targets.")]
        LayerMask m_Mask = 1;

        [SerializeField]
        [Tooltip("Only allow raycasts against objects with PlacementTarget scripts.")]
        bool m_PlacementTargetsOnly = false;

        [SerializeField]
        [Tooltip("Maximum distance to check for raycasting.")]
        float m_MaskDistance = 10.0f;

        [SerializeField]
        [Tooltip("If the cursor should align to the raycast surface normal.")]
        bool m_AlignToTargetNormal = false;

        [SerializeField]
        [Tooltip("In screen mode, how long input must be pressed before the object is placed.")]
        float m_PlacementHoldTime = 1.0f;

        Vector3 m_ScreenPoint = new Vector3(0.5f, 0.5f, 0.0f);
        Camera m_Camera;

        Transform m_CursorTransform;
        Transform m_CameraTransform;

        bool m_ValidTarget = false;
        float m_PlacementTimer = 0.0f;

        PlacementTarget m_LastTarget;
        Transform m_PlacementParent;

        void Start()
        {
            m_Camera = MarsRuntimeUtils.GetActiveCamera(true);
            m_CameraTransform = m_Camera.transform;

            if (m_Cursor != null)
            {
                m_CursorTransform = m_Cursor.transform;
                m_Cursor.SetActive(false);
            }

            if (m_ObjectToPlace != null)
                m_ObjectToPlace.SetActive(false);
        }

        void Update()
        {
            if (m_ObjectToPlace == null)
            {
                DisableCursor();
                return;
            }

            // If the screen has been tapped, update where the cursor is pointing
            if (Input.GetMouseButtonDown(0))
                m_ScreenPoint = Vector3.Scale(Input.mousePosition, new Vector3(1.0f / Screen.width, 1.0f / Screen.height, 0.0f));

            // Make a ray - set to the last touched screen position in screen mode. Otherwise just mirror the camera
            var screenRay = new Ray(m_CameraTransform.position, m_CameraTransform.forward);

            if (m_ScreenMode)
            {
                var screenCoords = Vector3.Scale(new Vector3(Screen.width, Screen.height, 0.0f), m_ScreenPoint);
                screenRay = m_Camera.ScreenPointToRay(screenCoords, Camera.MonoOrStereoscopicEye.Mono);
            }

            // Use this ray to find any potential colliders
            var foundHits = Physics.RaycastNonAlloc(screenRay, s_RaycastResults, m_MaskDistance, m_Mask);
            var rayHits = foundHits;

            // Filter out non-placement targets if the user has requested it
            var rayCounter = 0;
            if (m_PlacementTargetsOnly)
            {
                while (rayCounter < foundHits)
                {
                    if (s_RaycastResults[rayCounter].collider.GetComponent<PlacementTarget>() == null)
                    {
                        s_RaycastResults[rayCounter].distance = m_MaskDistance + 1.0f;
                        rayHits--;
                    }
                    rayCounter++;
                }
            }

            // No hits? Turn off the target
            if (rayHits <= 0)
            {
                DisableCursor();
                return;
            }
            else
                m_Cursor.SetActive(true);

            m_ValidTarget = true;

            // Find the closest target and align the cursor to that target
            var closestHit = 0;
            var closestHitDistance = m_MaskDistance;
            rayCounter = 0;
            while (rayCounter < foundHits)
            {
                if (s_RaycastResults[rayCounter].distance < closestHitDistance)
                {
                    closestHit = rayCounter;
                    closestHitDistance = s_RaycastResults[rayCounter].distance;
                }
                rayCounter++;
            }

            var closestPlacementTarget = s_RaycastResults[closestHit].collider.GetComponent<PlacementTarget>();
            if (closestPlacementTarget != null && closestPlacementTarget != m_LastTarget)
                closestPlacementTarget.HoverBegin();

            if (m_LastTarget != closestPlacementTarget && m_LastTarget != null)
                m_LastTarget.HoverEnd();

            m_LastTarget = closestPlacementTarget;

            m_PlacementParent = s_RaycastResults[closestHit].collider.transform;

            // Align the cursor to this target
            m_CursorTransform.position = s_RaycastResults[closestHit].point;
            var normal = m_AlignToTargetNormal ? s_RaycastResults[closestHit].normal : Vector3.up;
            var forward = m_CameraTransform.forward;
            if (Mathf.Abs(Vector3.Dot(normal, forward)) > 0.5f)
                forward = m_CameraTransform.up;

            m_CursorTransform.rotation = Quaternion.LookRotation(Vector3.Cross(Vector3.Cross(forward, normal), normal), normal);

            // If the touch is held down, trigger a placement
            m_PlacementTimer = Input.GetMouseButton(0) ? m_PlacementTimer + Time.deltaTime : 0.0f;

            if (m_PlacementTimer >= m_PlacementHoldTime)
                PlaceObject();
        }

        void DisableCursor()
        {
            m_ValidTarget = false;
            m_PlacementTimer = 0.0f;
            m_Cursor.SetActive(false);
        }

        /// <summary>
        /// Places the loaded object at the cursor's location
        /// Call this method manually if driving via XRI or other non-screen based approaches
        /// </summary>
        public void PlaceObject()
        {
            // Can't place if we don't have a valid target
            if (!m_ValidTarget)
                return;

            // No cursor needed after the object is placed
            DisableCursor();

            // Can't place an object we do not have!
            if (m_ObjectToPlace == null)
                return;

            var objectTransform = m_ObjectToPlace.transform;
            objectTransform.parent = m_PlacementParent;
            objectTransform.position = m_CursorTransform.position;
            objectTransform.rotation = m_CursorTransform.rotation;
            m_ObjectToPlace.SetActive(true);
            m_ObjectToPlace = null;
        }

        /// <summary>
        /// Sets the object that will be placed when the user holds down the input
        /// </summary>
        /// <param name="toPlace">The object to place</param>
        public void SetObjectToPlace(GameObject toPlace)
        {
            if (m_ObjectToPlace != null)
                m_ObjectToPlace.SetActive(false);
        }
    }
}

