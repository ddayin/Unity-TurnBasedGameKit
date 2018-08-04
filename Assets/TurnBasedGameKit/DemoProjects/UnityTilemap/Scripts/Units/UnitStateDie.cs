using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    public class UnitStateDie : UnitState
    {
        public UnitStateDie( UnitStateParameter _param )
            : base( _param )
        {
            m_Command = eUnitCommand.Die;
        }

        public override void Draw()
        {

        }

        public override void OnHover()
        {

        }

        public override void OnClicked()
        {

        }
    }
}