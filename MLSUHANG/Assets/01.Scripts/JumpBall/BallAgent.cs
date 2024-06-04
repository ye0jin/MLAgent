using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class BallAgent : Agent
{
    private Rigidbody2D rigid;
    private SpriteRenderer visual;

    private LayerMask playerColorID;

    [Header("Value")]
    [SerializeField] private float jumpPower = 15f;
    [SerializeField] private float speed = 3f;
    private bool canReverse = true;

    [Header("Manager")]
    [SerializeField] private JumpBallManager manager;

    public override void Initialize()
    {
        rigid = GetComponent<Rigidbody2D>();
        visual = transform.Find("Visual").GetComponent<SpriteRenderer>();
    }
    public override void OnEpisodeBegin()
    { 
        manager.SetGame();
        SetPlayerColor(1);

        transform.localPosition = Vector3.zero;
        rigid.velocity = new Vector2(speed, jumpPower);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(manager.target.localPosition);
        sensor.AddObservation((transform.localPosition - manager.target.localPosition).magnitude);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        var action = actions.DiscreteActions;
        if (action[0] == 1)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
        }

        AddReward(-1 / MaxStep);
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var action = actionsOut.DiscreteActions;

        if (Input.GetKey(KeyCode.Space))
        {
            action[0] = 1;
        }
    }

    private IEnumerator ReverseCor()
    {
        canReverse = false;
        yield return new WaitForSeconds(0.3f);
        yield return null;
        canReverse = true;
    }

    public void Success()
    {
        if (!canReverse) return;

        float x = -Mathf.Sign(rigid.velocity.x);
        rigid.velocity = new Vector2(x * speed, rigid.velocity.y);

        SetPlayerColor(x);

        manager.CheckWall(x);
        manager.AddScore();

        StartCoroutine(ReverseCor());
    }

    public void SetPlayerColor(float x)
    {
        int idx = Random.Range(0, 4);

        visual.color = manager.Colors[idx];
        playerColorID = manager.ColorID[idx];

        manager.SetTargetTransform(playerColorID, x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Wall>(out Wall wall))
        {
            if (playerColorID != wall.ColorID)
            {
                AddReward(-1f);
                EndEpisode();
                return;
            }

            Success();
            AddReward(1f);
        }
        else // 벽 말고 다른 콜라이더 충돌시
        {
            AddReward(-1f);
            EndEpisode();
        }
    }
}
