using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    public class TilemapController : TileMapBase
    {
        /// <summary>
        /// 
        /// </summary>
        private TilemapHighlight m_TilemapHighlight;

        /// <summary>
        /// 
        /// </summary>
        private TilemapPathFind m_TilemapPathFind;

        protected override void Awake()
        {
            base.Awake();

            m_TilemapHighlight = this.GetComponent<TilemapHighlight>();
            m_TilemapPathFind = this.GetComponent<TilemapPathFind>();
        }

        private void Start()
        {
            // 1. 일반 타일들 생성
            CreateTilesToNormal();

            // 2. 
            UnitManager.Init();

            // 3. 길찾기를 위한 그리드 생성
            m_TilemapPathFind.InitGridForPathFind();
        }

        #region 타일 컨트롤

        /// <summary>
        /// Tilemap에 SquareTile들로 설정한다.
        /// </summary>
        private void CreateTilesToNormal()
        {
            // Tilemap 크기에 맞추어 SquareTile들을 생성한다.
            for ( int x = m_Bounds.xMin; x < m_Bounds.xMax; x++ )
            {
                for ( int y = m_Bounds.yMin; y < m_Bounds.yMax; y++ )
                {
                    Vector3Int current = new Vector3Int( x, y, 0 );

                    SquareTile normal = ScriptableObject.CreateInstance<SquareTile>();
                    normal.m_Position = new Vector3Int( x, y, 0 );
                    normal.m_Cost = 1f;
                    normal.m_State = new TileStateNormal( normal.m_Position );
                    SquareTile tile = SquareTileFactory.CreateNormal( normal.m_Position );

                    m_Tilemap.SetTile( current, tile );
                }
            }
        }

        /// <summary>
        /// 지정한 위치의 타일을 유닛이 위치한 타일로 설정한다.
        /// </summary>
        /// <param name="_position"></param>
        public void SetTileAsUnitPlaced( Vector3Int _position )
        {
            SquareTile tile = ScriptableObject.CreateInstance<SquareTile>();
            tile.m_Position = _position;
            tile.m_Cost = 0f;
            tile.m_State = null;
            tile.m_State = new TileStateUnitPlaced( tile.m_Position );
            SquareTile unitTile = SquareTileFactory.CreateUnitPlaced( tile.m_Position );
            unitTile.m_Unit = UnitManager.GetUnitAt( _position );
            if ( unitTile.m_Unit == null )
            {
                Debug.LogError( "unitTile.m_Unit == null" );
                return;
            }

            m_Tilemap.SetTile( unitTile.m_Position, unitTile );
        }

        /// <summary>
        /// 유닛이 위치한 곳에는 모두 유닛이 있는 타일들로 설정
        /// </summary>
        public void SetAllUnitPlacedTiles()
        {
            for ( int i = 0; i < UnitManager.m_Units.Count; i++ )
            {
                Vector3Int position = UnitManager.m_Units[ i ].m_Position;
                SetTileAsUnitPlaced( position );
            }
        }

        /// <summary>
        /// 해당 위치에 타일을 삭제한다.
        /// </summary>
        /// <param name="_position"></param>
        public void DeleteTile( Vector3Int _position )
        {
            SquareTile tile = m_Tilemap.GetTile<SquareTile>( _position );
            if ( tile == null ) return;

            // 이동 가능한 타일 / 유닛이 위치한 타일 / 이동 경로 타일 일 때는 삭제할 필요가 없다.
            if ( tile.m_State.m_State == eTileType.Movable )
            {
                return;
            }
            

            // 그 외의 타일일 경우에는 일반 타일로 교체한다.
            tile.m_State = null;
            tile.m_State = new TileStateNormal( tile.m_Position );
            SquareTile normalTile = SquareTileFactory.CreateNormal( tile.m_Position );

            m_Tilemap.SetTile( normalTile.m_Position, normalTile );

            m_TilemapPathFind.m_Grid.UpdateTile( _position, 0f );
        }

        /// <summary>
        /// 해당 위치의 타일을 가져온다.
        /// </summary>
        /// <param name="_position"></param>
        /// <returns></returns>
        public SquareTile GetTileAt( Vector3Int _position )
        {
            return m_Tilemap.GetTile<SquareTile>( _position );
        }
        #endregion

        #region 타일들 초기화
        /// <summary>
        /// 유닛이 위치한 타일을 제외하고 모든 타일들을 모두 지운다.
        /// </summary>
        public void ResetAllTiles()
        {
            Dictionary<Vector3Int, float> costs = new Dictionary<Vector3Int, float>();

            for ( int x = m_Bounds.xMin; x < m_Bounds.xMax; x++ )
            {
                for ( int y = m_Bounds.yMin; y < m_Bounds.yMax; y++ )
                {
                    Vector3Int pos = new Vector3Int( x, y, 0 );

                    SquareTile tile = m_Tilemap.GetTile<SquareTile>( pos );

                    // 유닛이 위치한 타일은 초기화 할 필요 없다.
                    //if ( tile.m_State.m_State == eTileType.UnitPlaced )
                    //{
                    //    //tile.m_Cost = 0;
                    //    //tile.m_Distance = 0;
                    //    //m_Tilemap.SetTile( tile.m_Position, tile );
                    //    continue;
                    //}
                    // 타일 위에 유닛이 없으면 일반 타일로 초기화시킨다.
                    //else
                    {
                        tile.m_State = null;
                        tile.m_State = new TileStateNormal( tile.m_Position );
                        SquareTile normalTile = SquareTileFactory.CreateNormal( tile.m_Position );

                        m_Tilemap.SetTile( normalTile.m_Position, normalTile );

                        costs.Add( tile.m_Position, 0f );
                    }
                }
            }
            // 테스트 코드
            //for ( int x = m_Bounds.xMin; x < m_Bounds.xMax; x++ )
            //{
            //    for ( int y = m_Bounds.yMin; y < m_Bounds.yMax; y++ )
            //    {
            //        Vector3Int pos = new Vector3Int( x, y, 0 );

            //        SquareTile tile = m_Tilemap.GetTile<SquareTile>( pos );
            //        if ( tile.m_Distance != 0 )
            //        {
            //            Debug.Log( pos );
            //        }
            //    }
            //}

            m_TilemapPathFind.m_Grid.UpdateGrid( costs );
        }

        public void ResetAllExceptMoveTiles()
        {
            Dictionary<Vector3Int, float> costs = new Dictionary<Vector3Int, float>();

            for ( int x = m_Bounds.xMin; x < m_Bounds.xMax; x++ )
            {
                for ( int y = m_Bounds.yMin; y < m_Bounds.yMax; y++ )
                {
                    Vector3Int pos = new Vector3Int( x, y, 0 );

                    SquareTile tile = m_Tilemap.GetTile<SquareTile>( pos );

                    //if ( tile.m_State.m_State == eTileType.UnitPlaced )
                    //{
                    //    continue;
                    //}
                    /*else */if ( tile.m_State.m_State == eTileType.Movable )
                    {
                        continue;
                    }
                    //else if ( tile.m_State.m_State == eTileType.Path )
                    //{
                    //    continue;
                    //}
                    //else if ( tile.m_State.m_State == eTileType.Attackable )
                    //{
                    //    continue;
                    //}
                    else
                    {
                        tile.m_State = null;
                        tile.m_State = new TileStateNormal( tile.m_Position );
                        SquareTile normalTile = SquareTileFactory.CreateNormal( tile.m_Position );

                        m_Tilemap.SetTile( normalTile.m_Position, normalTile );

                        costs.Add( tile.m_Position, 0f );
                    }
                }
            }

            m_TilemapPathFind.m_Grid.UpdateGrid( costs );
        }
        #endregion
    }
}