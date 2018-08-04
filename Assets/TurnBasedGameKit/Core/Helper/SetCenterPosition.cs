using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit
{
    /// <summary>
    /// 유닛의 위치를 타일 위로 조정하는 클래스
    /// </summary>
    public class SetCenterPosition : MonoBehaviour
    {
        #region Properties
        /// <summary>
        /// 타일 그리드의 좌표
        /// </summary>
        private Vector3Int m_GridPosition;

        /// <summary>
        /// 실제로 월드 상의 위치
        /// </summary>
        private Vector3 m_WorldPosition;

        /// <summary>
        /// 이동할 값
        /// </summary>
        public Vector3 m_Offeset = new Vector3( 0.5f, 1.4f, 0f );
        #endregion

        #region MonoBehaviour
        private void Awake()
        {

        }
        #endregion

        /// <summary>
        /// 그리드 상의 좌표로 실제 위치를 조정해서 위치시킨다.
        /// </summary>
        /// <param name="_position">타일 그리드의 좌표</param>
        public void SetPosition( Vector3Int _position )
        {
            m_GridPosition = _position;

            m_WorldPosition = new Vector3( m_GridPosition.x, m_GridPosition.y, 0f );
            m_WorldPosition += m_Offeset;

            transform.position = m_WorldPosition;
        }

        /// <summary>
        /// 그리드 상의 좌표를 실제 월드 상의 위치를 오프셋 값을 더해서 반환한다.
        /// </summary>
        /// <param name="_position"></param>
        /// <returns></returns>
        public Vector3 GetWorldPosition( Vector3Int _position )
        {
            Vector3 position = new Vector3( _position.x, _position.y, 0f );
            position += m_Offeset;

            return position;
        }
    }
}