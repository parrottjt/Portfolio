using TSCore.Time;
using UnityEngine;

namespace FrikinCore.AI.Enemy.Movement
{
    public class MovementEnemyMasterNinjaStarFish : MovementEnemyAbstract
    {
        bool runMasterNinjaCheck;

        [SerializeField] float moveRadius = 10;

        protected override void OnTickCall(object sender, TickTimeTimer.OnTickEventArgs args)
        {

        }

        protected override void MovementControl()
        {
            var startPoint = dataEnemy.PawnStartPosition;
            var leashDistance = moveRadius;
            float minX = startPoint.x - leashDistance, minY = startPoint.y - leashDistance;
            float maxX = startPoint.x + leashDistance, maxY = startPoint.y + leashDistance;

            Vector2 newPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            RaycastHit2D hit = Physics2D.Linecast(transform.position, newPosition);

            if (hit.collider == null) transform.position = newPosition;
            runMasterNinjaCheck = false;
        }

        protected override void Update()
        {
            if (runMasterNinjaCheck) MovementControl();
        }


        public void RunMasterNinjaMove()
        {
            runMasterNinjaCheck = true;
        }
    }
}