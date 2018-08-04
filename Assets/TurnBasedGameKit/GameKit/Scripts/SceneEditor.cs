using UnityEngine;
using UnityEditor;

namespace TurnBasedGameKit
{
    public enum eTileShape
    {
        Cube = 0,
        SquareSprite,
        Hex,
        HexSprite,
        UnityTilemap
    }

    // 삭제해도 될듯 ?
    public enum eTileSource
    {
        Sprite = 0,
        Isometric,
        Model
    }

    public enum eCameraType
    {
        TwoDimension = 0,
        ThreeDimension
    }

    public enum eCameraProjection
    {
        Perpective = 0,
        Orthographic
    }

    public class SceneEditor : MonoBehaviour
    {
        [Tooltip( "Camera type to view" )]
        public eCameraType m_CameraType;

        [Tooltip( "Camera projection to view")]
        public eCameraProjection m_CameraProjection;

        [Tooltip( "Shape of tile" )]
        public eTileShape m_TileShape;

        [Tooltip( "View of tile" )]
        public eTileSource m_TileSource;

        [Tooltip( "Path find of distance type" )]
        public ePathFindDistanceType m_PathFindDistanceType;

        [Tooltip( "Display fog of war" )]
        public bool m_FogOfWar;

        // TODO
        // Button to create Scene
    }
}