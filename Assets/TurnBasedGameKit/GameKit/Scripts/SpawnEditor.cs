using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit
{
    public enum eSpawnType
    {
        OnStart = 0,
        OnRunTimeOnce,
        Regeneration
    }

    public enum eSpawnDeployment
    {
        Manual = 0,
        Random
    }


    public class SpawnEditor : MonoBehaviour
    {
        [Tooltip( "Spawn Group ID" )]
        public int m_GroupID = 0;

        [Tooltip( "Spawn type to create units" )]
        public eSpawnType m_SpawnType;

        [Tooltip( "How to deploy units at spawn point" )]
        public eSpawnDeployment m_SpawnDeployment;

        [Tooltip( "Direction to spawn new units" )]
        public eDirection m_SpawnDirection;


    }
}