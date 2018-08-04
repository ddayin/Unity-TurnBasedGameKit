using UnityEngine;
using System.Collections;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    public class UnitStateReadyToAttack : UnitState
    {
        public UnitStateReadyToAttack( UnitStateParameter _param )
            : base( _param )
        {
            m_Command = eUnitCommand.ReadyToAttack;
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