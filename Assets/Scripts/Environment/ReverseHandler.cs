using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ReverseHandler : MonoBehaviour
{
    [Header("Object Configuration")]
    [SerializeField] private bool canReverse;
    [SerializeField] private bool isReversed;
    [SerializeField] private bool isTile;

    private TilemapCollider2D tileMapCollider;
    private TilemapRenderer tilemapRenderer;
    private SpriteRenderer sr;
    private Collider2D col;
    private bool currentReverseStatus;
    void OnEnable()
    {
        GameManager.OnPlayerSpawned += Initialize;
        PlayerInputController_Platformer.OnPlayerJump += TriggerReverse;
        PlayerHandler.OnPlayerDeath += Initialize;
    }

    void OnDisable()
    {
        GameManager.OnPlayerSpawned -= Initialize;
        PlayerInputController_Platformer.OnPlayerJump -= TriggerReverse;
        PlayerHandler.OnPlayerDeath -= Initialize;
    }
    public void Initialize()
    {
        if (!canReverse) return;
        if (isTile)
        {
            tileMapCollider = GetComponent<TilemapCollider2D>();
            tilemapRenderer = GetComponent<TilemapRenderer>();
        }
        else
        {
            sr = GetComponent<SpriteRenderer>();
            col = GetComponent<Collider2D>();
        }

        if (canReverse)
        {
            if (isTile)
            {
                if (isReversed) DisablePlatform();
                else EnablePlatform();
            }
            else
            {
                if (isReversed) DisableObject();
                else EnableObject();
            }

        }
        currentReverseStatus = isReversed;
    }

    private void TriggerReverse()
    {
        if (!canReverse) return;
        currentReverseStatus = !currentReverseStatus;
        if (isTile)
        {
            if (currentReverseStatus) DisablePlatform();
            else EnablePlatform();
        }
        else
        {
            if (currentReverseStatus) DisableObject();
            else EnableObject();
        }
    }

    private void DisablePlatform()
    {
        tileMapCollider.enabled = false;
        tilemapRenderer.enabled = false;
    }

    private void EnablePlatform()
    {
        tileMapCollider.enabled = true;
        tilemapRenderer.enabled = true;
    }

    private void DisableObject()
    {
        sr.enabled = false;
        col.enabled = false;
    }

    private void EnableObject()
    { 
        sr.enabled = true;
        col.enabled = true;
    }
}
