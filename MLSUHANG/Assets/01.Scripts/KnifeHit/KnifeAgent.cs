using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine; 

public class KnifeAgent : Agent
{
    [SerializeField] private Target target;
    [SerializeField] private float moveSpeed = 10f;
    private Rigidbody2D rigid;
    private Vector3 originPos;

    public override void Initialize()
    {
        rigid = GetComponent<Rigidbody2D>();
        originPos = transform.localPosition;
    }
    public override void OnEpisodeBegin()
    {
        target.SetGame();
        rigid.velocity = Vector2.zero;

        transform.localPosition = originPos;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(target.CurrentApple.transform.localPosition); 
        sensor.AddObservation(transform.localPosition); 
        sensor.AddObservation(target.CurrentApple.transform.localPosition.y - transform.localPosition.y); // y 값만 잇어도 돼
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        var action = actions.DiscreteActions;
        if (action[0] == 1)
        {
            rigid.velocity = new Vector2(0, moveSpeed);
        }
        if(Mathf.Abs(transform.localPosition.y - 1.4f) <= 0.5f)
        {
            //print("d");
            target.ResetGame();
            EndEpisode();
        }
        //print(Mathf.Abs(transform.localPosition.y - 1.4f));
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
        if (collision.CompareTag("Apple")) // 사과에 박았을 때
        {
            AddReward(2f);
        }
        else if (collision.CompareTag("Knife") || collision.CompareTag("Wood")) // 나무에 박았을 때
        {
            AddReward(-1f);
        }
    }
}
