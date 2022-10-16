using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Shooter : MonoBehaviour
{
    #region Declarations
    [SerializeField] float maxRayDistance = 100;
    public Transform firePoint;
    public LineRenderer lineRenderer;
    #endregion


    private void Update()
    {
        ShootLaser();
    }
    void ShootLaser()
    {
       if(Physics2D.Raycast(transform.position, transform.right))
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, transform.right);
            DrawRay(firePoint.position, hit.point);
            Laser_Redirect laser_Redirect = hit.collider.GetComponent<Laser_Redirect>();

            if (laser_Redirect != null)
            {
                laser_Redirect.ChangeFirePoint(hit.point);
            }
        }
        else
        {
            DrawRay(firePoint.position, firePoint.right * maxRayDistance);
        }
    }

    void DrawRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
