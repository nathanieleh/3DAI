using UnityEngine;

public class GoalScript : MonoBehaviour
{
    [SerializeField] private GameObject manager;


    public void Reposition()
    {
        transform.localPosition = new Vector3(Random.Range(16f, 64f), /*15f*/Random.Range(15f, 25f), Random.Range(-29f, 19f));
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Reposition();
            manager.GetComponent<GameManager>().IncrementScore();
        }
    }
}
