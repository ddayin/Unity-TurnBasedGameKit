using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    public class UnitStateAttack : UnitState
    {
        public UnitStateAttack( UnitStateParameter _param )
            : base( _param )
        {
            m_Command = eUnitCommand.Attack;
        }

        public override void Draw()
        {
            m_Parameter.m_SpriteRenderer.color = Color.white;
        }

        public override void OnHover()
        {

        }

        public override void OnClicked()
        {

        }
    }
}