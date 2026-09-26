using System.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private BoxCollider2D spawnArea;
    [SerializeField] private Sprite[] ballSprites;
    [SerializeField] private float spawnInterval = 0.5f;

    public AudioClip _bgm;

    private void Start()
    {
        StartCoroutine(SpawnBall());
        SoundManager.Instance.PlayBGM(_bgm);
    }

    private IEnumerator SpawnBall()
    {
        while (true)
        {
            Bounds bounds = spawnArea.bounds;
            float randomX = Random.Range(bounds.min.x, bounds.max.x);

            GameObject ball = Instantiate(ballPrefab, new Vector3(randomX, bounds.center.y, 0f), Quaternion.identity);

            SpriteRenderer sprite = ball.GetComponent<SpriteRenderer>();
            sprite.sprite = ballSprites[Random.Range(0, ballSprites.Length)];

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}