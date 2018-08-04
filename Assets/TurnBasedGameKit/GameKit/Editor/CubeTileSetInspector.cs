using UnityEngine;
using UnityEditor;
using System.Collections;
using TurnBasedGameKit.DemoCubeTilemap;

namespace TurnBasedGameKit
{
    [CustomEditor( typeof( TileSetPathFind ) )]
    public class CubeTileSetInspector : Editor
    {
        private TileSetPathFind m_CubeTileSet;

        private void Awake()
        {

        }

        private void OnEnable()
        {
            
        }

        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();

            m_CubeTileSet = (TileSetPathFind) this.target;

            if ( GUILayout.Button( "Generate Tiles" ) == true )
            {
                TurnBasedGameKit.DemoCubeTilemap.TileFactory.Init();
                TurnBasedGameKit.DemoCubeTilemap.ResourcesLoader.LoadAll();

                m_CubeTileSet.Init();
                m_CubeTileSet.CreateTiles();
                m_CubeTileSet.CreateCoordinateTexts();
            }

            if ( GUILayout.Button( "Clear All Tiles" ) == true )
            {
                m_CubeTileSet.ClearAllTiles();
                m_CubeTileSet.ClearAllCoordinates();
            }
                
        }
    }
}