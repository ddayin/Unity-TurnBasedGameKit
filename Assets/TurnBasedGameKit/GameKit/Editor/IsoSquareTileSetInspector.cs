using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TurnBasedGameKit.DemoIsoTilemap;

namespace TurnBasedGameKit
{
    [CustomEditor( typeof( TileSetIsoSquare ) )]
    public class IsoSquareTileSetInspector : Editor
    {
        #region Properties
        private TileSetIsoSquare m_TileSet;
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

            m_TileSet = (TileSetIsoSquare) this.target;

            if ( GUILayout.Button( "Generate Tiles" ) == true )
            {
                TileFactory.Init();
                TurnBasedGameKit.DemoIsoTilemap.ResourcesLoader.LoadAll();

                m_TileSet.CreateTiles();
            }

            if ( GUILayout.Button( "Clear All Tiles" ) == true )
            {
                m_TileSet.ClearAllTiles();
            }
        }
    }
}