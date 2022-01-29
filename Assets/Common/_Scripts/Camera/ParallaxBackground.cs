using UnityEngine;
public class ParallaxBackground : MonoBehaviour
{
    private float _length, _startpos;
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffect;

    void Start()
    {
        _startpos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        var position = cam.transform.position;
        float temp = (position.x * (1 - parallaxEffect));
        float dist = (position.x * parallaxEffect);

        var transform1 = transform;
        var position1 = transform1.position;
        position1 = new Vector3(_startpos + dist, position1.y, position1.z);
        transform1.position = position1;
        if (temp > _startpos + _length)
        {
            _startpos += _length;
        }
        else if (temp < _startpos - _length)
        {
            _startpos -= _length;
        }
    }
}