using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
using UnityEngine.SceneManagement;

public class fishingCoroutine : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject fish;
    [SerializeField] private GameObject bait;
    [SerializeField] private GameObject handpoint;
    [SerializeField] private float baitspeed= 0.1f;
    [SerializeField] private float fishspeed = 0.5f;
    [SerializeField] private float catchTimeWindow = 3.0f;
    [SerializeField] private Canvas caught;
    private float originalTimeScale;
    public GameObject fishgotaway;//menu canvas
    bool playerReacted = false;
    [SerializeField] private TMP_Text reel;
    private Vector3 originalPosition;
    LineRenderer lineRenderer;

    private void Start()
    {
        caught.gameObject.SetActive(false);
        if (!caught) Debug.LogError("Please assign the Canvas component in the inspector.");
        originalTimeScale = Time.timeScale;
        caught.gameObject.SetActive(false);
        fishgotaway.SetActive(false);
        bait.SetActive(false);
        bait.transform.position = handpoint.transform.position;
        originalPosition = reel.transform.position;
        reel.gameObject.SetActive(false);

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, handpoint.gameObject.transform.position);
        lineRenderer.SetPosition(1, handpoint.gameObject.transform.position);
    }

    private bool throwBait = false;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !throwBait)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500, LayerMask.GetMask("WaterBody")))
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

        // 调用鱼接近的协程
        yield return FishApproaches();
        yield return CatchFish();
    }

    IEnumerator FishingSequence()
    {
        // Cast the line
        yield return StartCoroutine(CastLine());

        // Fish approaches the bait
        yield return StartCoroutine(FishApproaches());

        // Give the player a window of time to react
        yield return new WaitForSeconds(3);  // window to press space

        if (playerReacted)  // Check the flag to see if the player pressed space in time
        {
            playerReacted = false;  // Reset the flag for the next attempt
            yield return StartCoroutine(ReelFish());
        }
        else
        {
            // Fish got away or player didn't react in time
            yield return StartCoroutine(FishGotAway());
        }
    }

    IEnumerator ReelFish()
    { 
        lineRenderer.SetPosition(1, fish.gameObject.transform.position);
        var dir = handpoint.transform.position - fish.gameObject.transform.position;
        dir.Normalize();
        while (true)
        {
            fish.gameObject.transform.position += dir * baitspeed;
            lineRenderer.SetPosition(1, fish.gameObject.transform.position);
            if (Vector3.Distance(fish.transform.position, handpoint.transform.position) < 0.05f)
            {
                // TODO 鱼到手里了
                PauseGame();
                caught.gameObject.SetActive(true);
                reel.gameObject.SetActive(false);
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
        caught.gameObject.SetActive(false);
    }
    public void ReloadActiveScene()
    {
        Time.timeScale = 1f; // Reset the timescale
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the active scene
    }
    IEnumerator FishApproaches()
    {
        var baitPos = bait.transform.position;
        var dir = bait.transform.position - fish.transform.position;
        dir.Normalize();
        while (true)
        {
            fish.transform.position += dir * fishspeed;
            if (Vector3.Distance(fish.transform.position, baitPos) < 0.05f)
            {
                break;
            }

            yield return null;
        }
        
        bait.gameObject.SetActive(false);
    }
    
    // 检查玩家是否在一个时间窗口内按空格键
    IEnumerator CatchFish(float shakeDuration = 5f, float shakeMagnitude = 2f)
    {
        reel.gameObject.SetActive(true);
        StartCoroutine(ShakeText(shakeDuration, shakeMagnitude));
        var time = 0f;
        bool catchedFish = false;
        while (time < catchTimeWindow)
        {
            // 在时间窗口内按下空格键
            if (Input.GetKeyDown(KeyCode.Space))
            {
                catchedFish = true;
                break;
            }

            time += Time.deltaTime;
            yield return null;
        }

        // 如果捉到了鱼
        if (catchedFish)
        {
            yield return ReelFish();
        }
        else
        {
            // TODO 失败
            yield return FishGotAway();
        }

    }
    IEnumerator ShakeText(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            reel.transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        reel.transform.position = originalPosition; // Reset back to original position after shaking
    }
    IEnumerator CastLine()
    {
        Debug.Log("castline");
        yield return new WaitForSeconds(1);  // assuming the casting animation is 1 second
    }

    IEnumerator FishGotAway()
    {
        Debug.Log("fishgotaway");
        yield return new WaitForSeconds(3);  // time it takes for fish to escape
        fishgotaway.gameObject.SetActive(true);
        reel.gameObject.SetActive(false);
    }
    IEnumerator MoveFishToBait()
    {
        yield return new WaitForSeconds(2);
        fish.transform.position = bait.transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
