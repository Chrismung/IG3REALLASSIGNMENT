using System.Collections;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public GameObject cherryPrefab;
    public float spawntime = 10f;
    public float speed = 5f;
    private Camera maincam;
    private Vector3 screencenter;

    void Start()
    {
        maincam = Camera.main;
        screencenter = maincam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, maincam.nearClipPlane));
        screencenter.z = 0; // Make sure z-axis is always zero

        StartCoroutine(SpawnCherry());
    }

    IEnumerator SpawnCherry()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawntime);
            // Chooses a random side
            int randomSide = Random.Range(0, 4);
            Vector3 startpos = GetRandomStartPosition(randomSide);
            Vector3 endpos = GetAlignedEndPosition(startpos);

            // Instantiate the cherry and start the movement coroutine for each cherry
            GameObject cherry = Instantiate(cherryPrefab, startpos, Quaternion.identity);
            StartCoroutine(MoveCherry(cherry, startpos, endpos));
        }
    }

    Vector3 GetRandomStartPosition(int side)
    {
        //switch case with inputting the randomly generated side number to give a spawn position based of the generated side number
        switch (side)
        {
            case 0: // Left
                return maincam.ViewportToWorldPoint(new Vector3(-0.1f, Random.Range(0f, 1f), maincam.nearClipPlane));
            case 1: // Right
                return maincam.ViewportToWorldPoint(new Vector3(1.1f, Random.Range(0f, 1f), maincam.nearClipPlane));
            case 2: // Top
                return maincam.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1.1f, maincam.nearClipPlane));
            case 3: // Bottom
                return maincam.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), -0.1f, maincam.nearClipPlane));
            default:
                return Vector3.zero;
        }
    }

    Vector3 GetAlignedEndPosition(Vector3 startpos)
    {
        //Get the direction between startpos to center, then from there calculate the direction for center to end by extrapolating it
        Vector3 direction = (screencenter - startpos).normalized;
        float distanceToScreenEdge = Mathf.Max(
            Mathf.Abs(maincam.orthographicSize / direction.y),
            Mathf.Abs(maincam.orthographicSize * maincam.aspect / direction.x)
        );
        return startpos + direction * distanceToScreenEdge * 2; //move across the main camera
    }

    IEnumerator MoveCherry(GameObject cherry, Vector3 startpos, Vector3 endpos)
    {
        while (cherry != null)
        {
            cherry.transform.position = Vector3.MoveTowards(cherry.transform.position, endpos, speed * Time.deltaTime);
            // check for cherry reaching end pos
            if (Vector3.Distance(cherry.transform.position, endpos) < 0.1f)
            {
                Destroy(cherry);
                yield break; //end coroutine
            }

            yield return null; //end and wait for next frame
        }
    }
}