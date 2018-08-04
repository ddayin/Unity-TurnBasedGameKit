using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WanzyeeStudio;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 유닛을 관리
    /// </summary>
    public static class UnitManager
    {
        #region Properties

        /// <summary>
        /// 플레이어, 동맹, 적 등을 모두 포함한 유닛들 리스트
        /// </summary>
        public static List<Unit> m_Units = new List<Unit>();

        /// <summary>
        /// 플레이어가 조작 가능한 유닛들
        /// </summary>
        /// <typeparam name="UnitKnight"></typeparam>
        /// <returns></returns>
        public static List<UnitKnight> m_PlayerUnits = new List<UnitKnight>();

        /// <summary>
        /// 플레이어와 같은 편의 동맹 캐릭터들은 AI 액션을 한다.
        /// </summary>
        /// <typeparam name="UnitKnight"></typeparam>
        /// <returns></returns>
        public static List<UnitSpearman> m_AlliesUnits = new List<UnitSpearman>();

        /// <summary>
        /// 적 캐릭터들 리스트
        /// </summary>
        /// <typeparam name="UnitKnight"></typeparam>
        /// <returns></returns>
        public static List<UnitOrc> m_EnemyUnits = new List<UnitOrc>();

        #endregion

        /// <summary>
        /// 초기화
        /// </summary>
        public static void Init()
        {
            UnitTestUnitKnight();
        }

        /// <summary>
        /// 단위 테스트를 위해 하나의 유닛만 생성한다.
        /// </summary>
        private static void UnitTestUnitKnight()
        {
            UnitKnight knight = UnitFactory.CreatePlayerUnits( Vector3Int.zero );
            m_PlayerUnits.Add( knight );
            m_Units.Add( knight );
        }

        /// <summary>
        /// 지정된 위치의 플레이어 유닛을 반환한다.
        /// </summary>
        /// <param name="_position"></param>
        /// <returns></returns>
        public static UnitKnight GetPlayerUnitAt( Vector3Int _position )
        {
            for ( int i = 0; i < m_PlayerUnits.Count; i++ )
            {
                if ( m_PlayerUnits[ i ].m_Position == _position )
                {
                    return m_PlayerUnits[ i ];
                }
            }
            Debug.LogError( "unit does not exist in position : " + _position );

            return null;
        }

        /// <summary>
        /// 현재 이동 중인 플레이어 유닛을 반환한다.
        /// </summary>
        /// <returns></returns>
        public static UnitKnight GetMovingPlayer()
        {
            for ( int i = 0; i < m_PlayerUnits.Count; i++ )
            {
                if ( m_PlayerUnits[ i ].m_Command == eUnitCommand.Move )
                {
                    return m_PlayerUnits[ i ];
                }
            }

            Debug.Log( "moving player unit does not exist" );

            return null;
        }

        /// <summary>
        /// 지정된 위치의 동맹 유닛을 반환한다.
        /// </summary>
        /// <param name="_position"></param>
        /// <returns></returns>
        public static UnitSpearman GetAlliesUnitAt( Vector3Int _position )
        {
            for ( int i = 0; i < m_AlliesUnits.Count; i++ )
            {
                if ( m_AlliesUnits[ i ].m_Position == _position )
                {
                    return m_AlliesUnits[ i ];
                }
            }

            Debug.LogError( "unit does not exist in position : " + _position );

            return null;
        }

        /// <summary>
        /// 지정된 위치의 적 유닛을 반환한다.
        /// </summary>
        /// <param name="_position"></param>
        /// <returns></returns>
        public static UnitOrc GetEnemyUnitAt( Vector3Int _position )
        {
            for ( int i = 0; i < m_EnemyUnits.Count; i++ )
            {
                if ( m_EnemyUnits[ i ].m_Position == _position )
                {
                    return m_EnemyUnits[ i ];
                }
            }

            Debug.LogError( "unit does not exist in position : " + _position );

            return null;
        }

        /// <summary>
        /// 지정된 위치의 유닛을 반환한다.
        /// </summary>
        /// <param name="_position"></param>
        /// <returns></returns>
        public static Unit GetUnitAt( Vector3Int _position )
        {
            for ( int i = 0; i < m_Units.Count; i++ )
            {
                if ( m_Units[ i ].m_Position == _position )
                {
                    return m_Units[ i ];
                }
            }
            //Debug.Log( "unit does not exist in position : " + _position );

            return null;
        }

        /// <summary>
        /// 지금 현재 선택되어서 이동이 가능한 유닛을 반환한다.
        /// </summary>
        /// <returns></returns>
        public static UnitKnight GetReadyToMovePlayer()
        {
            for ( int i = 0; i < m_PlayerUnits.Count; i++ )
            {
                if ( m_PlayerUnits[ i ].m_State.m_Command == eUnitCommand.ReadyToMove )
                {
                    return m_PlayerUnits[ i ];
                }
            }

            Debug.Log( "there is no ready to move knight on tilemap" );
            return null;
        }
    }
}