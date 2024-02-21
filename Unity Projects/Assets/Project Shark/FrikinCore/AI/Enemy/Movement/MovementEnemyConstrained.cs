using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TSCore.Utils;
using UnityEngine;

namespace FrikinCore.AI.Enemy.Movement
{
    public class MovementEnemyConstrained : MovementEnemyAbstract
    {
        [EnumToggleButtons] [SerializeField] MoveDirections directions;

        List<Vector2> _availableMoveDirections = new List<Vector2>();

        int _directionNumber;
        int _moveIndex;

        bool IsSingleDirectionMover => _availableMoveDirections.Count == 1;

        protected override void Awake()
        {
            base.Awake();
            ConstrainedMovementSetup();
        }

        protected override void MovementControl()
        {
            switch (movementState)
            {
                case MovementState.Idle:
                    if (PlayerInAlertRange())
                    {
                        movementState = MovementState.Movement;
                    }

                    break;
                case MovementState.Movement:
                    if (runRandom)
                    {
                        moveDirection =
                            (IsSingleDirectionMover
                                ? _availableMoveDirections.First()
                                : _availableMoveDirections[_moveIndex]).normalized;
                        randomMoveTickTotal = maxRandomMoveTick;
                        runRandom = false;
                    }
                    else
                    {
                        MoveAwayFromWallCheck();
                        if (moveTick >= randomMoveTickTotal)
                        {
                            runRandom = true;
                            if (_moveIndex < _availableMoveDirections.Count - 1)
                            {
                                _moveIndex++;
                            }
                            else _moveIndex = 0;

                            SpriteFlip();
                            moveTick = 0;
                        }
                    }

                    transform.Translate(moveDirection * AdjustedMoveSpeed());

                    if (PlayerInAlertRange() == false)
                    {
                        movementState = MovementState.MoveHome;
                        moveTick = 0;
                    }

                    break;
                case MovementState.MoveAwayFromWall:
                    if (moveTick >= 5)
                    {
                        runRandom = true;
                        moveTick = 0;
                        movementState = MovementState.Movement;
                    }

                    transform.Translate(closestPoint.normalized * AdjustedMoveSpeed());
                    break;
                case MovementState.MoveHome:
                    if (Conditions.DistanceBetweenLessThan(PawnCurrentPosition(), dataEnemy.PawnStartPosition, 1f))
                    {
                        runRandom = true;
                        movementState = MovementState.Idle;
                    }

                    transform.position =
                        Vector2.MoveTowards(transform.position, dataEnemy.PawnStartPosition, AdjustedMoveSpeed());

                    break;
                default:
                    DebugScript.LogError($"{gameObject.name}",
                        "MovementControl() reverted to default, a case was not reached");
                    break;
            }
        }

        void ConstrainedMovementSetup()
        {
            _directionNumber = (int)directions;
            var moveDirectionsAsStrings = Enums.GetValuesAsStrings<MoveDirections>();

            for (int i = moveDirectionsAsStrings.Length - 1; i >= 0; i--)
            {
                var direction = Enums.Parse<MoveDirections>(moveDirectionsAsStrings[i]);
                if (_directionNumber >= (int)direction)
                {
                    _availableMoveDirections.Add(MoveDirectionVectorDictionary[direction]);
                    _directionNumber -= (int)direction;
                }
            }

            _availableMoveDirections.Reverse();
        }
    }
}