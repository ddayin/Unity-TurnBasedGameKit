using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using TurnBasedGameKit;

namespace TurnBasedGameKit.DemoUnityTilemap
{
    /// <summary>
    /// 캐릭터
    /// </summary>
    public class UnitKnight : Unit
    {
        #region Properties        

        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

        }

        protected override void OnEnable()
        {
            base.OnEnable();

            m_Type = eCampType.Player;

            // 시작은 Idle로 시작        
            m_Command = eUnitCommand.Idle;

            m_Direction = eDirection.East;

            m_State = null;
            UnitStateParameter param = new UnitStateParameter();
            param.m_Unit = this;
            param.m_Animator = m_Animator;
            param.m_SpriteRenderer = m_SpriteRenderer;

            m_State = new UnitStateIdle( param );
            m_State.Draw();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            m_Command = eUnitCommand.Die;

            m_State = null;
        }

        protected override void Start()
        {
            base.Start();
        }


        private void OnMouseEnter()
        {

        }

        private void OnMouseOver()
        {
            // 유닛의 정보를 표시한다.
            TurnBasedGameKit.DemoUnityTilemap.PopupManager.instance.SetUnitInfo( true );
        }

        private void OnMouseExit()
        {
            // 유닛의 정보를 표시하지 않는다.
            TurnBasedGameKit.DemoUnityTilemap.PopupManager.instance.SetUnitInfo( false );
        }

        private void OnMouseDown()
        {

        }

        private void OnMouseUp()
        {
            if ( m_State != null )
            {
                m_State.OnClicked();
            }
        }
        #endregion

        /// <summary>
        /// 이동
        /// </summary>
        /// <param name="_targetPosition">이동할 지점</param>
        public override void Move( Vector3Int _targetPosition )
        {
            base.Move( _targetPosition );

            m_Command = eUnitCommand.Move;

            // 이동 애니메이션을 보여준다.
            m_State = null;
            UnitStateParameter param = new UnitStateParameter();
            param.m_Unit = this;
            param.m_SpriteRenderer = m_SpriteRenderer;
            param.m_Animator = m_Animator;

            m_State = new UnitStateMove( param );
            m_State.Draw();


            HashSet<Vector3Int> path = Map.instance.m_TilemapPathFind.GetPath();

            // 이동할 방향 설정하고 왼쪽으로 이동할 경우, 스프라이트를 뒤집는다.
            Vector3Int firstPoint = path.First();
            Vector3Int lastPoint = path.Last();
            if ( lastPoint.x - firstPoint.x < 0 )
            {
                m_Direction = eDirection.West;
                m_SpriteRenderer.flipX = true;
            }
            else
            {
                m_Direction = eDirection.East;
                m_SpriteRenderer.flipX = false;
            }

            // 실제로 목표 지점까지 이동
            MoveByPathTile( path, _targetPosition );
        }

        public override void Attack( Vector3Int _targetPosition )
        {
            base.Attack( _targetPosition );


        }



        /// <summary>
        /// 이동 경로를 따라 타일 단위로 이동
        /// </summary>
        /// <param name="_path"></param>
        private void MoveByPathTile( HashSet<Vector3Int> _path, Vector3Int _targetPosition )
        {
            if ( _path.Count == 0 )
            {
                Debug.LogError( "_path.Count == 0" );
                return;
            }

            StartCoroutine( MoveCoroutine( _path, _targetPosition ) );
        }

        /// <summary>
        /// 유닛을 실제로 이동 시키고 각종 초기화를 수행한다.
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        IEnumerator MoveCoroutine( HashSet<Vector3Int> _path, Vector3Int _targetPosition )
        {
            SquareTile item = Map.instance.m_TilemapHighlight.m_Tilemap.GetTile<SquareTile>( m_Position );
            item.m_State = null;
            item.m_State = new TileStateNormal( m_Position );
            SquareTile normalTile = SquareTileFactory.CreateNormal( m_Position );

            Map.instance.m_TilemapHighlight.m_Tilemap.SetTile( normalTile.m_Position, normalTile );

            foreach ( Vector3Int point in _path )
            {
                Vector3 pos = m_SetCenterPosition.GetWorldPosition( point );
                transform.DOMove( pos, 1f );
                yield return new WaitForSeconds( 1f );

                if ( point == _targetPosition ) break;
            }

            // 초기화
            SetToIdle();

            SetPosition( _targetPosition, true );

            InitTiles();
        }

        private void InitTiles()
        {
            SquareTile tile = ScriptableObject.CreateInstance<SquareTile>();
            tile.m_Position = m_Position;
            tile.m_State = new TileStateNormal( tile.m_Position );
            m_CurrentTile = SquareTileFactory.CreateNormal( tile.m_Position );
            Map.instance.m_TilemapHighlight.m_Tilemap.SetTile( m_Position, m_CurrentTile );

            Map.instance.m_TilemapController.ResetAllTiles();
            Map.instance.m_TilemapController.SetAllUnitPlacedTiles();
        }
    }
}