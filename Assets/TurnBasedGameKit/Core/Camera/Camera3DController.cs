using UnityEngine;
using UnityEngine.Extensions;
using System.Collections;

public class Camera3DController : MonoBehaviour
{
    // Public fields
    public Terrain m_Terrain;

    public float m_PanSpeed = 1.5f;
    public float m_ZoomSpeed = 10.0f;
    public float m_RotationSpeed = 5.0f;

    public float m_MousePanMultiplier = 0.1f;
    public float m_MouseRotationMultiplier = 0.2f;
    public float m_MouseZoomMultiplier = 5.0f;

    public float m_MinZoomDistance = 2.0f;
    public float m_MaxZoomDistance = 20.0f;
    public float m_SmoothingFactor = 0.1f;
    public float m_GoToSpeed = 0.1f;

    public bool m_UseKeyboardInput = true;
    public bool m_UseMouseInput = true;
    public bool m_AdaptToTerrainHeight = true;
    public bool m_IncreaseSpeedWhenZoomedOut = true;
    public bool m_CorrectZoomingOutRatio = true;
    public bool m_Smoothing = true;
    public bool m_AllowDoubleClickMovement = false;
    private float m_DoubleClickTimeWindow = 0.3f;
    public bool m_RestrictMouseToWindow = true;

    public bool m_AllowScreenEdgeMovement = true;
    public float m_ScreenPercentForScroll = 0.1f;
    private int m_ScreenPixelThresholdForScroll;

    public float m_ScreenEdgeSpeed = 1.0f;

    public GameObject m_ObjectToFollow;
    public Vector3 m_CameraTarget;

    // private fields
    private float m_CurrentCameraDistance;
    private Vector3 m_LastMousePos;
    private Vector3 m_LastPanSpeed = Vector3.zero;
    private Vector3 m_GoingToCameraTarget = Vector3.zero;
    private bool m_DoingAutoMovement = false;
    private DoubleClickDetector m_DoubleClickDetector;


    // Use this for initialization
    public void Start()
    {
        m_CurrentCameraDistance = m_MinZoomDistance + ( ( m_MaxZoomDistance - m_MinZoomDistance ) / 2.0f );
        m_LastMousePos = Vector3.zero;
        m_DoubleClickDetector = gameObject.AddComponent<DoubleClickDetector>();
        m_DoubleClickDetector.m_DoubleClickTimeWindow = m_DoubleClickTimeWindow;
    }

    // Update is called once per frame
    public void Update()
    {
        if ( m_RestrictMouseToWindow )
        {
            // Confine cursor to the game window.
            // Note that confined cursor lock mode is only supported on the standalone player platform on Windows and Linux.
            Cursor.lockState = CursorLockMode.Confined;
        }

        if ( m_AllowDoubleClickMovement == true )
        {
            //doubleClickDetector.Update();
            UpdateDoubleClick();
        }
        UpdatePanning();
        UpdateRotation();
        UpdateZooming();
        UpdatePosition();
        UpdateAutoMovement();
        m_LastMousePos = Input.mousePosition;
    }

    public void GoTo( Vector3 position )
    {
        m_DoingAutoMovement = true;
        m_GoingToCameraTarget = position;
        m_ObjectToFollow = null;
    }

    public void Follow( GameObject gameObjectToFollow )
    {
        m_ObjectToFollow = gameObjectToFollow;
    }

    #region private functions
    private void UpdateDoubleClick()
    {
        if ( m_DoubleClickDetector.IsDoubleClick() == true && m_Terrain && m_Terrain.GetComponent<Collider>() )
        {
            var cameraTargetY = m_CameraTarget.y;

            var collider = m_Terrain.GetComponent<Collider>();
            var ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            RaycastHit hit = new RaycastHit();
            Vector3 pos;

            if ( collider.Raycast( ray, out hit, Mathf.Infinity ) )
            {
                pos = hit.point;
                pos.y = cameraTargetY;
                GoTo( pos );
            }
        }
    }

    private void UpdatePanning()
    {
        Vector3 moveVector = new Vector3( 0, 0, 0 );
        if ( m_UseKeyboardInput == true )
        {
            //! rewrite to address xyz separately
            if ( Input.GetKey( KeyCode.A ) == true )
            {
                moveVector.x -= 1;
            }
            if ( Input.GetKey( KeyCode.S ) == true )
            {
                moveVector.z -= 1;
            }
            if ( Input.GetKey( KeyCode.D ) == true )
            {
                moveVector.x += 1;
            }
            if ( Input.GetKey( KeyCode.W ) == true )
            {
                moveVector.z += 1;
            }
        }
        if ( m_AllowScreenEdgeMovement == true )
        {
            m_ScreenPixelThresholdForScroll = (int)(Screen.height * m_ScreenPercentForScroll);

            bool isInScrollingZone = Input.mousePosition.x < m_ScreenPixelThresholdForScroll ||
                Input.mousePosition.x > Screen.width - m_ScreenPixelThresholdForScroll ||
                Input.mousePosition.y < m_ScreenPixelThresholdForScroll ||
                Input.mousePosition.y > Screen.height - m_ScreenPixelThresholdForScroll;

            if ( isInScrollingZone )
            {
                float horizontalValue = (Input.mousePosition.x / Screen.width) * 2 - 1;
                float verticalValue = (Input.mousePosition.y / Screen.height) * 2 - 1;

                // Power the values to get better distribution
                horizontalValue = Mathf.Sign(horizontalValue) * Mathf.Pow(horizontalValue, 2);
                verticalValue = Mathf.Sign(verticalValue) * Mathf.Pow(verticalValue, 2);

                moveVector.x += m_ScreenEdgeSpeed * horizontalValue;
                moveVector.z += m_ScreenEdgeSpeed * verticalValue;
            }
        }

        if ( m_UseMouseInput == true )
        {
            if ( Input.GetMouseButton( 2 ) == true && Input.GetKey( KeyCode.LeftShift ) == true )
            {
                Vector3 deltaMousePos = ( Input.mousePosition - m_LastMousePos );
                moveVector += new Vector3( -deltaMousePos.x, 0, -deltaMousePos.y ) * m_MousePanMultiplier;
            }
        }

        if ( moveVector != Vector3.zero )
        {
            m_ObjectToFollow = null;
            m_DoingAutoMovement = false;
        }

        var effectivePanSpeed = moveVector;
        if ( m_Smoothing == true )
        {
            effectivePanSpeed = Vector3.Lerp( m_LastPanSpeed, moveVector, m_SmoothingFactor );
            m_LastPanSpeed = effectivePanSpeed;
        }

        var oldXRotation = transform.localEulerAngles.x;

        // Set the local X rotation to 0;
        transform.SetLocalEulerAngles( 0.0f );

        float panMultiplier = m_IncreaseSpeedWhenZoomedOut ? ( Mathf.Sqrt( m_CurrentCameraDistance ) ) : 1.0f;
        m_CameraTarget = m_CameraTarget + transform.TransformDirection( effectivePanSpeed ) * m_PanSpeed * panMultiplier * Time.deltaTime;

        // Set the old x rotation.
        transform.SetLocalEulerAngles( oldXRotation );
    }

    private void UpdateRotation()
    {
        float deltaAngleH = 0.0f;
        float deltaAngleV = 0.0f;

        if ( m_UseKeyboardInput == true )
        {
            if ( Input.GetKey( KeyCode.Q ) == true )
            {
                deltaAngleH = 1.0f;
            }
            if ( Input.GetKey( KeyCode.E ) == true )
            {
                deltaAngleH = -1.0f;
            }
        }

        if ( m_UseMouseInput == true )
        {
            if ( Input.GetMouseButton( 2 ) == true && !Input.GetKey( KeyCode.LeftShift ) == true )
            {
                var deltaMousePos = ( Input.mousePosition - m_LastMousePos );
                deltaAngleH += deltaMousePos.x * m_MouseRotationMultiplier;
                deltaAngleV -= deltaMousePos.y * m_MouseRotationMultiplier;
            }
        }

        transform.SetLocalEulerAngles(
            Mathf.Min( 80.0f, Mathf.Max( 5.0f, transform.localEulerAngles.x + deltaAngleV * Time.deltaTime * m_RotationSpeed ) ),
            transform.localEulerAngles.y + deltaAngleH * Time.deltaTime * m_RotationSpeed
        );
    }

    private void UpdateZooming()
    {
        float deltaZoom = 0.0f;
        if ( m_UseKeyboardInput == true )
        {
            if ( Input.GetKey( KeyCode.F ) == true )
            {
                deltaZoom = 1.0f;
            }
            if ( Input.GetKey( KeyCode.R ) == true )
            {
                deltaZoom = -1.0f;
            }
        }
        if ( m_UseMouseInput == true )
        {
            var scroll = Input.GetAxis( "Mouse ScrollWheel" );
            deltaZoom -= scroll * m_MouseZoomMultiplier;
        }
        var zoomedOutRatio = m_CorrectZoomingOutRatio ? ( m_CurrentCameraDistance - m_MinZoomDistance ) / ( m_MaxZoomDistance - m_MinZoomDistance ) : 0.0f;
        m_CurrentCameraDistance = Mathf.Max( m_MinZoomDistance, Mathf.Min( m_MaxZoomDistance, m_CurrentCameraDistance + deltaZoom * Time.deltaTime * m_ZoomSpeed * ( zoomedOutRatio * 2.0f + 1.0f ) ) );
    }

    private void UpdatePosition()
    {
        if ( m_ObjectToFollow != null )
        {
            m_CameraTarget = Vector3.Lerp( m_CameraTarget, m_ObjectToFollow.transform.position, m_GoToSpeed );
        }

        transform.position = m_CameraTarget;
        transform.Translate( Vector3.back * m_CurrentCameraDistance );

        if ( m_AdaptToTerrainHeight == true && m_Terrain != null )
        {
            transform.SetPosition(
                null,
                Mathf.Max( m_Terrain.SampleHeight( transform.position ) + m_Terrain.transform.position.y + 10.0f, transform.position.y )
            );
        }
    }

    private void UpdateAutoMovement()
    {
        if ( m_DoingAutoMovement == true )
        {
            m_CameraTarget = Vector3.Lerp( m_CameraTarget, m_GoingToCameraTarget, m_GoToSpeed );
            if ( Vector3.Distance( m_GoingToCameraTarget, m_CameraTarget ) < 1.0f )
            {
                m_DoingAutoMovement = false;
            }
        }
    }
    #endregion
}