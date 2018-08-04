using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace TurnBasedGameKit
{

    /// <summary>
    /// 타일 에셋을 만드는 메뉴
    /// </summary>
    public static class CreateSquareTileAsset
    {
        [MenuItem( "TBGameKit/Unity Tilemap/Create Square Grid Tile Asset" )]
        public static void CreateSquareGridTileAsset()
        {
            CreateAssetAtLocation<SquareTile>( "SquareGrid.asset" );
        }

        [MenuItem( "TBGameKit/Unity Tilemap/Create Square Blank Tile Asset" )]
        public static void CreateBlankTileAsset()
        {
            CreateAssetAtLocation<SquareTile>( "BlankTile.asset" );
        }

        [MenuItem( "TBGameKit/Unity Tilemap/Create Square Movable Tile Asset" )]
        public static void CreateMovableTileAsset()
        {
            CreateAssetAtLocation<SquareTile>( "MovableTile.asset" );
        }

        [MenuItem( "TBGameKit/Unity Tilemap/Create Square Selected Tile Asset" )]
        public static void CreateSelectedTileAsset()
        {
            CreateAssetAtLocation<SquareTile>( "SelectedTile.asset" );
        }

        [MenuItem( "TBGameKit/Unity Tilemap/Create Red Tile Asset" )]
        public static void CreateRedTileAsset()
        {
            CreateAssetAtLocation<SquareTile>( "RedTile.asset" );
        }

        /// <summary>
        /// 해당 이름의 에셋을 생성한다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_name"></param>
        private static void CreateAssetAtLocation<T>( string _name ) where T : ScriptableObject
        {
            Object selectedObject = Selection.activeObject;
            string path = "Assets";

            if ( selectedObject != null )
            {
                string assetPath = AssetDatabase.GetAssetPath( selectedObject.GetInstanceID() );

                if ( !string.IsNullOrEmpty( assetPath ) )
                {
                    if ( Directory.Exists( assetPath ) )
                    {
                        path = assetPath;
                    }
                    else
                    {
                        path = Path.GetDirectoryName( assetPath );
                    }
                }
            }

            AssetDatabase.CreateAsset(
                ScriptableObject.CreateInstance<T>(),
                AssetDatabase.GenerateUniqueAssetPath( Path.Combine( path, _name ) ) );
        }
    }
}