using UnityEngine;
using UnityEngine.UI;


public class Test_Float : MonoBehaviour
{
    public RectTransform image;
    public float height = 1.34f;
    public float frequency = 50f;
    public float speed = 3f;

    void Start()
    {
        image = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = image.position;
        p.y = (Mathf.Cos(Time.time*speed)/frequency)+height;
        transform.position = p;
    }
}
