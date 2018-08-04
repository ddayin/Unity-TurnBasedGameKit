using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TurnBasedGameKit.DemoCubeTilemap
{
    public static class ResourcesLoader
    {
        #region Properties
        [HideInInspector]
        public static bool m_IsLoadedAll = false;

        public static GameObject m_CubeTilePrefab = null;
        public static GameObject m_CoordinatePrefab = null;
        #endregion

        public static void LoadAll()
        {
            if ( m_CubeTilePrefab == null )
            {
                LoadCubeTile();
            }
            if ( m_CoordinatePrefab == null )
            {
                LoadTileCoordinateText();
            }

            m_IsLoadedAll = true;
        }

        public static void LoadCubeTile()
        {
            m_CubeTilePrefab = Resources.Load( "Prefabs/CubeTile" ) as GameObject;
        }

        public static void LoadTileCoordinateText()
        {
            m_CoordinatePrefab = Resources.Load( "Prefabs/Text_TileCoordinate" ) as GameObject;            
        }
    }
}