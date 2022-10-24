using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Redirect : MonoBehaviour
{
    #region Declarations
    public Transform firePoint;
    public LineRenderer lineRenderer;

    Laser_Redirect laser_Redirect;
    Laser_End laser_End;
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
    #region LASER
    public void ChangeFirePoint(Vector2 position, Vector2 normal, Vector2 origin)
    {
        StartCoroutine(ChangeFirePointRoutine(position, normal, origin));
    }

    /// <summary>
    /// Muda onde o laser vai
    /// </summary>
    /// <param name="position"> Onde o raycast anterior acertou</param>
    /// <param name="normal"> Normal(90º da superficie)</param>
    /// <param name="origin">posicao do objeto anterior</param>
    /// <returns></returns>
    IEnumerator ChangeFirePointRoutine(Vector2 position, Vector2 normal, Vector2 origin)
    {
        yield return new WaitForEndOfFrame();

        Vector2 directiona = (position - origin).normalized;
        directiona=directiona.Snap();

        firePoint.position = position;

        StartCoroutine(ShootLaser(normal, directiona));
        shooting = true;
        lineRenderer.enabled = true;

    }
    public void StopFire()
    {
        laser_Redirect = null;
    }
    /// <summary>
    /// Atira o Laser
    /// </summary>
    /// <param name="normal"> Normal </param>
    /// <param name="dir">Vetor de onde o laser veio</param>
    /// <returns></returns>
    IEnumerator ShootLaser(Vector2 normal, Vector2 dir)
    {
        yield return new WaitForEndOfFrame();

        Vector2 reflectDirection = Vector2.Reflect(dir, normal);
        //Vector2 reflectDirection = ReflectDirection(dir, normal);

        RaycastHit2D hit = Physics2D.Raycast( (Vector2)firePoint.position + (reflectDirection) , reflectDirection);

        DrawRay(firePoint.position, hit.point);

        if (hit.collider.CompareTag("Mirror") && hit.collider != null)
        {

            if (laser_Redirect != null)
            {
                laser_Redirect.ChangeFirePoint(hit.point, hit.normal, firePoint.position);

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
                laser_Redirect.StopFire();
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