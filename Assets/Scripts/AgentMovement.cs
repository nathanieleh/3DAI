using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentMovement : Agent
{
    [SerializeField] private GameObject manager;
    [SerializeField] private Transform target;
    #pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    [SerializeField] private Renderer renderer;
    public float speed;

    public void Start()
    {
        renderer = renderer.GetComponent<Renderer>();
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(16f, 64f), 16f, Random.Range(-29f, 19f)/*40, 16, -5*/);
        target.localPosition = new Vector3(Random.Range(16f, 64f), /*15f*/Random.Range(15f, 25f), Random.Range(-29f, 19f));
    }

    public void Update()
    {
        AddReward(-0.001f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float moveY = actions.ContinuousActions[2];

        transform.localPosition += new Vector3(moveX, /*0*/moveY, moveZ) * Time.deltaTime * speed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
        continuousActions[2] = Input.GetAxis("Jump");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Goal")
        {
            AddReward(+15f);
            renderer.material.color = Color.green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Bounds")
        {
            AddReward(-2f);
            renderer.material.color = Color.red;
            manager.GetComponent<GameManager>().Reset();
            EndEpisode();
        }
    }
}
