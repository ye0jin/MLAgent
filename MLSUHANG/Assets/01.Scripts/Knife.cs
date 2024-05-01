using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine; 

public class Knife : Agent
{
    [SerializeField] private float moveSpeed = 10f;
    private Rigidbody2D rigid;

    public override void Initialize()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public override void OnEpisodeBegin()
    {
        Target.Instance.SetGame();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        //sensor.AddObservation();
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        var action = actions.DiscreteActions;
        if (action[0] == 1)
        {
            Target.Instance.ShootKnife(this);
            rigid.velocity = new Vector2(0, moveSpeed);
        }
        if(Mathf.Abs(transform.position.y - 1.4f) <= 0.01f)
        {
            rigid.velocity = Vector2.zero;
            transform.SetParent(Target.Instance.transform);
        }
        AddReward(-1f / MaxStep);
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var action = actionsOut.DiscreteActions;

        if (Input.GetKey(KeyCode.Space))
        {
            action[0] = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Knife")) // 나이프에 박았을 때
        {
            AddReward(-1f);
            Target.Instance.ResetGame();
            EndEpisode();
        }
        else if (collision.CompareTag("Apple")) // 사과에 박았을 때
        {
            AddReward(1f);
        }
        else if (collision.CompareTag("Wood")) // 나무에 박았을 때
        {
            AddReward(-0.1f);
        }
    }
}
