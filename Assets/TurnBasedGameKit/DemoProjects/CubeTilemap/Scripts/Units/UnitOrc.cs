using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

namespace TurnBasedGameKit.DemoCubeTilemap
{
    public class UnitOrc : UnitBase
    {
        #region Properties        
        private bool m_IsMouseOver = false;
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();

            Init();
        }
        
        private void OnMouseDown()
        {
            ProcessMouseDown();
        }

        private void OnMouseOver()
        {
            m_IsMouseOver = true;
        }

        private void OnMouseExit()
        {
            m_IsMouseOver = false;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void ProcessMouseDown()
        {
            UnitManager.instance.SetSelectedUnit( this );

            Map.instance.m_TileSetPathFind.DrawMovableRange( this, m_CurrentTile );

            //if ( Map.instance.m_TileSetPathFind.HasAttackable( this, m_CurrentTile ) )
            //{
            //    Map.instance.m_TileSetPathFind.DrawAttackableRange( this, m_CurrentTile );
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Init()
        {
            m_Property.m_CampType = eCampType.Player;
            m_Property.m_Command = eUnitCommand.Idle;

            m_CurrentTile = Map.instance.m_TileSetPathFind.GetTileAt( m_Property.m_Coordinate );
            
            m_CurrentTile.m_Unit = null;
            m_CurrentTile.m_Unit = this;
        }

        /// <summary>
        /// 유닛의 위치를 설정한다.
        /// </summary>
        /// <param name="_coordinate"></param>
        public override void SetPosition( Vector3Int _coordinate, bool _addOffset = false )
        {
            m_CurrentTile = Map.instance.m_TileSetPathFind.GetTileAt( _coordinate );
            
            m_CurrentTile.m_Unit = null;
            m_CurrentTile.m_Unit = this;
            m_Property.m_Coordinate = _coordinate;
            transform.position = new Vector3( _coordinate.x, 0, _coordinate.y );
        }

        /// <summary>
        /// 대기 상태로 설정
        /// </summary>
        public override void SetToIdle()
        {
            m_Property.m_Command = eUnitCommand.Idle;
            m_Animator.SetBool( "move", false );
        }

        /// <summary>
        /// 
        /// </summary>
        public override void SetColor()
        {
            base.SetColor();
        }

        /// <summary>
        /// 이동
        /// </summary>
        /// <param name="_targetPosition"></param>
        public override void Move( Vector3Int _targetPosition )
        {
            m_CurrentTile.m_Unit = null;

            HashSet<Vector3Int> path = Map.instance.m_TileSetPathFind.GetPath();
            HashSet<Vector3Int> converted = ConvertPathCoordinates( path );

            m_Animator.SetBool( "move", true );

            // 이동할 방향 설정하고 왼쪽으로 이동할 경우, 스프라이트를 뒤집는다.
            Vector3Int firstPoint = converted.First();
            Vector3Int lastPoint = converted.Last();
            if ( lastPoint.x - firstPoint.x < 0 )
            {
                m_Property.m_LookDirection = eDirection.West;
                
            }
            else
            {
                m_Property.m_LookDirection = eDirection.East;
                
            }

            // 실제로 목표 지점까지 이동
            MoveByPathTile( converted, _targetPosition );
        }

        private HashSet<Vector3Int> ConvertPathCoordinates( HashSet<Vector3Int> _path )
        {
            HashSet<Vector3Int> pathSet = new HashSet<Vector3Int>();
            
            foreach ( Vector3Int p in _path )
            {
                Vector3Int newPoint = new Vector3Int( p.x, 0, p.y );
                pathSet.Add( newPoint );
            }

            return pathSet;
        }

        /// <summary>
        /// 이동 경로를 따라 타일 단위로 이동
        /// </summary>
        /// <param name="_path"></param>
        private void MoveByPathTile( HashSet<Vector3Int> _path, Vector3Int _targetPosition )
        {
            if ( _path.Count == 0 )
            {
                Debug.LogError( "_path.Count == 0" );
                return;
            }

            StartCoroutine( MoveCoroutine( _path, _targetPosition ) );
        }

        /// <summary>
        /// 유닛을 실제로 이동 시키고 각종 초기화를 수행한다.
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        IEnumerator MoveCoroutine( HashSet<Vector3Int> _path, Vector3Int _targetPosition )
        {
            foreach ( Vector3Int point in _path )
            {
                Vector3 pos = point;
                transform.DOMove( pos, 1f );
                yield return new WaitForSeconds( 1f );

                if ( point == _targetPosition ) break;
            }
    
            SetPosition( _targetPosition, true );

            // 초기화
            SetToIdle();

            Map.instance.m_TileSetPathFind.InitAllTiles();
        }


        /// <summary>
        /// 공격
        /// </summary>
        /// <param name="_targetPosition"></param>
        public override void Attack( Vector3Int _targetPosition )
        {

        }
    }
}