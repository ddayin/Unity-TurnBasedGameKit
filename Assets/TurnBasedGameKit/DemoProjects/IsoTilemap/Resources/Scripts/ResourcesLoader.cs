using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoIsoTilemap
{
    public class ResourcesLoader : MonoBehaviour
    {
        #region Properties
        [HideInInspector]
        public static GameObject m_IsoSquareTilePrefab;
        public static GameObject m_IsoSquareSpriteTilePrefab;
        public static GameObject m_IsoHexSpriteTilePrefab;
        public static bool m_IsLoadedAll = false;
        #endregion

        public static void LoadAll()
        {
            if ( m_IsLoadedAll == true ) return;

            //LoadIsoSquareTile();

            LoadIsoSquareSpriteTile();

            m_IsLoadedAll = true;
        }

        public static void LoadIsoSquareTile()
        {
            m_IsoSquareTilePrefab = Resources.Load( "Prefabs/IsoSquareTile" ) as GameObject;
        }

        public static void LoadIsoSquareSpriteTile()
        {
            m_IsoSquareSpriteTilePrefab = Resources.Load( "Prefabs/IsoSquareSpriteTile" ) as GameObject;
        }

        public static void LoadIsoHexSpriteTile()
        {
            m_IsoHexSpriteTilePrefab = Resources.Load( "Prefabs/IsoHexSpriteTile" ) as GameObject;
        }
    }
}