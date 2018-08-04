using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace TurnBasedGameKit
{
    /// <summary>
    /// 
    /// </summary>
    public static class CreateScene
    {
        [MenuItem( "TBGameKit/Scene/3D Cube Tilemap Scene" )]
        public static void CreateCubeTileSetScene()
        {
            // ===============================================
            // camera setting
            if ( GameObject.Find( "Main Camera") == null || Camera.main == null )
            {
                GameObject cameraObj = new GameObject();
                cameraObj.name = "Main Camera";
                cameraObj.AddComponent<Camera>();
                cameraObj.AddComponent<AudioListener>();
                cameraObj.AddComponent<Camera3DController>();

                Camera cam = cameraObj.GetComponent<Camera>();
                cam.orthographic = false;
            }
            // ===============================================

            // 
            GameObject tileSetObj = new GameObject();
            tileSetObj.name = "TileSet";
            tileSetObj.AddComponent<DemoCubeTilemap.TileSetPathFind>();

            DemoCubeTilemap.TileSetPathFind tileset = tileSetObj.GetComponent<DemoCubeTilemap.TileSetPathFind>();
            tileset.m_TileShape = eTileShape.Cube;


            // =====================================================================
            GameObject rootObj = new GameObject();
            rootObj.name = "TBGameKit_Editors";

            AddCubeTileSetSceneScript<SceneEditor>( rootObj.transform, "SceneEditor" );

            AddCubeTileSetSceneScript<GameEditor>( rootObj.transform, "GameEditor" );

            AddCubeTileSetSceneScript<TileMapEditor>( rootObj.transform, "TileMapEditor" );

            AddCubeTileSetSceneScript<TileEditor>( rootObj.transform, "TileEditor" );

            AddCubeTileSetSceneScript<CampEditor>( rootObj.transform, "CampEditor" );

            AddCubeTileSetSceneScript<SpawnEditor>( rootObj.transform, "SpawnEditor" );

            AddCubeTileSetSceneScript<UnitEditor>( rootObj.transform, "UnitEditor" );

            AddCubeTileSetSceneScript<UnitStatEditor>( rootObj.transform, "UnitStatEditor" );

            AddCubeTileSetSceneScript<AIEditor>( rootObj.transform, "AIEditor" );
        }

        public static void AddCubeTileSetSceneScript<T>( Transform parent, string name ) where T : UnityEngine.Component
        {
            GameObject go = new GameObject();
            go.name = name;
            go.AddComponent<T>();
            go.transform.SetParent( parent );
        }
    }
}