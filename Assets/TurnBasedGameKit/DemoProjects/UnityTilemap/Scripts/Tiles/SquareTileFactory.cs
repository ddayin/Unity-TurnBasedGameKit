using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 
    /// </summary>
    public static class SquareTileFactory
    {
        public static SquareTile CreateAttackable( Vector3Int _position )
        {
            SquareTile tile = ScriptableObject.CreateInstance<SquareTile>();
            tile.sprite = TurnBasedGameKit.DemoUnityTilemap.ResourcesLoader.m_AttackableSprite;
            Color color = Color.white;
            color.a = 0.5f;
            tile.color = color;
            tile.m_Position = _position;
            tile.m_Cost = 0;
            tile.m_Distance = 0;
            tile.m_State = new TileStateAttackable( _position );
            tile.m_State.m_Tile = tile;            
            tile.m_Unit = UnitManager.GetUnitAt( _position );

            return tile;
        }

        public static SquareTile CreateGrid( Vector3Int _position )
        {
            SquareTile tile = ScriptableObject.CreateInstance<SquareTile>();
            tile.sprite = TurnBasedGameKit.DemoUnityTilemap.ResourcesLoader.m_GridSprite;
            tile.m_Position = _position;
            tile.m_Cost = 0;
            tile.m_Distance = 0;
            tile.m_State = new TileStateOutline( _position );
            tile.m_State.m_Tile = tile;            
            tile.m_Unit = UnitManager.GetUnitAt( _position );

            return tile;
        }

        public static SquareTile CreateMovable( Vector3Int _position )
        {
            SquareTile tile = ScriptableObject.CreateInstance<SquareTile>();
            tile.sprite = TurnBasedGameKit.DemoUnityTilemap.ResourcesLoader.m_MovableSprite;
            Color color = Color.white;
            color.a = 0.5f;
            tile.color = color;
            tile.m_Position = _position;
            tile.m_Cost = 0;
            tile.m_Distance = 0;
            tile.m_State = new TileStateMovable( _position );
            tile.m_State.m_Tile = tile;
            tile.m_State.m_State = eTileType.Movable;
            tile.m_Unit = UnitManager.GetUnitAt( _position );

            return tile;
        }

        public static SquareTile CreateNormal( Vector3Int _position )
        {
            SquareTile tile = ScriptableObject.CreateInstance<SquareTile>();
            tile.sprite = TurnBasedGameKit.DemoUnityTilemap.ResourcesLoader.m_NormalSprite;
            Color color = Color.white;
            color.a = 0.5f;
            tile.color = color;
            tile.m_Position = _position;
            tile.m_Cost = 0;
            tile.m_Distance = 0;
            tile.m_State = new TileStateNormal( _position );
            tile.m_State.m_Tile = tile;            
            tile.m_Unit = UnitManager.GetUnitAt( _position );

            return tile;
        }

        public static SquareTile CreateObstacle( Vector3Int _position )
        {
            SquareTile tile = ScriptableObject.CreateInstance<SquareTile>();
            tile.sprite = TurnBasedGameKit.DemoUnityTilemap.ResourcesLoader.m_NormalSprite;
            Color color = Color.white;
            tile.color = color;
            tile.m_Position = _position;
            tile.m_Cost = 0;
            tile.m_Distance = 0;
            tile.m_State = new TileStateObstacle( _position );
            tile.m_State.m_Tile = tile;
            tile.m_State.m_State = eTileType.Obstacle;
            tile.m_Unit = UnitManager.GetUnitAt( _position );

            return tile;
        }

        public static SquareTile CreatePath( Vector3Int _position )
        {
            SquareTile tile = ScriptableObject.CreateInstance<SquareTile>();
            tile.sprite = TurnBasedGameKit.DemoUnityTilemap.ResourcesLoader.m_PathSprite;
            Color color = Color.white;
            color.a = 0.5f;
            tile.color = color;
            tile.m_Position = _position;
            tile.m_Cost = 0;
            tile.m_Distance = 0;
            tile.m_State = new TileStatePath( _position );
            tile.m_State.m_Tile = tile;            
            tile.m_Unit = UnitManager.GetUnitAt( _position );

            return tile;
        }

        public static SquareTile CreateSelected( Vector3Int _position )
        {
            SquareTile tile = ScriptableObject.CreateInstance<SquareTile>();
            tile.sprite = TurnBasedGameKit.DemoUnityTilemap.ResourcesLoader.m_SelectedSprite;
            Color color = Color.white;
            color.a = 0.5f;
            tile.color = color;
            tile.m_Position = _position;
            tile.m_Cost = 0;
            tile.m_Distance = 0;
            tile.m_State = new TileStateSelected( _position );
            tile.m_State.m_Tile = tile;            
            tile.m_Unit = UnitManager.GetUnitAt( _position );

            return tile;
        }

        public static SquareTile CreateUnitPlaced( Vector3Int _position )
        {
            SquareTile tile = ScriptableObject.CreateInstance<SquareTile>();
            tile.sprite = TurnBasedGameKit.DemoUnityTilemap.ResourcesLoader.m_UnitPlacedSprite;
            Color color = Color.white;
            color.a = 0.5f;
            tile.color = color;
            tile.m_Position = _position;
            tile.m_Cost = 0;
            tile.m_Distance = 0;
            tile.m_State = new TileStateUnitPlaced( _position );
            tile.m_State.m_Tile = tile;            
            tile.m_Unit = UnitManager.GetUnitAt( _position );

            return tile;
        }
    }
}