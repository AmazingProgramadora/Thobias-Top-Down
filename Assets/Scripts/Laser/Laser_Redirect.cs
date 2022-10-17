using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Redirect : MonoBehaviour
{
    #region Declarations
    [SerializeField] float maxRayDistance = 100;
    public Transform firePoint;
    public LineRenderer lineRenderer;

    bool shooting;
    #endregion

    private void Update()
    {
        if (!shooting)
        {
            lineRenderer.enabled = false;
        }
        shooting = false;


    }
    public void ChangeFirePoint(Vector2 position)
    {
        firePoint.position = position + new Vector2(-0.01f,-0.01f);
        ShootLaser();
        shooting = true;
        lineRenderer.enabled = true;
    }
    void ShootLaser()
    {
        if (Physics2D.Raycast(transform.position, transform.up * -1))
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, transform.up * -1);
            DrawRay(firePoint.position, hit.point);
        }
        else
        {
            DrawRay(firePoint.position, firePoint.up * -1 * maxRayDistance);
        }
    }

    void DrawRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

}
