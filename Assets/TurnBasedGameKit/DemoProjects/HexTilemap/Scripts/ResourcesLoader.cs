using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoHexTilemap
{
    public static class ResourcesLoader
    {
        #region Properties
        [HideInInspector]
        public static GameObject m_HexTilePrefab = null;
        public static GameObject m_HexSpriteTilePrefab = null;
        public static GameObject m_CoordinatePrefab = null;
        public static bool m_IsLoadedAll = false;
        #endregion

        public static void LoadAll()
        {
            if ( m_IsLoadedAll == true ) return;

            if ( m_HexTilePrefab == null )
            {
                LoadHexTile();
            }

            if ( m_HexSpriteTilePrefab == null )
            {
                LoadHexSpriteTile();
            }

            if ( m_CoordinatePrefab == null )
            {
                LoadTileCoordinateText();
            }

            m_IsLoadedAll = true;
        }

        public static void LoadHexTile()
        {
            m_HexTilePrefab = Resources.Load( "Prefabs/HexTile" ) as GameObject;
        }

        public static void LoadHexSpriteTile()
        {
            m_HexSpriteTilePrefab = Resources.Load( "Prefabs/HexSpriteTile" ) as GameObject;
        }

        public static void LoadTileCoordinateText()
        {
            m_CoordinatePrefab = Resources.Load( "Prefabs/Text_TileCoordinate" ) as GameObject;
        }
    }
}