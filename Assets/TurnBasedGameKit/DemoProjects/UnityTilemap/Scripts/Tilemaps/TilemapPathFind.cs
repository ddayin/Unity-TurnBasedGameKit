using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    

    /// <summary>
    /// 길찾기를 위한 타일맵 클래스
    /// </summary>
    public class TilemapPathFind : TileMapBase
    {
        #region Properties
        /// <summary>
        /// 타일의 상태를 표시하기 위한 타일맵
        /// </summary>
        private TilemapHighlight m_TilemapHighlight;

        private TilemapController m_TilemapController;

        private TilemapInput m_TilemapInput;

        /// <summary>
        /// 길찾기를 위한 Grid
        /// </summary>
        [HideInInspector]
        public Grid m_Grid;

        /// <summary>
        /// 이동 가능한 범위 내에 있는 타일들
        /// </summary>
        private HashSet<SquareTile> m_MovableTiles = new HashSet<SquareTile>();

        /// <summary>
        /// 이동 경로
        /// </summary>
        private HashSet<Vector3Int> m_Paths = new HashSet<Vector3Int>();

        /// <summary>
        /// 공격 가능한 범위 내에 있는 타일들
        /// </summary>
        private HashSet<SquareTile> m_AttackableTiles = new HashSet<SquareTile>();

        /// <summary>
        /// 4 방향
        /// </summary>
        private readonly Vector3Int[] m_Direction = new Vector3Int[ 4 ];
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

            m_TilemapHighlight = this.GetComponent<TilemapHighlight>();
            m_TilemapController = this.GetComponent<TilemapController>();
            m_TilemapInput = this.GetComponent<TilemapInput>();

            m_Tilemap = GetComponent<Tilemap>();

            m_Bounds = m_Tilemap.cellBounds;

            m_Direction[ (int) eDirection.North ] = Vector3Int.up;
            m_Direction[ (int) eDirection.South ] = Vector3Int.down;
            m_Direction[ (int) eDirection.West ] = Vector3Int.left;
            m_Direction[ (int) eDirection.East ] = Vector3Int.right;
        }
        #endregion

        /// <summary>
        /// 길찾기를 위해 필요한 그리드 초기화 (여러 번 호출될 수 있다)
        /// </summary>
        public void InitGridForPathFind()
        {
            // create the tiles map        
            Dictionary<Vector3Int, float> tilesCost = new Dictionary<Vector3Int, float>();
            HashSet<Vector3Int> obstacles = Map.instance.m_TilemapObstacle.GetObstacleTiles();

            for ( int x = m_Bounds.xMin; x < m_Bounds.xMax; x++ )
            {
                for ( int y = m_Bounds.yMin; y < m_Bounds.yMax; y++ )
                {
                    // set values here....
                    // every float in the array represent the cost of passing the tile at that position.
                    // use 0.0f for blocking tiles.                
                    Vector3Int pos = new Vector3Int( x, y, 0 );
                    tilesCost.Add( pos, 0f );
                }
            }

            // create a grid
            m_Grid = null;
            m_Grid = new Grid( m_Bounds, tilesCost );
        }

        /// <summary>
        /// 이동할 범위 내에 있는 타일 리스트를 반환한다.
        /// </summary>
        /// <param name="start">유닛이 위치한 타일</param>
        /// <returns></returns>
        private HashSet<SquareTile> SearchToMove( SquareTile _start, int _range )
        {

            HashSet<SquareTile> movableTiles = new HashSet<SquareTile>();

            Queue<SquareTile> checkNowQueue = new Queue<SquareTile>();
            checkNowQueue.Enqueue( _start );

            while ( checkNowQueue.Count > 0 )
            {
                SquareTile tile = checkNowQueue.Dequeue();

                if ( tile == null )
                {
                    continue;
                }

                // 상, 하, 좌, 우로 검색해야 하기 때문에 4번 반복
                for ( int i = 0; i < 4; i++ )
                {
                    SquareTile nextTile = m_TilemapController.GetTileAt( tile.m_Position + m_Direction[ i ] );
                    if ( nextTile == null )
                    {
                        continue;
                    }

                    // 유닛이 위치한 타일은 movableTiles에 추가하지 않는다.
                    //if ( nextTile.m_State.m_State == eTileType.UnitPlaced )
                    //{
                    //    continue;
                    //}

                    nextTile.m_Distance = tile.m_Distance + 1;

                    // 이동 범위 보다 적은 타일만 이동 가능한 타일로 추가
                    if ( nextTile.m_Distance < _range )
                    {
                        checkNowQueue.Enqueue( nextTile );
                        movableTiles.Add( nextTile );
                    }
                }
            }

            movableTiles.Remove( _start );

            return movableTiles;
        }

        /// <summary>
        /// 공격할 대상이 있는 타일을 반환한다.
        /// </summary>
        /// <param name="_start"></param>
        /// <param name="_range"></param>
        /// <returns></returns>
        private HashSet<SquareTile> SearchToAttack( SquareTile _start, int _range )
        {
            HashSet<SquareTile> attackableTiles = new HashSet<SquareTile>();

            Queue<SquareTile> checkNowQueue = new Queue<SquareTile>();
            checkNowQueue.Enqueue( _start );

            while ( checkNowQueue.Count > 0 )
            {
                SquareTile tile = checkNowQueue.Dequeue();

                if ( tile == null )
                {
                    continue;
                }

                // 상, 하, 좌, 우로 검색해야 하기 때문에 4번 반복
                for ( int i = 0; i < 4; i++ )
                {
                    SquareTile nextTile = m_TilemapController.GetTileAt( tile.m_Position + m_Direction[ i ] );
                    if ( nextTile == null )
                    {
                        continue;
                    }

                    nextTile.m_Distance = tile.m_Distance + 1;

                    // 이동 범위 보다 적은 타일만 이동 가능한 타일로 추가
                    if ( nextTile.m_Distance < _range )
                    {
                        //if ( nextTile.m_State.m_State == eTileType.UnitPlaced )
                        {
                            checkNowQueue.Enqueue( nextTile );
                            attackableTiles.Add( nextTile );
                        }
                    }
                }
            }

            attackableTiles.Remove( _start );

            return attackableTiles;
        }

        /// <summary>
        /// 이동 가능한 영역의 타일이 이미 있는 여부
        /// </summary>
        /// <returns></returns>
        public bool HasMovableTile()
        {
            if ( m_MovableTiles == null ) return false;
            if ( m_MovableTiles.Count == 0 ) return false;

            return true;
        }

        /// <summary>
        /// 해당 위치를 기준으로 이동 가능한 타일들을 표시한다.
        /// </summary>
        /// <param name="_start"></param>
        public void DrawMovableRange( Unit _unit, SquareTile _start )
        {
            m_MovableTiles = SearchToMove( _start, _unit.m_Stat.m_MoveRange );

            if ( m_MovableTiles.Count == 0 ) return;

            Dictionary<Vector3Int, float> costs = new Dictionary<Vector3Int, float>();

            HashSet<Vector3Int> obstacles = Map.instance.m_TilemapObstacle.GetObstacleTiles();

            foreach ( var tile in m_MovableTiles )
            {
                bool isObstacle = false;

                // 장애물이 있을 경우, 장애물 타일로 변경
                foreach ( var point in obstacles )
                {
                    if ( point == tile.m_Position )
                    {
                        tile.m_State = null;
                        tile.m_State = new TileStateObstacle( tile.m_Position );
                        SquareTile obstacle = SquareTileFactory.CreateObstacle( tile.m_Position );

                        m_Tilemap.SetTile( obstacle.m_Position, obstacle );

                        costs.Add( obstacle.m_Position, 0f );

                        isObstacle = true;
                        break;
                    }
                }

                // 장애물이 아닐 경우,
                if ( isObstacle == false )
                {
                    tile.m_State = null;
                    tile.m_State = new TileStateMovable( tile.m_Position );
                    SquareTile moveTile = SquareTileFactory.CreateMovable( tile.m_Position );

                    m_Tilemap.SetTile( moveTile.m_Position, moveTile );

                    costs.Add( tile.m_Position, 1f );
                }
            }

            m_Grid.UpdateGrid( costs );
        }

        /// <summary>
        /// 해당 위치를 기준으로 공격 가능한 캐릭터가 있으면 true를 반환한다.
        /// </summary>
        /// <param name="_unit"></param>
        /// <param name="_start"></param>
        /// <returns></returns>
        public bool IsAttackable( Unit _unit, SquareTile _start )
        {
            HashSet<SquareTile> attackables = SearchToAttack( _start, _unit.m_Stat.m_AttackRange );
            if ( attackables.Count == 0 )
            {
                return false;
            }
            else
            {
                // 공격 가능한 타일에 적 캐릭터가 존재하면 true를 반환한다.
                foreach ( SquareTile item in attackables )
                {
                    //if ( item.m_State.m_State == eTileType.UnitPlaced )
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 해당 위치를 기준으로 공격 가능한 타일들을 표시한다.
        /// </summary>
        /// <param name="_unit"></param>
        /// <param name="_start"></param>
        public void DrawAttackableRange( Unit _unit, SquareTile _start )
        {
            m_AttackableTiles = SearchToAttack( _start, _unit.m_Stat.m_AttackRange );

            if ( m_AttackableTiles.Count == 0 ) return;

            Dictionary<Vector3Int, float> costs = new Dictionary<Vector3Int, float>();

            HashSet<Vector3Int> obstacles = Map.instance.m_TilemapObstacle.GetObstacleTiles();

            foreach ( var tile in m_AttackableTiles )
            {
                bool isObstacle = false;

                // 장애물이 있을 경우, 장애물 타일로 변경
                foreach ( var point in obstacles )
                {
                    if ( point == tile.m_Position )
                    {
                        tile.m_State = null;
                        tile.m_State = new TileStateObstacle( tile.m_Position );
                        SquareTile obstacle = SquareTileFactory.CreateObstacle( tile.m_Position );

                        m_Tilemap.SetTile( tile.m_Position, obstacle );

                        costs.Add( tile.m_Position, 0f );

                        isObstacle = true;
                        break;
                    }
                }

                // 장애물이 아니고 타일 위에 적 캐릭터가 있으면 공격 가능한 타일로 설정한다.
                if ( isObstacle == false && tile.m_Unit.m_Type == eCampType.Enemies )
                {
                    tile.m_State = null;
                    tile.m_State = new TileStateAttackable( tile.m_Position );
                    SquareTile attackTile = SquareTileFactory.CreateAttackable( tile.m_Position );

                    m_Tilemap.SetTile( attackTile.m_Position, attackTile );

                    costs.Add( tile.m_Position, 1f );
                }
            }

            m_Grid.UpdateGrid( costs );
        }

        /// <summary>
        /// 이동 경로를 설정한다.
        /// </summary>
        /// <param name="_from"></param>
        /// <param name="_to"></param>
        public void SetPathFindGrid( Vector3Int _from, Vector3Int _to )
        {
            // 초기화 필수
            ClearPath();

            // get path
            // path will either be a list of Points (x, y), or an empty list if no path is found.
            m_Paths = PathFind.FindPath(
                m_Grid,
                _from,
                _to,
                ePathFindDistanceType.Manhattan );

            foreach ( Vector3Int point in m_Paths )
            {
                SquareTile tile = m_Tilemap.GetTile<SquareTile>( point );

                tile.m_State = null;
                tile.m_State = new TileStatePath( tile.m_Position );
                SquareTile pathTile = SquareTileFactory.CreatePath( tile.m_Position );

                m_Tilemap.SetTile( pathTile.m_Position, pathTile );
            }
        }

        /// <summary>
        /// 이동 가능한 영역의 타일들을 모두 삭제한다.
        /// </summary>
        public void ClearMovableTiles()
        {
            if ( m_MovableTiles == null ) return;

            if ( m_MovableTiles.Count == 0 ) return;

            Dictionary<Vector3Int, float> costs = new Dictionary<Vector3Int, float>();

            foreach ( var item in m_MovableTiles )
            {
                if ( item.m_State.m_State != eTileType.Movable )
                {
                    continue;
                }

                item.m_State = null;
                item.m_State = new TileStateNormal( item.m_Position );
                SquareTile normalTile = SquareTileFactory.CreateNormal( item.m_Position );

                m_Tilemap.SetTile( normalTile.m_Position, normalTile );

                costs.Add( item.m_Position, 0f );
            }

            m_Grid.UpdateGrid( costs );
        }

        /// <summary>
        /// 이동 경로를 반환한다.
        /// </summary>
        /// <returns></returns>
        public HashSet<Vector3Int> GetPath()
        {
            return m_Paths;
        }

        /// <summary>
        /// 이동 경로를 모두 삭제한다.
        /// </summary>
        private void ClearPath()
        {
            if ( m_Paths == null ) return;
            if ( m_Paths.Count == 0 ) return;

            foreach ( Vector3Int point in m_Paths )
            {
                SquareTile tile = m_Tilemap.GetTile<SquareTile>( point );
                tile.m_State = null;
                tile.m_State = new TileStateMovable( tile.m_Position );
                SquareTile moveTile = SquareTileFactory.CreateMovable( tile.m_Position );

                m_Tilemap.SetTile( moveTile.m_Position, moveTile );
            }

            m_Paths.Clear();
        }

        /// <summary>
        /// 공격 가능한 타일들을 모두 삭제한다.
        /// </summary>
        private void ClearAttackables()
        {
            if ( m_AttackableTiles == null ) return;

            if ( m_AttackableTiles.Count == 0 ) return;

            Dictionary<Vector3Int, float> costs = new Dictionary<Vector3Int, float>();

            foreach ( var item in m_AttackableTiles )
            {
                //if ( item.m_State.m_State != eTileType.Attackable )
                //{
                //    continue;
                //}

                item.m_State = null;
                item.m_State = new TileStateUnitPlaced( item.m_Position );
                SquareTile unitTile = SquareTileFactory.CreateUnitPlaced( item.m_Position );

                m_Tilemap.SetTile( unitTile.m_Position, unitTile );

                costs.Add( item.m_Position, 0f );
            }

            m_Grid.UpdateGrid( costs );
        }

        public void ResetTileDistance()
        {
            for ( int x = m_Bounds.xMin; x < m_Bounds.xMax; x++ )
            {
                for ( int y = m_Bounds.yMin; y < m_Bounds.yMax; y++ )
                {
                    Vector3Int pos = new Vector3Int( x, y, 0 );

                    SquareTile tile = m_Tilemap.GetTile<SquareTile>( pos );
                    tile.m_Distance = 0;

                    m_Tilemap.SetTile( pos, tile );
                }
            }
        }
    }

}