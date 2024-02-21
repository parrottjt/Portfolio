using System;
using System.Collections.Generic;
using FrikinCore.Interfaces;
using Sirenix.OdinInspector;
using TSCore.Time;
using TSCore.Utils;
using UnityEngine;

namespace FrikinCore.AI.Enemy.Movement
{
    public abstract class MovementEnemyAbstract : MonoBehaviour, IPause, IMove
    {
        [Flags]
        protected enum MoveDirections
        {
            Up = 1,
            UpRight = 2,
            Right = 4,
            DownRight = 8,
            Down = 16,
            DownLeft = 32,
            Left = 64,
            UpLeft = 128
        }

        protected enum MovementState
        {
            Idle,
            Movement,
            MoveAwayFromWall,
            MoveHome
        }

        protected readonly Dictionary<MoveDirections, Vector2> MoveDirectionVectorDictionary =
            new Dictionary<MoveDirections, Vector2>
            {
                { MoveDirections.Up, Vector2.up },
                { MoveDirections.UpRight, new Vector2(1f, 1f) },
                { MoveDirections.Right, Vector2.right },
                { MoveDirections.DownRight, new Vector2(1f, -1f) },
                { MoveDirections.Down, Vector2.down },
                { MoveDirections.DownLeft, new Vector2(-1f, -1f) },
                { MoveDirections.Left, Vector2.left },
                { MoveDirections.UpLeft, new Vector2(-1f, 1f) },
            };

        [SerializeField] protected DataEnemy dataEnemy;

        [SerializeField] protected float moveSpeed = 5f;
        [SerializeField] protected float alertRange = 50f, wallCheckDistance = 5f;

        [SerializeField] protected int minRandomMoveTick, maxRandomMoveTick = 10;

        public bool dontFlip, facingLeft;

        [ReadOnly] [SerializeField] protected MovementState movementState = MovementState.Idle;

        protected Vector2 moveDirection, closestPoint;

        protected bool runRandom;
        bool _currentCanMoveBeforePause;

        protected int moveTick, randomMoveTickTotal;

        protected float AdjustedMoveSpeed() => moveSpeed * TimeManager.DeltaLayer(TimeLayers.Enemy);

        protected bool PlayerInAlertRange() =>
            Conditions.DistanceBetweenLessThan(dataEnemy.PawnStartPosition,
                dataEnemy.Player.position, alertRange);

        protected Vector2 PawnCurrentPosition()
        {
            Vector3 position = transform.position;
            return new Vector2(position.x, position.y);
        }

        protected RaycastHit2D WallCheck() => Physics2D.Raycast(transform.position, moveDirection.normalized,
            wallCheckDistance, (1 << 19 | 1 << 20 | 1 << 22));

        protected virtual void Awake()
        {
            TickTimeTimer.OnTick += OnTickCall;
            PauseManager.OnPause += OnPause;
            PauseManager.OnUnpause += OnUnpause;

            CanMove = true;
        }

        void OnDestroy()
        {
            TickTimeTimer.OnTick -= OnTickCall;
            PauseManager.OnPause -= OnPause;
            PauseManager.OnUnpause -= OnUnpause;
        }

        protected virtual void Update()
        {
            if (CanMove) Movement();
        }

        public void EnemyFlip()
        {
            var check = transform.position.x - dataEnemy.Player.position.x;

            if (check < 0 && facingLeft || check > 0 && !facingLeft)
            {
                SpriteFlip();
            }
        }

        public void SpriteFlip()
        {
            facingLeft = !facingLeft;
            dataEnemy.spriteControl.SpriteControlPattern();
        }

        protected void MoveAwayFromWallCheck()
        {
            if (WallCheck() == false) return;
            //var outHit = Physics2D.Raycast(transform.position, direction.normalized, wallCheckDistance);
            var outHit = WallCheck();
            closestPoint = PawnCurrentPosition() - outHit.point;
            movementState = MovementState.MoveAwayFromWall;
            moveTick = 0;
        }

        protected virtual void OnTickCall(object sender, TickTimeTimer.OnTickEventArgs args)
        {
            if (PlayerInAlertRange()) moveTick += 1;
        }

        public virtual void OnPause()
        {
            _currentCanMoveBeforePause = CanMove;
            CanMove = false;
        }

        public virtual void OnUnpause()
        {
            CanMove = _currentCanMoveBeforePause;
        }

        public bool CanMove { get; set; }

        public void Movement() => MovementControl();

        protected abstract void MovementControl();
    }
}