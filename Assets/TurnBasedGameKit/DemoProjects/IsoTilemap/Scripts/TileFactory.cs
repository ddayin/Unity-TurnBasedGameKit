using UnityEngine;
using System.Collections;

namespace TurnBasedGameKit.DemoIsoTilemap
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
        /// 
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_z"></param>
        /// <returns></returns>
        public static IsoSquareTile CreateIsoSquareTileAt( int _x, int _y, int _z )
        {
            return CreateIsoSquareTileAt( new Vector3Int( _x, _y, _z ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_coordinate"></param>
        /// <returns></returns>
        public static IsoSquareTile CreateIsoSquareTileAt( Vector3Int _coordinate )
        {
            GameObject prefab = TurnBasedGameKit.DemoIsoTilemap.ResourcesLoader.m_IsoSquareSpriteTilePrefab;

            GameObject square = Object.Instantiate( prefab ) as GameObject;
            square.transform.SetParent( m_Parent, false );

            Vector2 converted = Isometric.TwoDToIso( _coordinate.x, _coordinate.y );
            Vector3 pos = new Vector3( converted.x, converted.y, 0f );

            square.transform.localPosition = pos;
            IsoSquareTile tile = square.GetComponent<IsoSquareTile>();
            tile.m_Property.m_Coordinate = _coordinate;
            return tile;
        }
    }
}