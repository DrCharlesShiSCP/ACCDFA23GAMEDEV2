using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class fishingCoroutine : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] fishArray;  // Changed to array
    [SerializeField] private GameObject bait;
    [SerializeField] private GameObject handpoint;
    [SerializeField] private float baitspeed = 0.1f;
    [SerializeField] private float fishspeed = 0.5f;
    [SerializeField] private float catchTimeWindow = 3.0f;
    [SerializeField] private float stopDistance = 0.5f;
    /*[SerializeField] private Canvas caught1Canvas;
    [SerializeField] private Canvas caught2Canvas;
    [SerializeField] private Canvas caught3Canvas;*/

    //[SerializeField] private Canvas caught;
    private GameObject closestFish;
    private float originalTimeScale;
    public GameObject fishgotaway;
    bool playerReacted = false;
    private Vector3 originalPosition;
    LineRenderer lineRenderer;

    private void Start()
    {
        originalTimeScale = Time.timeScale;
        fishgotaway.SetActive(false);
        bait.SetActive(false);
        bait.transform.position = handpoint.transform.position;
        //caught.gameObject.SetActive(false);
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, handpoint.gameObject.transform.position);
        lineRenderer.SetPosition(1, handpoint.gameObject.transform.position);
        /*caught1Canvas.gameObject.SetActive(false);
        caught2Canvas.gameObject.SetActive(false);
        caught3Canvas.gameObject.SetActive(false);*/
    }

    private bool throwBait = false;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !throwBait)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500) && hit.collider.gameObject.name != "island") 
            {
                throwBait = true;
                Debug.Log(hit.point);
                StartCoroutine(ThrowBait(hit.point));
            }
        }
    }

    // 扔鱼饵
    IEnumerator ThrowBait(Vector3 targetPos)
    {
        bait.gameObject.SetActive(true);
        var throwDir = targetPos - bait.transform.position;
        throwDir.Normalize();

        while (true)
        {
            bait.gameObject.transform.position += throwDir * 0.1f;
            lineRenderer.SetPosition(1, bait.gameObject.transform.position);

            if (Vector3.Distance(bait.transform.position, targetPos) < 0.05f)
            {
                break;
            }
            yield return null;
        }
        closestFish = GetClosestFish(bait.transform.position);
        // 调用鱼接近的协程
        yield return FishApproaches(closestFish);
        yield return CatchFish();
    }
    GameObject GetClosestFish(Vector3 baitPos)
    {
        GameObject closestFish = null;
        float closestDistance = float.MaxValue;

        foreach (var fish in fishArray)
        {
            float distance = Vector3.Distance(baitPos, fish.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestFish = fish;
            }
        }

        return closestFish;
    }
    IEnumerator FishingSequence()
    {
        // Cast the line
        yield return StartCoroutine(CastLine());

        // Fish approaches the bait
        yield return StartCoroutine (FishApproaches(closestFish));

        // Give the player a window of time to react
        yield return new WaitForSeconds(3);  // window to press space

        if (playerReacted)  // Check the flag to see if the player pressed space in time
        {
            playerReacted = false;  // Reset the flag for the next attempt
            yield return StartCoroutine(ReelFish(closestFish));
        }
        else
        {
            // Fish got away or player didn't react in time
            yield return StartCoroutine(FishGotAway());
        }
    }

    IEnumerator ReelFish(GameObject closestFish)
    { 
        lineRenderer.SetPosition(1, closestFish.gameObject.transform.position);
        var dir = handpoint.transform.position - closestFish.gameObject.transform.position;
        dir.Normalize();
        while (true)
        {
            closestFish.gameObject.transform.position += dir * baitspeed;
            lineRenderer.SetPosition(1, closestFish.gameObject.transform.position);
           
            if (Vector3.Distance(closestFish.transform.position, handpoint.transform.position) < 0.1f)
            {
                // TODO 鱼到手里了
                //caught.gameObject.SetActive(true);
                Debug.Log("inhand");
                /*switch (closestFish.tag)
                {
                    case "fishe1":
                        caught1Canvas.gameObject.SetActive(true);
                        break;
                    case "fishe2":
                        caught2Canvas.gameObject.SetActive(true);
                        break;
                    case "fishe3":
                        caught3Canvas.gameObject.SetActive(true);
                        break;
                    default:
                        Debug.LogWarning("Unknown fish type: " + closestFish.tag);
                        break;
                }*/

                PauseGame();
                break;
            }

            yield return null;
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = originalTimeScale;
    }
    public void ReloadActiveScene()
    {
        Time.timeScale = 1f; // Reset the timescale
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the active scene
    }
    IEnumerator FishApproaches(GameObject closestFish)
    {
        var baitPos = bait.transform.position;
        Vector3 stopPosition = Vector3.zero;  // variable to record the stop position

        while (true)
        {
            var dir = bait.transform.position - closestFish.transform.position;
            dir.Normalize();
            closestFish.transform.position += dir * fishspeed;

            // Check if the stopping distance is reached
            if (Vector3.Distance(closestFish.transform.position, baitPos) < stopDistance)
            {
                stopPosition = closestFish.transform.position;  // record the stop position
                break;  // exit the loop
            }

            yield return null;
        }

        int touchTimes = Random.Range(2, 5);  // Get a random number between 2 and 4 (5 is exclusive)

        // Loop to make the fish move back and forth between the bait and the stop position
        for (int i = 0; i < touchTimes; i++)
        {
            // Move towards the bait
            while (Vector3.Distance(closestFish.transform.position, baitPos) > 0.05f)
            {
                var dir = bait.transform.position - closestFish.transform.position;
                dir.Normalize();
                closestFish.transform.position += dir * fishspeed;
                yield return null;
            }

            yield return new WaitForSeconds(0.01f);  // Pause for a second at the bait

            // Move back to the stop position
            while (Vector3.Distance(closestFish.transform.position, stopPosition) > 0.05f)
            {
                var dir = stopPosition - closestFish.transform.position;
                dir.Normalize();
                closestFish.transform.position += dir * fishspeed;
                yield return null;
            }

            yield return new WaitForSeconds(1f);  // Pause for a second at the stop position
        }
        while (Vector3.Distance(closestFish.transform.position, baitPos) > 0.05f)
        {
            var dir = bait.transform.position - closestFish.transform.position;
            dir.Normalize();
            closestFish.transform.position += dir * fishspeed;
            yield return null;
        }

        bait.gameObject.SetActive(false);
        bool playerClicked = false;
        float reactionTime = 1.5f;  // 设置反应时间为1.5秒
        float elapsedTime = 0f;

        while (elapsedTime < reactionTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                playerClicked = true;
                break;  // 如果玩家点击，立即跳出循环
            }

            elapsedTime += Time.deltaTime;  // 更新经过的时间
            yield return null;
        }

        if (playerClicked)
        {
            // 如果玩家点击，进入CatchFish协程
            yield return CatchFish();
        }
        else
        {
            // 如果玩家未点击，进入FishGotAway协程
            yield return FishGotAway();
        }

    }


    // 检查玩家是否在一个时间窗口内按鼠标
    IEnumerator CatchFish()
    {
        GameObject closestFish = GetClosestFish(bait.transform.position);

        var baitPos = bait.transform.position;
        float angle = 0f;
        float radius = 1.5f;
        float speed = 10f;
        float time = 0f;
        bool catchedFish = false;
        int requiredClicks = 10;  // Set the number of required clicks to catch the fish
        int clickCount = 0;
        float catchTimeout = 6f;  // 6 seconds timeout
        float catchTimeElapsed = 0f;  // Time elapsed since catching process started

        while (clickCount < requiredClicks && catchTimeElapsed < catchTimeout)
        {
            // Check for mouse clicks
            if (Input.GetMouseButtonDown(0))
            {
                clickCount++;
                if (clickCount >= requiredClicks)
                {
                    catchedFish = true;
                    break;
                }
            }

            float x = baitPos.x + radius * Mathf.Cos(angle);
            float z = baitPos.z + radius * Mathf.Sin(angle);
            closestFish.transform.position = new Vector3(x, closestFish.transform.position.y, z);
            angle += speed * Time.deltaTime;

            time += Time.deltaTime;
            catchTimeElapsed += Time.deltaTime;
            yield return null;
        }

        if (catchedFish)
        {
            yield return ReelFish(closestFish);  // Reel in the fish if it's caught
        }
        else
        {
            Debug.Log("escape");
            yield return FishGotAway();  // The fish got away
        }
    }
    IEnumerator CastLine()
    {
        Debug.Log("castline");
        yield return new WaitForSeconds(1);  // assuming the casting animation is 1 second
    }

    IEnumerator FishGotAway()
    {
        Debug.Log("fishgotaway");
        fishgotaway.gameObject.SetActive(true);
        Time.timeScale = 0f;
        yield return null;  // time it takes for fish to escape
    }
}
