using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetWaypoint : MonoBehaviour
{
    /// <summary>
    /// Target waypoint controller
    /// </summary>

    [SerializeField]
    Image img;
    [SerializeField]
    Transform target;
    [SerializeField]
    Vector3 waypointOffset;
    [SerializeField]
    TextMeshProUGUI targetDistanceText;

    int distanceToTarget;

    void Update()
    {
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + waypointOffset);

        if(Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            //Target is behind the player
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
                pos.x = minX;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
        distanceToTarget = (int)Vector3.Distance(target.position, transform.position);

        targetDistanceText.text = distanceToTarget.ToString() + "m";
    }
}
