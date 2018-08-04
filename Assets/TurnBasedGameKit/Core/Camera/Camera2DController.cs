using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WanzyeeStudio;

namespace TurnBasedGameKit
{

    /// <summary>
    /// 카메라 조작
    /// </summary>
    public class Camera2DController : MonoBehaviour
    {
        #region Properties
        private Camera m_Camera;

        /// <summary>
        /// 마우스 드래그로 이동할 때 위치
        /// </summary>
        private Vector3 m_DragOrigin = Vector3.zero;

        /// <summary>
        /// 확대 축소 시킬 값
        /// </summary>
        private float m_ZoomTo = 0f;

        /// <summary>
        /// 확대 축소할 때 더하거나 뺄 값
        /// </summary>
        private float m_ZoomFactor = 0.1f;

        /// <summary>
        /// 마우스 드래그할 때 이동 속도
        /// </summary>
        public float m_DragMoveSpeed = 1f;

        /// <summary>
        /// 키 입력으로 이동시킬 때 속도
        /// </summary>
        public float m_KeyMoveSpeed = 5f;

        /// <summary>
        /// 이동 중인지
        /// </summary>
        public bool m_IsMoving = false;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            m_Camera = Camera.main;

            m_Camera.orthographicSize = 5.0f;

            m_ZoomTo = m_Camera.orthographicSize;

        }

        private void LateUpdate()
        {
            if ( m_Camera == null ) return;

            m_IsMoving = false;

            if ( Move() == false )
            {
                m_IsMoving = false;
            }

            Zoom();
        }
        #endregion

        /// <summary>
        /// 이동
        /// </summary>
        public bool Move()
        {
            Vector3 move = Vector3.zero;

            // 화살표 키로도 조작이 가능하도록
            if ( Input.GetKey( KeyCode.UpArrow ) )
            {
                move += new Vector3( 0f, m_KeyMoveSpeed * Time.deltaTime, 0f );
            }
            if ( Input.GetKey( KeyCode.DownArrow ) )
            {
                move += new Vector3( 0f, -m_KeyMoveSpeed * Time.deltaTime, 0f );
            }
            if ( Input.GetKey( KeyCode.LeftArrow ) )
            {
                move += new Vector3( -m_KeyMoveSpeed * Time.deltaTime, 0f, 0f );
            }
            if ( Input.GetKey( KeyCode.RightArrow ) )
            {
                move += new Vector3( m_KeyMoveSpeed * Time.deltaTime, 0f, 0f );
            }

            if ( move != Vector3.zero )
            {
                m_Camera.transform.Translate( move, Space.World );
                m_IsMoving = true;
                return true;
            }

            // reference - https://forum.unity.com/threads/click-drag-camera-movement.39513/

            if ( Input.GetMouseButtonDown( 0 ) == true )
            {
                m_DragOrigin = Input.mousePosition;
                m_IsMoving = true;
                return true;
            }

            if ( Input.GetMouseButton( 0 ) == false )
            {
                m_IsMoving = false;
                return false;
            }

            // 마우스 드래그로 이동
            Vector3 pos = Camera.main.ScreenToViewportPoint( m_DragOrigin - Input.mousePosition );
            move = new Vector3( pos.x * m_DragMoveSpeed, pos.y * m_DragMoveSpeed, 0f );

            m_Camera.transform.Translate( move, Space.World );

            return true;
        }



        /// <summary>
        /// 확대 축소
        /// </summary>
        public void Zoom()
        {
            float y = 0f;

            if ( Input.GetKey( KeyCode.Period ) == true )
            {
                y = -1f;
            }
            else if ( Input.GetKey( KeyCode.Slash ) == true )
            {
                y = 1f;
            }
            else
            {
                // Attaches the float y to scrollwheel up or down            
                y = Input.GetAxisRaw( "Mouse ScrollWheel" );
            }

            if ( y > 0 )
            {
                m_ZoomTo -= m_ZoomFactor;
            }
            else if ( y < 0 )
            {
                m_ZoomTo += m_ZoomFactor;
            }
            else
            {
                return;
            }

            // 예외 처리
            if ( m_ZoomTo < 1f )
            {
                m_ZoomTo = 1f;
            }

            m_Camera.orthographicSize = m_ZoomTo;
        }

    }
}