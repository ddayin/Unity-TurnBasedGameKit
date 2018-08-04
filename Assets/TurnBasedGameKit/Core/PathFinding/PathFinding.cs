/**
 * Provide simple path-finding algorithm with tile prices support.
 * Based on code and tutorial by Sebastian Lague (https://www.youtube.com/channel/UCmtyQOKKmrMVaKuRXz02jbQ).
 *   
 * Author: Ronen Ness.
 * Since: 2016. 
*/
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit
{
    /// <summary>
    /// Different ways to calculate path distance.
    /// </summary>
    public enum ePathFindDistanceType
    {
        /// <summary>
        /// The "ordinary" straight-line distance between two points.
        /// </summary>
        Euclidean,

        /// <summary>
        /// Distance without diagonals, only horizontal and/or vertical path lines.
        /// </summary>
        Manhattan
    }

    /// <summary>
    /// Main class to find the best path to walk from A to B.
    /// 
    /// Usage example:
    /// Grid grid = new Grid(width, height, tiles_costs);
    /// List<Point> path = Pathfinding.FindPath(grid, from, to);
    /// </summary>
    public class PathFind
    {
        /// <summary>
        /// Find a path between two points.
        /// </summary>
        /// <param name="_grid">Grid to search.</param>
        /// <param name="_startPos">Starting position.</param>
		/// <param name="_targetPos">Ending position.</param>
        /// <param name="_distance">The type of distance, Euclidean or Manhattan.</param>
        /// <param name="_ignorePrices">If true, will ignore tile price (how much it "cost" to walk on).</param>
        /// <returns>List of points that represent the path to walk.</returns>
		public static HashSet<Vector3Int> FindPath( 
            Grid _grid, Vector3Int _startPos, Vector3Int _targetPos, 
            ePathFindDistanceType _distance = ePathFindDistanceType.Euclidean, bool _ignorePrices = false )
        {
            // find path
            List<Node> nodes_path = _ImpFindPath( _grid, _startPos, _targetPos, _distance, _ignorePrices );

            // convert to a list of points and return
            HashSet<Vector3Int> ret = new HashSet<Vector3Int>();
            if ( nodes_path != null )
            {
                foreach ( Node node in nodes_path )
                {
                    ret.Add( node.m_GridPosition );
                }
            }
            return ret;
        }

        /// <summary>
        /// Internal function that implements the path-finding algorithm.
        /// </summary>
        /// <param name="_grid">Grid to search.</param>
        /// <param name="_startPos">Starting position.</param>
        /// <param name="_targetPos">Ending position.</param>
        /// <param name="_distance">The type of distance, Euclidean or Manhattan.</param>
        /// <param name="_ignorePrices">If true, will ignore tile price (how much it "cost" to walk on).</param>
        /// <returns>List of grid nodes that represent the path to walk.</returns>
        private static List<Node> _ImpFindPath( Grid _grid, Vector3Int _startPos, Vector3Int _targetPos, ePathFindDistanceType _distance = ePathFindDistanceType.Euclidean, bool _ignorePrices = false )
        {
            Node startNode = _grid.m_NodesDic[ _startPos ];
            Node targetNode = _grid.m_NodesDic[ _targetPos ];

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add( startNode );

            while ( openSet.Count > 0 )
            {
                Node currentNode = openSet[ 0 ];

                //Debug.Log( "[currentNode] x = " + currentNode.gridX + " y = " + currentNode.gridY );

                for ( int i = 1; i < openSet.Count; i++ )
                {
                    if ( openSet[ i ].fCost < currentNode.fCost || openSet[ i ].fCost == currentNode.fCost && openSet[ i ].m_hCost < currentNode.m_hCost )
                    {
                        currentNode = openSet[ i ];
                    }
                }

                openSet.Remove( currentNode );
                closedSet.Add( currentNode );

                if ( currentNode.m_GridPosition == targetNode.m_GridPosition )
                {
                    return RetracePath( startNode, targetNode );
                }

                foreach ( Node neighbour in _grid.GetNeighbours( currentNode, _distance ) )
                {
                    if ( !neighbour.m_Walkable || closedSet.Contains( neighbour ) )
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.m_gCost + GetDistance( currentNode, neighbour ) * ( _ignorePrices ? 1 : (int) ( 10.0f * neighbour.m_Price ) );
                    if ( newMovementCostToNeighbour < neighbour.m_gCost || !openSet.Contains( neighbour ) )
                    {
                        neighbour.m_gCost = newMovementCostToNeighbour;
                        neighbour.m_hCost = GetDistance( neighbour, targetNode );
                        neighbour.m_Parent = currentNode;

                        if ( !openSet.Contains( neighbour ) )
                        {
                            openSet.Add( neighbour );
                        }   
                    }
                }
            }

            Debug.LogError( "end of _ImpFindPath() return null" );
            return null;
        }

        /// <summary>
        /// Retrace path between two points.
        /// </summary>
        /// <param name="_grid">Grid to work on.</param>
        /// <param name="_startNode">Starting node.</param>
        /// <param name="_endNode">Ending (target) node.</param>
        /// <returns>Retraced path between nodes.</returns>
        private static List<Node> RetracePath( Node _startNode, Node _endNode )
        {
            List<Node> path = new List<Node>();
            Node currentNode = _endNode;

            while ( true )
            {
                if ( currentNode.m_GridPosition == _startNode.m_GridPosition )
                {
                    break;
                }

                path.Add( currentNode );
                currentNode = currentNode.m_Parent;
            }
            path.Reverse();

            return path;
        }

        /// <summary>
        /// Get distance between two nodes.
        /// </summary>
        /// <param name="_nodeA">First node.</param>
        /// <param name="_nodeB">Second node.</param>
        /// <returns>Distance between nodes.</returns>
        private static int GetDistance( Node _nodeA, Node _nodeB )
        {
            int dstX = System.Math.Abs( _nodeA.m_GridPosition.x - _nodeB.m_GridPosition.x );
            int dstY = System.Math.Abs( _nodeA.m_GridPosition.y - _nodeB.m_GridPosition.y );
            return ( dstX > dstY ) ?
                14 * dstY + 10 * ( dstX - dstY ) :
                14 * dstX + 10 * ( dstY - dstX );
        }
    }

}