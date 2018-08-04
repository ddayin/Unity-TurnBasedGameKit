/**
 * Represent a grid of nodes we can search paths on.
 * Based on code and tutorial by Sebastian Lague (https://www.youtube.com/channel/UCmtyQOKKmrMVaKuRXz02jbQ).
 *   
 * Author: Ronen Ness.
 * Since: 2016. 
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TurnBasedGameKit
{
    /// <summary>
    /// A 2D grid of nodes we use to find path.
    /// The grid mark which tiles are walkable and which are not.
    /// </summary>
    public class Grid
    {
        #region Properties
        // nodes in grid
        public Dictionary<Vector3Int, Node> m_NodesDic;

        // grid size
        private Vector2Int m_GridSize = Vector2Int.zero;
        
        private BoundsInt m_Bounds;

        private List<Node> m_Neighbours = new List<Node>();
        #endregion

        /// <summary>
        /// Create a new grid with tile prices.
        /// </summary>
        /// <param name="tiles_costs">A 2d array of tile prices.
        ///     0.0f = Unwalkable tile.
        ///     1.0f = Normal tile.
        ///     > 1.0f = costy tile.
        ///     < 1.0f = cheap tile.
        /// </param>
        public Grid( BoundsInt _bounds, Dictionary<Vector3Int, float> _tilesCost )
        {
            m_Bounds = _bounds;
            // create nodes
            m_GridSize.x = m_Bounds.size.x;
            m_GridSize.y = m_Bounds.size.y;

            m_NodesDic = new Dictionary<Vector3Int, Node>();

            // init nodes
            for ( int x = m_Bounds.xMin; x < m_Bounds.xMax; x++ )
            {
                for ( int y = m_Bounds.yMin; y < m_Bounds.yMax; y++ )
                {
                    Vector3Int pos = new Vector3Int( x, y, 0 );

                    m_NodesDic.Add( pos, new Node( _tilesCost[ pos ], pos ) );
                }
            }
        }

        /// <summary>
        /// Updates the already created grid with new tile prices.
        /// </summary>        
        /// <param name="_tilesCosts">Tiles costs.</param>
        public void UpdateGrid( Dictionary<Vector3Int, float> _tilesCosts )
        {
            if ( _tilesCosts.Count == 0 ) return;
            
            foreach ( KeyValuePair<Vector3Int, Node> node in m_NodesDic )
            {
                foreach ( KeyValuePair<Vector3Int, float> cost in _tilesCosts )
                {
                    if ( node.Key == cost.Key )
                    {
                        node.Value.Update( _tilesCosts[ node.Key ], node.Key );
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_position"></param>
        /// <param name="_cost"></param>
        /// <returns></returns>
        public bool UpdateTile( Vector3Int _position, float _cost )
        {
            foreach ( KeyValuePair<Vector3Int, Node> node in m_NodesDic )
            {
                if ( node.Key == _position )
                {
                    node.Value.Update( _cost, _position );
                    return true;
                }
            }

            Debug.LogError( "tile not found _position = " + _position );
            return false;
        }

        /// <summary>
        /// Get all the neighbors of a given tile in the grid.
        /// </summary>
        /// <param name="_node">Node to get neighbors for.</param>
        /// <returns>List of node neighbors.</returns>
        public List<Node> GetNeighbours( Node _node, ePathFindDistanceType _distanceType )
        {
            if ( m_Neighbours.Count > 0 )
            {
                m_Neighbours.Clear();
                m_Neighbours = null;
            }

            m_Neighbours = new List<Node>();

            int x = 0, y = 0;

            switch ( _distanceType )
            {
                case ePathFindDistanceType.Manhattan:
                    y = 0;
                    for ( x = -1; x <= 1; ++x )
                    {
                        Vector3Int position = new Vector3Int( x, y, 0 );
                        AddNodeNeighbour( position, _node );
                    }

                    x = 0;
                    for ( y = -1; y <= 1; ++y )
                    {
                        Vector3Int position = new Vector3Int( x, y, 0 );
                        AddNodeNeighbour( position, _node );
                    }
                    break;

                case ePathFindDistanceType.Euclidean:
                    for ( x = -1; x <= 1; x++ )
                    {
                        for ( y = -1; y <= 1; y++ )
                        {
                            Vector3Int position = new Vector3Int( x, y, 0 );
                            AddNodeNeighbour( position, _node );
                        }
                    }
                    break;

                default:
                    break;
            }

            return m_Neighbours;
        }

        /// <summary>
        /// Adds the node neighbour.
        /// </summary>
        /// <returns><c>true</c>, if node neighbour was added, <c>false</c> otherwise.</returns>
        /// <param name="_position">Position</param>
        /// <param name="_node">Node.</param>        
        private bool AddNodeNeighbour( Vector3Int _position, Node _node )
        {
            if ( _position == Vector3Int.zero ) return false;

            Vector3Int check = _node.m_GridPosition + _position;

            if ( check.x < m_GridSize.x && check.y < m_GridSize.y )
            {
                if ( m_NodesDic.ContainsKey( check ) == false )
                {
                    //Debug.LogError( "check = " + check + " does not exist" );
                }
                else
                {
                    m_Neighbours.Add( m_NodesDic[ check ] );
                }
                return true;
            }

            return false;
        }
    }
}