using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TurnBasedGameKit
{
    [CustomPropertyDrawer( typeof( Vector3IntTileBaseDictionary ) )]
    public class TileSerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer
    {

    }
}