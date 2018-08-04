using UnityEngine;
using System.Collections;

namespace TurnBasedGameKit
{
    /// <summary>
    /// type of tile
    /// </summary>
    public enum eTileType
    {
        Movable = 0,    // 
        Obstacle,       // 
        Breakable       // 
    }

    /// <summary>
    /// properties of tile
    /// </summary>
    [System.Serializable]
    public class TileProperties
    {
        #region Properties
        public Vector3Int m_Coordinate  = Vector3Int.zero;
        public eTileType m_Type         = eTileType.Movable;
        public float m_Distance         = 0;
        public bool m_IsMovable         = false;
        public bool m_IsPath            = false;
        public bool m_IsAttackable      = false;
<<<<<<< HEAD
=======
        public Color m_Color            = TileTypeColor.Normal;
>>>>>>> refactor
        #endregion

        /// <summary>
        /// initialize
        /// </summary>
        public void Init()
        {
            // coordinate don't need to be reinitialized
            //m_Coordinate  = Vector3Int.zero;

            // type can not be change on run time
<<<<<<< HEAD
            //m_Type = eTileType.Movable;
            m_Distance = 0;
            m_IsMovable = false;
            m_IsPath = false;
            m_IsAttackable = false;
=======
            //m_Type        = eTileType.Movable;
            m_Distance      = 0;
            m_IsMovable     = false;
            m_IsPath        = false;
            m_IsAttackable  = false;
            m_Color         = TileTypeColor.Normal;
>>>>>>> refactor
        }
    }

    /// <summary>
    /// color of tile
    /// </summary>
    public static class TileTypeColor
    {
        public static readonly Color Normal     = Color.white;        
        public static readonly Color Over       = Color.green;
        public static readonly Color Movable    = Color.blue;
        public static readonly Color Path       = Color.yellow;
        public static readonly Color Attackable = Color.red;
        public static readonly Color Obstacle   = Color.cyan;
        public static readonly Color Breakable  = Color.gray;
    }

    /// <summary>
    /// base class of tile
    /// </summary>
    public class TileBase : MonoBehaviour
    {
        #region Properties
        public UnitBase m_Unit = null;
        public TileProperties m_Property = new TileProperties();
        #endregion

        #region MonoBehaviour
        protected virtual void Awake()
        {
            
        }
        #endregion

        /// <summary>
        /// initialize properties of class
        /// </summary>
        public virtual void Init()
        {
            m_Unit = null;
            m_Property = null;
            m_Property = new TileProperties();
            m_Property.Init();
        }

        /// <summary>
        /// set color of tile
        /// </summary>
        /// <param name="_type">tile type</param>
        public virtual void SetColor( eTileType _type )
        {
            m_Property.m_Type = _type;
        }
    }
}