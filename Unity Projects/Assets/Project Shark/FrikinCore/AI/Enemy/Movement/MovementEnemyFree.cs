using TSCore.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FrikinCore.AI.Enemy.Movement
{
    public class MovementEnemyFree : MovementEnemyAbstract
    {
        float randomX, randomY;

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            EnemyFlip();
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
                        randomX = Random.Range(-1f, 1f);
                        randomY = Random.Range(-1f, 1f);
                        moveDirection = new Vector2(randomX, randomY).normalized;
                        randomMoveTickTotal = Random.Range(minRandomMoveTick, maxRandomMoveTick + 1);
                        runRandom = false;
                    }
                    else
                    {
                        MoveAwayFromWallCheck();
                        if (moveTick >= randomMoveTickTotal)
                        {
                            runRandom = true;
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

                    transform.position = Vector2.MoveTowards(transform.position, dataEnemy.PawnStartPosition,
                        AdjustedMoveSpeed());

                    break;
                default:
                    DebugScript.LogError($"{gameObject.name}",
                        "MovementControl() reverted to default, a case was not reached");
                    break;
            }
        }
    }
}