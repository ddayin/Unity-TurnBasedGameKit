using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 유닛을 생성하는 팩토리 클래스
    /// </summary>
    public static class UnitFactory
    {
        /// <summary>
        /// 플레이어가 조작 가능한 유닛들을 생성한다.
        /// </summary>
        public static UnitKnight CreatePlayerUnits( Vector3Int _position )
        {
            GameObject prefab = Resources.Load( "Prefabs/Knight" ) as GameObject;
            GameObject unitParent = GameObject.Find( "UnitParent" );

            GameObject playerUnit = Lean.LeanPool.Spawn( prefab, Vector3.zero, Quaternion.identity, unitParent.transform );
            UnitKnight knight = playerUnit.GetComponent<UnitKnight>();
            knight.SetPosition( _position, true );

            return knight;
        }

        /// <summary>
        /// 플레이어와 같은 편의 동맹 유닛들을 생성한다.
        /// </summary>
        public static UnitSpearman CreateAllies( Vector3Int _position )
        {
            GameObject prefab = Resources.Load( "Prefabs/UnitSpearman" ) as GameObject;
            GameObject unitParent = GameObject.Find( "UnitParent" );

            GameObject allies = Lean.LeanPool.Spawn( prefab, Vector3.zero, Quaternion.identity, unitParent.transform );
            UnitSpearman spearman = allies.GetComponent<UnitSpearman>();
            spearman.SetPosition( _position, true );

            return spearman;
        }

        /// <summary>
        /// 적 유닛들을 생성한다.
        /// </summary>
        public static UnitOrc CreateEnemies( Vector3Int _position )
        {
            GameObject prefab = Resources.Load( "Prefabs/UnitOrc" ) as GameObject;
            GameObject unitParent = GameObject.Find( "UnitParent" );

            GameObject playerUnit = Lean.LeanPool.Spawn( prefab, Vector3.zero, Quaternion.identity, unitParent.transform );
            UnitOrc orc = playerUnit.GetComponent<UnitOrc>();
            orc.SetPosition( _position, true );

            return orc;
        }
        
        
    }
}