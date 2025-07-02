//using UnityEngine;

//public class OrcPOV : MonoBehaviour
//{
//    [HideInInspector] public Orc orc;

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            Debug.Log("Player vào vùng phát hiện");
//            orc.target = other.transform;
//            orc.isChasing = true;

//            orc.stateMachine.SetState(new OrcMoveState(orc.animator, orc));
//            return;
//        }

//        RaycastHit2D hit = Physics2D.Raycast(
//            orc.transform.position,
//            other.transform.position - orc.transform.position,
//            orc.distanceToTarget,
//            orc.targetLayerMask
//        );

//        if (hit.collider != null && hit.collider.CompareTag("Tower"))
//        {
//            Debug.Log("Entered Tower");
//            orc.stateMachine.SetState(new OrcAttackState(orc.animator, orc, orc.enemyData.attackCooldown));
//        }
//    }

//    private void OnTriggerExit2D(Collider2D other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            Debug.Log("Player rời khỏi vùng");
//            orc.target = orc.towerTarget;
//            orc.isChasing = false;
//            orc.stateMachine.SetState(new OrcMoveState(orc.animator, orc));
//            return;
//        }

//        if (other.CompareTag("Tower"))
//        {
//            Debug.Log("Exited Tower");
//            orc.stateMachine.SetState(new OrcMoveState(orc.animator, orc));
//        }
//    }
//}
