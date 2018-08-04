using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoCubeTilemap
{
    /// <summary>
    /// 
    /// </summary>
    public static class TileFactory
    {
        public static Transform m_Parent;

        /// <summary>
        /// initialize
        /// </summary>
        public static void Init()
        {
            m_Parent = GameObject.Find( "TileSet" ).transform;
        }

        /// <summary>
        /// create a cube tile at _position
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_z"></param>
        /// <returns></returns>
        public static TileCube CreateCubeTileAt( int _x, int _y, int _z )
        {
            Vector3Int position = new Vector3Int( _x, _y, _z );
            return CreateCubeTileAt( position );
        }

        /// <summary>
        /// create a cube tile at _position
        /// </summary>
        /// <param name="_coordinate"></param>
        /// <returns></returns>
        public static TileCube CreateCubeTileAt( Vector3Int _coordinate )
        {
            GameObject prefab = TurnBasedGameKit.DemoCubeTilemap.ResourcesLoader.m_CubeTilePrefab;

            GameObject cubeObject = Object.Instantiate( prefab, m_Parent ) as GameObject;
            cubeObject.name = cubeObject.name.Replace( "(Clone)", "" );
            cubeObject.name += _coordinate.ToString();
            cubeObject.transform.position = new Vector3( _coordinate.x, 0f, _coordinate.y );
            TileCube cubeTile = cubeObject.GetComponent<TileCube>();
            cubeTile.m_Property.m_Coordinate = _coordinate;
            cubeTile.Init();

            return cubeTile;
        }
        
        public static IsoHexSpriteTile IsoHexSpriteTileAt( Vector3Int _coordinate )
        {
            GameObject prefab = TurnBasedGameKit.DemoIsoTilemap.ResourcesLoader.m_IsoSquareSpriteTilePrefab;

            GameObject hex = Object.Instantiate( prefab ) as GameObject;
            hex.transform.SetParent( m_Parent, false );

            Vector2 converted = Isometric.TwoDToIso( _coordinate.x, _coordinate.y );
            Vector3 pos = new Vector3( converted.x, converted.y, 0f );

            hex.transform.localPosition = pos;
            IsoHexSpriteTile tile = hex.GetComponent<IsoHexSpriteTile>();
            tile.m_Property.m_Coordinate = _coordinate;
            return tile;
        }
    }
}