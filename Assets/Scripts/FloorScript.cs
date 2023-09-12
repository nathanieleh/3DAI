using UnityEngine;

public class FloorScript : MonoBehaviour
{
    private new Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.color = Color.gray;
    }
}
