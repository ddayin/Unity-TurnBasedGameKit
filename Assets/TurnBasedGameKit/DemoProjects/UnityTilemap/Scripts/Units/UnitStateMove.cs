using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    public class UnitStateMove : UnitState
    {
        public UnitStateMove( UnitStateParameter _param )
            : base( _param )
        {
            m_Command = eUnitCommand.Move;
        }

        public override void Draw()
        {
            m_Parameter.m_SpriteRenderer.color = Color.green;

            m_Parameter.m_Animator.SetBool( "Idle", false );
            m_Parameter.m_Animator.SetBool( "Move", true );
        }

        public override void OnHover()
        {
        }

        public override void OnClicked()
        {
        }
    }
}