using UnityEngine;
using System.Collections;

namespace TurnBasedGameKit.DemoHexTilemap
{
    public static class UnitManager
    {
        public static DemoCubeTilemap.UnitOrc m_Orc;

        public static void Init()
        {
            m_Orc = GameObject.Find( "UnitParent" ).transform.GetComponentInChildren<DemoCubeTilemap.UnitOrc>();
        }

        public static UnitBase GetUnitAt( Vector3Int _coordinate )
        {
            return m_Orc;
        }
    }
}