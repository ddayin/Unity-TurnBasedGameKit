using UnityEngine;
using System.Collections;

namespace TurnBasedGameKit.DemoHexTilemap
{
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
        /// create a hexagon tile at position
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_z"></param>
        /// <returns></returns>
        public static TileBase CreateHexTileAt( int _x, int _y, int _z )
        {
            Vector3Int coordinate = new Vector3Int( _x, _y, _z );
            return CreateHexTileAt( coordinate );
        }

        /// <summary>
        /// create a hexagon tile at _position
        /// </summary>
        /// <param name="_position"></param>
        /// <returns></returns>
        public static TileBase CreateHexTileAt( Vector3Int _coordinate )
        {
            GameObject prefab = ResourcesLoader.m_HexTilePrefab;

            GameObject cubeObject = Object.Instantiate( prefab ) as GameObject;
            cubeObject.transform.SetParent( m_Parent, false );

            Vector3 position = HexCoordinates.FromCoordinates3D( _coordinate );
            cubeObject.transform.localPosition = position;
            TileHex hexagonTile = cubeObject.GetComponent<TileHex>();
            hexagonTile.m_Property.m_Coordinate = _coordinate;
            return hexagonTile;
        }

        /// <summary>
        /// create a hexagon sprite tile at position
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_z"></param>
        /// <returns></returns>
        public static TileBase CreateHexSpriteTileAt( int _x, int _y, int _z )
        {
            Vector3Int coordinate = new Vector3Int( _x, _y, _z );
            return CreateHexSpriteTileAt( coordinate );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_coordinate"></param>
        /// <returns></returns>
        public static TileBase CreateHexSpriteTileAt( Vector3Int _coordinate )
        {
            GameObject prefab = TurnBasedGameKit.DemoHexTilemap.ResourcesLoader.m_HexSpriteTilePrefab;

            GameObject hexagon = Object.Instantiate( prefab ) as GameObject;
            hexagon.transform.SetParent( m_Parent, false );

            Vector3 position = HexCoordinates.FromCoordinates2D( _coordinate );
            hexagon.transform.localPosition = position;
            HexSpriteTile tile = hexagon.GetComponent<HexSpriteTile>();
            tile.m_Property.m_Coordinate = _coordinate;
            return tile;
        }
    }
}