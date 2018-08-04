using UnityEngine;
using System.Collections;

namespace TurnBasedGameKit.DemoCubeTilemap
{
    public static class UnitFactory
    {
        /// <summary>
        /// create orc at destinated _coordinate
        /// </summary>
        /// <param name="_coordinate"></param>
        /// <returns></returns>
        public static UnitOrc CreateOrc( Vector3Int _coordinate )
        {
            GameObject prefab = Resources.Load( "Prefabs/UnitOrcRoot" ) as GameObject;
            GameObject unitParent = GameObject.Find( "UnitParent" );

            GameObject playerUnit = Lean.LeanPool.Spawn( prefab, Vector3.zero, Quaternion.identity, unitParent.transform );
            UnitOrc orc = playerUnit.GetComponent<UnitOrc>();
            orc.SetPosition( _coordinate, true );

            return orc;
        } 
    }
}