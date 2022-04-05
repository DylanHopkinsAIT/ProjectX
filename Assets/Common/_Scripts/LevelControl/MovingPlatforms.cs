using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    //moveToWaypoint variables
    [SerializeField] private GameObject[] anchorArray;
    [SerializeField] private float speed;
    private int anchorIndex;
    
    // Update is called once per frame
    void Update()
    {
        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        // Process the current instruction in our control data array
        GameObject waypoint = anchorArray[anchorIndex];

        // Find the range to close vector
        Vector3 rangeToClose = waypoint.transform.position - transform.position;
        
        // What's our distance to the waypoint?
        float distance = rangeToClose.magnitude;

        // How far do we move each frame
        float speedDelta = speed * Time.deltaTime;

        // If we're close enough to the current waypoint 
        // then increase the pattern index

        if (distance <= speedDelta)
        {
            anchorIndex++;
            // Reset the patternIndex if we are at the end of the instruction array
            if (anchorIndex >= anchorArray.Length)
            {
                anchorIndex = 0;
            }

            // Process the current instruction in our control data array
            waypoint = anchorArray[anchorIndex];

            // Find the new range to close vector
            rangeToClose = waypoint.transform.position - transform.position;
        }

        // In what direction is our waypoint?
        Vector3 normalizedRangeToClose = rangeToClose.normalized;
        
        Vector3 delta = speedDelta * normalizedRangeToClose;

        transform.Translate(delta);
    }
}
