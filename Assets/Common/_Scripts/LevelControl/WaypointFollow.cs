using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int waypointIndex;

    [SerializeField] private float speed;
    
    private void FixedUpdate()
    {
        //If the platform and waypoint are close (.1f for imprecision)
        if (Vector2.Distance(waypoints[waypointIndex].transform.position, transform.position) < 0.1f)
        {
            //Move to next waypoint
            waypointIndex++;
            
            //If at last waypoint in array
            if (waypointIndex >= waypoints.Length)
            {
                waypointIndex = 0;
            }
            
        }

        //Get delta time since using transforms
        var speedDelta = speed * Time.deltaTime;

        //Move platform towards waypoint at delta speed
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position,speedDelta);
    }
}
