using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoCubeTilemap
{
    /// <summary>
    /// tile set for path finding
    /// </summary>
    public class TileSetPathFind : TileSetBase
    {
        #region Properties
        /// <summary>
        /// bound of this tile set
        /// </summary>
        private BoundsInt m_Bounds; 

        /// <summary>
        /// 
        /// </summary>
        [HideInInspector]
        public Grid m_Grid;

        /// <summary>
        /// 
        /// </summary>
        private HashSet<TileBase> m_MovableTiles = new HashSet<TileBase>();

        /// <summary>
        /// 
        /// </summary>
        private HashSet<Vector3Int> m_Paths = new HashSet<Vector3Int>();

        /// <summary>
        /// 
        /// </summary>
        private HashSet<TileBase> m_AttackableTiles = new HashSet<TileBase>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Vector3Int[] m_SearchDirection = new Vector3Int[ 6 ];
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
            
            m_Bounds.xMin = 0;
            m_Bounds.xMax = m_Width;
            m_Bounds.yMin = 0;
            m_Bounds.yMax = m_Length;
            m_Bounds.zMin = 0;
            m_Bounds.zMax = 0;

            m_SearchDirection[ (int) eDirection.North ] = new Vector3Int( 0, 1, 0 );
            m_SearchDirection[ (int) eDirection.South ] = new Vector3Int( 0, -1, 0 );
            m_SearchDirection[ (int) eDirection.West ] = new Vector3Int( -1, 0, 0 );
            m_SearchDirection[ (int) eDirection.East ] = new Vector3Int( 1, 0, 0 );
            m_SearchDirection[ (int) eDirection.NorthWest ] = new Vector3Int( -1, 1, 0 );   // for hexagonal
            m_SearchDirection[ (int) eDirection.SouthEast ] = new Vector3Int( 1, -1, 0 ); // for hexagonal
            
        }

        protected override void Start()
        {
            base.Start();

            InitGrid();
        }
        #endregion

        /// <summary>
        /// initialize grid for path-finding
        /// </summary>
        public void InitGrid()
        {
            // create the tiles map        
            Dictionary<Vector3Int, float> tilesCost = new Dictionary<Vector3Int, float>();

            foreach ( KeyValuePair<Vector3Int, TileBase> kv in m_Tiles )
            {
                if ( kv.Value != null )
                {
                    if ( kv.Value.m_Property.m_Type == eTileType.Movable )
                    {
                        if ( kv.Value.m_Unit != null )
                        {
                            tilesCost.Add( kv.Key, 0f );
                        }
                        else if ( kv.Value.m_Unit == null )
                        {
                            tilesCost.Add( kv.Key, 1f );
                        }
                    }
                    else if ( kv.Value.m_Property.m_Type == eTileType.Obstacle )
                    {
                        tilesCost.Add( kv.Key, 0f );
                    }
                    else if ( kv.Value.m_Property.m_Type == eTileType.Breakable )
                    {
                        tilesCost.Add( kv.Key, 0f );
                    }
                }
            }

            // create a grid
            m_Grid = null;
            m_Grid = new Grid( m_Bounds, tilesCost );
        }

        /// <summary>
        /// initialize all tiles
        /// </summary>
        public void InitAllTiles()
        {
            Dictionary<Vector3Int, float> tilesCost = new Dictionary<Vector3Int, float>();

            foreach ( KeyValuePair<Vector3Int, TileBase> kv in m_Tiles )
            {
                TileBase tile = kv.Value;
                tile.Init();

                tile.SetColor( tile.m_Property.m_Type );

                if ( tile.m_Property.m_Type == eTileType.Movable )
                {
                    if ( tile.m_Unit == null )
                    {
                        tilesCost.Add( kv.Key, 1f );
                    }
                    else
                    {
                        tilesCost.Add( kv.Key, 0f );
                    }
                }
                else
                {
                    tilesCost.Add( kv.Key, 0f );
                }
            }

            m_Grid.UpdateGrid( tilesCost );

            m_MovableTiles.Clear();
            m_AttackableTiles.Clear();
            m_Paths.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_start"></param>
        /// <param name="_range"></param>
        /// <returns></returns>
        private HashSet<TileBase> SearchToMove( TileBase _start, int _range )
        {
            HashSet<TileBase> movableTiles = new HashSet<TileBase>();

            Queue<TileBase> checkNowQueue = new Queue<TileBase>();
            checkNowQueue.Enqueue( _start );

            while ( checkNowQueue.Count > 0 )
            {
                TileBase tile = checkNowQueue.Dequeue();

                if ( tile == null )
                {
                    continue;
                }

                int search = 0;
                if ( m_TileShape == eTileShape.Cube )
                {
                    search = 4;
                }
                else if ( m_TileShape == eTileShape.Hex )
                {
                    search = 6;
                }
                
                for ( int i = 0; i < search; i++ )
                {
                    TileBase nextTile = GetTileAt( tile.m_Property.m_Coordinate + m_SearchDirection[ i ] );

                    if ( nextTile == null )
                    {
                        continue;
                    }

                    if ( nextTile.m_Unit != null )
                    {
                        continue;
                    }

                    nextTile.m_Property.m_Distance = tile.m_Property.m_Distance + 1;

                    if ( nextTile.m_Property.m_Distance < _range )
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
        /// 
        /// </summary>
        /// <param name="_start"></param>
        /// <param name="_range"></param>
        /// <returns></returns>
        private HashSet<TileBase> SearchToAttack( TileBase _start, int _range )
        {
            HashSet<TileBase> attackableTiles = new HashSet<TileBase>();

            Queue<TileBase> checkNowQueue = new Queue<TileBase>();
            checkNowQueue.Enqueue( _start );

            while ( checkNowQueue.Count > 0 )
            {
                TileBase tile = checkNowQueue.Dequeue();

                if ( tile == null )
                {
                    continue;
                }

                int search = 0;
                if ( m_TileShape == eTileShape.Cube )
                {
                    search = 4;
                }
                else if ( m_TileShape == eTileShape.Hex )
                {
                    search = 6;
                }

                for ( int i = 0; i < search; i++ )
                {
                    TileBase nextTile = GetTileAt( tile.m_Property.m_Coordinate + m_SearchDirection[ i ] );
                    if ( nextTile == null )
                    {
                        continue;
                    }

                    nextTile.m_Property.m_Distance = tile.m_Property.m_Distance + 1;

                    if ( nextTile.m_Property.m_Distance < _range )
                    {
                        //if ( nextTile.m_Unit != null )
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
        /// 
        /// </summary>
        /// <returns></returns>
        public bool HasMovableTile()
        {
            if ( m_MovableTiles == null ) return false;
            if ( m_MovableTiles.Count == 0 ) return false;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_start"></param>
        public void DrawMovableRange( UnitBase _unit, TileBase _start )
        {
            ClearMovables();

            m_MovableTiles = SearchToMove( _start, _unit.m_Stat.m_MoveRange );

            if ( m_MovableTiles.Count == 0 ) return;

            Dictionary<Vector3Int, float> costs = new Dictionary<Vector3Int, float>();

            foreach ( var tile in m_MovableTiles )
            {
                if ( tile.m_Property.m_Type == eTileType.Movable )
                {
                    if ( tile.m_Unit != null )
                    {
                        costs.Add( tile.m_Property.m_Coordinate, 0f );
                        continue;
                    }

                    tile.m_Property.m_IsMovable = true;
                    tile.SetColor( eTileType.Movable );
                    costs.Add( tile.m_Property.m_Coordinate, 1f );
                }
                else if ( tile.m_Property.m_Type == eTileType.Obstacle )
                {
                    tile.m_Property.m_IsMovable = false;
                    tile.SetColor( eTileType.Obstacle );
                    costs.Add( tile.m_Property.m_Coordinate, 0f );
                }
                else if ( tile.m_Property.m_Type == eTileType.Breakable )
                {
                    tile.m_Property.m_IsMovable = false;
                    tile.SetColor( eTileType.Breakable );
                    costs.Add( tile.m_Property.m_Coordinate, 0f );
                }
            }

            m_Grid.UpdateGrid( costs );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_unit"></param>
        /// <param name="_start"></param>
        /// <returns></returns>
        public bool HasAttackable( UnitBase _unit, TileBase _start )
        {
            if ( m_AttackableTiles == null || m_AttackableTiles.Count == 0 )
            {
                m_AttackableTiles = SearchToAttack( _start, _unit.m_Stat.m_AttackRange );
            }
            if ( m_AttackableTiles == null ) return false;
            if ( m_AttackableTiles.Count == 0 ) return false;
            
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_unit"></param>
        /// <param name="_start"></param>
        public void DrawAttackableRange( UnitBase _unit, TileBase _start )
        {
            //m_AttackableTiles = SearchToAttack( _start, _unit.m_Stat.m_AttackRange );

            if ( m_AttackableTiles.Count == 0 ) return;
            if ( m_AttackableTiles == null ) return;

            //Debug.Log( "m_AttackableTiles.Count = " + m_AttackableTiles.Count );

            Dictionary<Vector3Int, float> costs = new Dictionary<Vector3Int, float>();

            foreach ( var tile in m_AttackableTiles )
            {
                if ( tile.m_Unit != null )
                //if ( tile.m_Unit.m_Property.m_CampType == eCampType.Enemies )
                {
                    tile.m_Property.m_IsAttackable = true;
                    tile.SetColor( eTileType.Movable );

                    costs.Add( tile.m_Property.m_Coordinate, 0f );
                }
            }

            m_Grid.UpdateGrid( costs );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_from"></param>
        /// <param name="_to"></param>
        public void DrawPath( Vector3Int _from, Vector3Int _to )
        {
            ClearPath();

            // get path
            // path will either be a list of Points (x, y), or an empty list if no path is found.
            m_Paths = PathFind.FindPath(
                m_Grid,
                _from,
                _to,
                ePathFindDistanceType.Manhattan );

            Dictionary<Vector3Int, float> costs = new Dictionary<Vector3Int, float>();
            
            foreach ( Vector3Int point in m_Paths )
            {
                TileBase tile = GetTileAt( point );
                if ( tile.m_Unit != null )
                {
                    costs.Add( point, 0f );
                    continue;
                }

                tile.m_Property.m_IsPath = true;
                tile.SetColor( eTileType.Movable );

                costs.Add( point, 1f );
            }

            m_Grid.UpdateGrid( costs );
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearMovables()
        {
            if ( m_MovableTiles == null ) return;

            if ( m_MovableTiles.Count == 0 ) return;

            Dictionary<Vector3Int, float> costs = new Dictionary<Vector3Int, float>();

            foreach ( var item in m_MovableTiles )
            {
                if ( item.m_Property.m_Type != eTileType.Movable )
                {
                    costs.Add( item.m_Property.m_Coordinate, 0f );
                    continue;
                }
                if ( item.m_Unit != null )
                {
                    costs.Add( item.m_Property.m_Coordinate, 0f );
                    continue;
                }

                item.m_Property.m_IsMovable = false;
                item.SetColor( eTileType.Movable );
                costs.Add( item.m_Property.m_Coordinate, 1f );
            }
            m_MovableTiles.Clear();
            m_MovableTiles = null;

            m_Grid.UpdateGrid( costs );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HashSet<Vector3Int> GetPath()
        {
            return m_Paths;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearPath()
        {
            if ( m_Paths == null ) return;
            if ( m_Paths.Count == 0 ) return;

            Dictionary<Vector3Int, float> costs = new Dictionary<Vector3Int, float>();

            foreach ( Vector3Int point in m_Paths )
            {
                TileBase tile = GetTileAt( point );

                if ( tile.m_Unit != null )
                {
                    costs.Add( point, 0f );
                    continue;
                }

                tile.m_Property.m_IsPath = false;
                tile.SetColor( eTileType.Movable );

                costs.Add( point, 1f );
            }
            
            m_Paths.Clear();
            m_Paths = null;

            m_Grid.UpdateGrid( costs );
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearAttackables()
        {
            if ( m_AttackableTiles == null ) return;

            if ( m_AttackableTiles.Count == 0 ) return;

            Dictionary<Vector3Int, float> costs = new Dictionary<Vector3Int, float>();

            foreach ( var item in m_AttackableTiles )
            {
                if ( item.m_Unit != null )
                {
                    costs.Add( item.m_Property.m_Coordinate, 0f );
                    continue;
                }

                item.m_Property.m_IsAttackable = false;
                item.SetColor( eTileType.Movable );

                costs.Add( item.m_Property.m_Coordinate, 1f );
            }

            m_AttackableTiles.Clear();
            m_AttackableTiles = null;

            m_Grid.UpdateGrid( costs );
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetTileDistance()
        {
            for ( int x = m_Bounds.xMin; x < m_Bounds.xMax; x++ )
            {
                for ( int y = m_Bounds.yMin; y < m_Bounds.yMax; y++ )
                {
                    Vector3Int pos = new Vector3Int( x, y, 0 );

                    TileBase tile = GetTileAt( pos );
                    tile.m_Property.m_Distance = 0;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearGrid()
        {
            Dictionary<Vector3Int, float> tilesCost = new Dictionary<Vector3Int, float>();

            foreach ( KeyValuePair<Vector3Int, TileBase> kv in m_Tiles )
            {
                if ( kv.Value != null )
                {
                    if ( kv.Value.m_Property.m_Type == eTileType.Movable )
                    {
                        if ( kv.Value.m_Unit != null )
                        {
                            tilesCost.Add( kv.Key, 0f );
                        }
                        else if ( kv.Value.m_Unit == null )
                        {
                            tilesCost.Add( kv.Key, 1f );
                        }
                    }
                    else if ( kv.Value.m_Property.m_Type == eTileType.Obstacle )
                    {
                        tilesCost.Add( kv.Key, 0f );
                    }
                    else if ( kv.Value.m_Property.m_Type == eTileType.Breakable )
                    {
                        tilesCost.Add( kv.Key, 0f );
                    }
                }
            }

            m_Grid.UpdateGrid( tilesCost );
        }
    }
}