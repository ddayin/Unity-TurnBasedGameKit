using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    public class UnitStateReadyToMove : UnitState
    {
        public UnitStateReadyToMove( UnitStateParameter _param )
            : base( _param )
        {
            m_Command = eUnitCommand.ReadyToMove;
        }

        public override void Draw()
        {
            if ( m_Parameter.m_Unit.m_Type != eCampType.Player )
            {
                return;
            }
            m_Parameter.m_SpriteRenderer.color = Color.green;
        }

        public override void OnHover()
        {
        }

        public override void OnClicked()
        {
        }
    }
}