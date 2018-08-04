/**
 * Represent a single node in the pathfinding grid.
 * Based on code and tutorial by Sebastian Lague (https://www.youtube.com/channel/UCmtyQOKKmrMVaKuRXz02jbQ).
 *   
 * Author: Ronen Ness.
 * Since: 2016. 
*/

using UnityEngine;

namespace TurnBasedGameKit
{

    /// <summary>
    /// Represent a single node in the pathfinding grid.
    /// </summary>
    public class Node
    {
        #region Properties
        // is this node walkable?
        public bool m_Walkable;
        public Vector3Int m_GridPosition;
        public float m_Price;

        // calculated values while finding path
        public int m_gCost;
        public int m_hCost;
        public Node m_Parent;
        #endregion

        /// <summary>
        /// Create the grid node.
        /// </summary>
        /// <param name="_price"></param>
        /// <param name="_position"></param>
        public Node( float _price, Vector3Int _position )
        {
            m_Walkable = _price != 0.0f;
            m_Price = _price;
            m_GridPosition = _position;
        }

        /// <summary>
        /// Updates the grid node.
        /// </summary>
        /// <param name="_price"></param>
        /// <param name="_position"></param>
        public void Update( float _price, Vector3Int _position )
        {
            m_Walkable = _price != 0.0f;
            m_Price = _price;
            m_GridPosition = _position;
        }

        /// <summary>
        /// Get fCost of this node.
        /// </summary>
        public int fCost
        {
            get
            {
                return m_gCost + m_hCost;
            }
        }
    }
}