using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TurnBasedGameKit.DemoHexTilemap;

namespace TurnBasedGameKit
{
    [CustomEditor( typeof( TileSetHex ) )]
    public class HexTileSetInspector : Editor
    {
        #region Properties
        private TileSetHex m_HexTileSet;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {

        }

        private void OnEnable()
        {

        }
        #endregion

        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();

            m_HexTileSet = (TileSetHex) this.target;

            if ( GUILayout.Button( "Generate Tiles" ) == true )
            {
                DemoHexTilemap.TileFactory.Init();
                DemoHexTilemap.ResourcesLoader.LoadAll();

                m_HexTileSet.Init();
                m_HexTileSet.CreateTiles();
                m_HexTileSet.CreateCoordinateTexts();
            }

            if ( GUILayout.Button( "Clear All Tiles" ) == true )
            {
                m_HexTileSet.ClearAllTiles();
                m_HexTileSet.ClearAllCoordinates();
            }
        }
    }
}