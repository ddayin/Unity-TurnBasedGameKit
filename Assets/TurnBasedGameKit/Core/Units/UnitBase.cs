using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit
{
    /// <summary>
    /// 유닛에게 내릴 명령 열거체
    /// </summary>
    public enum eUnitCommand
    {
        Idle = 0,       // 대기
        ReadyToAttack,  // 공격하기 직전 준비 상태
        Attack,         // 공격
        Defend,         // 방어
        Hurt,           // 맞음
        Dizzy,          // 혼돈 상태
        ReadyToMove,    // 이동 준비 상태
        Move,           // 이동
        CastMagic,      // 스킬 시전
        Die,            // 죽음
        Win             // 스테이즈에서 승리
    }

    [System.Serializable]
    public class UnitProperties
    {
        /// <summary>
        /// 아이디
        /// </summary>
        public int m_ID = 0;

        /// <summary>
        /// 유닛 진영
        /// </summary>
        public eCampType m_CampType = eCampType.Player;

        /// <summary>
        /// 유닛에게 내릴 명령
        /// </summary>
        public eUnitCommand m_Command = eUnitCommand.Idle;

        /// <summary>
        /// 유닛 상태 클래스
        /// </summary>
        //public UnitState m_State = null;

        /// <summary>
        /// 바라보는 방향
        /// </summary>
        public eDirection m_LookDirection = eDirection.South;

        /// <summary>
        /// 유닛의 현재 위치 (좌표)
        /// </summary>
        public Vector3Int m_Coordinate = Vector3Int.zero;
    }

    /// <summary>
    /// 유닛의 스탯
    /// </summary>
    [System.Serializable]
    public class UnitStat
    {
        /// <summary>
        /// 이동 가능한 범위
        /// </summary>
        public int m_MoveRange = 0;

        /// <summary>
        /// 체력
        /// </summary>
        public float m_HealthPoint = 0f;

        /// <summary>
        /// 마력
        /// </summary>
        public float m_MagicPoint = 0f;

        /// <summary>
        /// 공격 가능한 범위
        /// </summary>
        public int m_AttackRange = 0;

        /// <summary>
        /// 유닛이 공격할 때 상대방에게 입히는 피해 수치
        /// </summary>
        public float m_HitPoint = 0f;

        /// <summary>
        /// 이동 속도
        /// </summary>
        public float m_MoveSpeed = 0.2f;
    }

    /// <summary>
    /// 
    /// </summary>
    public class UnitBase : MonoBehaviour
    {
        #region Properties
        public int m_ID;

        public UnitProperties m_Property = new UnitProperties();

        public UnitStat m_Stat = new UnitStat();

        public TileBase m_CurrentTile = null;

        public Animator m_Animator = null;
        #endregion

        #region MonoBehaviour
        protected virtual void Awake()
        {
            m_Animator = this.GetComponentInChildren<Animator>();
        }

        protected virtual void Start()
        {

        }
        #endregion

        /// <summary>
        /// 초기화
        /// </summary>
        public virtual void Init()
        {

        }

        /// <summary>
        /// 유닛의 위치를 설정한다.
        /// </summary>
        /// <param name="_position"></param>
        public virtual void SetPosition( Vector3Int _position, bool _addOffset = false )
        {
            
        }

        /// <summary>
        /// 대기 상태로 설정
        /// </summary>
        public virtual void SetToIdle()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void SetColor()
        {

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