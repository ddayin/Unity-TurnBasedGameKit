using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using WanzyeeStudio;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 리소스를 불러들이는 클래스
    /// </summary>
    public static class ResourcesLoader
    {
        #region Properties
        // 타일
        [HideInInspector]
        public static SquareTile m_NormalTile;

        [HideInInspector]
        public static SquareTile m_SelectedTile;

        [HideInInspector]
        public static SquareTile m_MovableTile;

        [HideInInspector]
        public static SquareTile m_AttackableTile;

        [HideInInspector]
        public static SquareTile m_UnitPlacedTile;

        [HideInInspector]
        public static SquareTile m_PathTile;

        // 스프라이트
        [HideInInspector]
        public static Sprite m_NormalSprite;

        [HideInInspector]
        public static Sprite m_GridSprite;

        [HideInInspector]
        public static Sprite m_SelectedSprite;

        [HideInInspector]
        public static Sprite m_MovableSprite;

        [HideInInspector]
        public static Sprite m_AttackableSprite;

        [HideInInspector]
        public static Sprite m_UnitPlacedSprite;

        [HideInInspector]
        public static Sprite m_PathSprite;
        #endregion

        public static void Init()
        {
            LoadTiles();

            LoadSprites();
        }

        /// <summary>
        /// 타일 에셋을 불러들인다.
        /// </summary>
        private static void LoadTiles()
        {
            m_NormalTile = Resources.Load<SquareTile>( "Palettes/Tiles/BlankTile 1" );
            m_SelectedTile = Resources.Load<SquareTile>( "Palettes/Tiles/SelectedTile 1" );
            m_MovableTile = Resources.Load<SquareTile>( "Palettes/Tiles/MovableTile 1" );
            m_AttackableTile = Resources.Load<SquareTile>( "Palettes/Tiles/AttackableTile 1" );
            m_UnitPlacedTile = Resources.Load<SquareTile>( "Palettes/Tiles/BlankTile 1" );
        }

        /// <summary>
        /// 타일 스프라이트를 불러들인다.
        /// </summary>
        private static void LoadSprites()
        {
            m_NormalSprite = Resources.Load<Sprite>( "Textures/BlankTile" );
            m_GridSprite = Resources.Load<Sprite>( "Textures/GridTile" );
            m_SelectedSprite = Resources.Load<Sprite>( "Textures/SelectedTile" );
            m_MovableSprite = Resources.Load<Sprite>( "Textures/MovableTile" );
            m_AttackableSprite = Resources.Load<Sprite>( "Textures/AttackableTile" );
            m_UnitPlacedSprite = Resources.Load<Sprite>( "Textures/BlankTile" );
            m_PathSprite = Resources.Load<Sprite>( "Textures/PathTile" );
        }
    }
}