using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Shooter : MonoBehaviour
{
    #region Declarations
    [SerializeField] float maxRayDistance = 100;
    public Transform firePoint;
    public LineRenderer lineRenderer;

    Laser_Redirect laser_Redirect;
    Laser_End laser_End;
    #endregion


    private void Update()
    {
        ShootLaser();
    }
    #region LASER
    void ShootLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, transform.right);

        DrawRay(firePoint.position, hit.point);

        if (hit.collider && hit.collider.CompareTag("Mirror"))
        {

            if (laser_Redirect != null)
            {
                laser_Redirect.ChangeFirePoint(hit.point, hit.normal, transform.position);
            }
            else
            {
                    laser_Redirect = hit.collider.GetComponent<Laser_Redirect>();
            }
        }
        else
        {
            if (hit.collider.CompareTag("Laser End"))
            {
                laser_End = hit.collider.GetComponent<Laser_End>();
                laser_End.Activate();
            }

            DrawRay(firePoint.position, hit.point);
            if (laser_Redirect)
            {
                laser_Redirect = null;
            }
        }
    }

    void DrawRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
    #endregion
}