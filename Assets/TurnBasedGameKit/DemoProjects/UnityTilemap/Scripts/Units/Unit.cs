using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    



    /// <summary>
    /// 부모가 되는 유닛
    /// </summary>
    public class Unit : MonoBehaviour
    {
        #region Properties
        /// <summary>
        /// 아이디
        /// </summary>
        public int m_ID;

        /// <summary>
        /// 
        /// </summary>
        public UnitStat m_Stat;

        /// <summary>
        /// 유닛 진영
        /// </summary>
        public eCampType m_Type;

        /// <summary>
        /// 유닛에게 내릴 명령
        /// </summary>
        public eUnitCommand m_Command;

        /// <summary>
        /// 유닛 상태 클래스
        /// </summary>
        public UnitState m_State;

        /// <summary>
        /// 바라보는 방향
        /// </summary>
        public eDirection m_Direction;

        /// <summary>
        /// 유닛의 현재 위치 (좌표)
        /// </summary>
        public Vector3Int m_Position = Vector3Int.zero;

        /// <summary>
        /// 유닛이 현재 위치하고 있는 타일
        /// </summary>
        public SquareTile m_CurrentTile = null;

        /// <summary>
        /// 
        /// </summary>
        public Animator m_Animator;

        /// <summary>
        /// 
        /// </summary>
        public SpriteRenderer m_SpriteRenderer;

        /// <summary>
        /// 
        /// </summary>
        protected SetCenterPosition m_SetCenterPosition;

        /// <summary>
        /// 
        /// </summary>
        protected SetZOrder m_SetZOrder;
        #endregion

        #region MonoBehaviour
        protected virtual void Awake()
        {
            m_Stat = GetComponent<UnitStat>();
            m_Animator = GetComponent<Animator>();
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_SetCenterPosition = GetComponent<SetCenterPosition>();
            m_SetZOrder = GetComponent<SetZOrder>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {

        }
        #endregion

        /// <summary>
        /// 유닛의 위치를 설정한다.
        /// </summary>
        /// <param name="_position"></param>
        public void SetPosition( Vector3Int _position, bool _addOffset = false )
        {
            m_Position = _position;

            m_SetZOrder.SetSortingOrder( m_Position.y );

            Vector3 worldPosition = Vector3.zero;
            if ( _addOffset == true )
            {
                worldPosition = m_SetCenterPosition.GetWorldPosition( _position );
            }
            else
            {
                worldPosition = new Vector3( _position.x, _position.y, _position.z );
            }

            transform.position = new Vector3( worldPosition.x, worldPosition.y, worldPosition.z );

            m_CurrentTile = Map.instance.m_TilemapHighlight.m_Tilemap.GetTile<SquareTile>( m_Position );
        }

        /// <summary>
        /// 대기 상태로 설정
        /// </summary>
        public void SetToIdle()
        {
            m_Command = eUnitCommand.Idle;

            m_State = null;
            UnitStateParameter param = new UnitStateParameter();
            param.m_Unit = this;
            param.m_SpriteRenderer = m_SpriteRenderer;
            param.m_Animator = m_Animator;

            m_State = new UnitStateIdle( param );
            m_State.Draw();
        }

        /// <summary>
        /// 이동
        /// </summary>
        /// <param name="_targetPosition"></param>
        public virtual void Move( Vector3Int _targetPosition )
        {

        }

        /// <summary>
        /// 공격
        /// </summary>
        /// <param name="_targetPosition"></param>
        public virtual void Attack( Vector3Int _targetPosition )
        {

        }
    }
}