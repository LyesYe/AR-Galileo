using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public Transform playerTransform; // Player's transform
    public Renderer fogOfWarRenderer; // Renderer component of fog of war geometry
    public float revealRadius = 3f; // Radius around the player to reveal

    private void Update()
    {
        // Calculate fog of war texture coordinates based on player position
        Vector3 playerPosition = playerTransform.position;
        Vector2 fogOfWarUV = new Vector2(playerPosition.x, playerPosition.z) / revealRadius + new Vector2(0.5f, 0.5f);

        // Update fog of war texture coordinates
        fogOfWarRenderer.material.SetTextureOffset("_MainTex", fogOfWarUV);
        fogOfWarRenderer.material.SetTextureScale("_MainTex", new Vector2(1f / revealRadius, 1f / revealRadius));
    }
}
