using UnityEngine;
using UnityEditor;
using System.Collections;
using TurnBasedGameKit.DemoHexTilemap;

namespace TurnBasedGameKit
{
    [CustomEditor( typeof( TileSetHexSprite ) )]
    public class HexSpriteTileSetInspector : Editor
    {
        private TileSetHexSprite m_HexSpriteTileSet;

        private void Awake()
        {

        }

        private void OnEnable()
        {

        }

        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();

            m_HexSpriteTileSet = (TileSetHexSprite) this.target;

            if ( GUILayout.Button( "Generate Tiles" ) == true )
            {
                TileFactory.Init();
                TurnBasedGameKit.DemoHexTilemap.ResourcesLoader.LoadAll();

                m_HexSpriteTileSet.CreateTiles();
            }

            if ( GUILayout.Button( "Clear All Tiles" ) == true )
            {
                m_HexSpriteTileSet.ClearAllTiles();
            }

        }
    }
}