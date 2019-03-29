using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Vector2Int position;

    public static Entity Create(Transform parent)
    {
        GameObject go = new GameObject("entity", typeof(SpriteRenderer), typeof(Entity));
        Entity entity = go.GetComponent<Entity>();
        entity.spriteRenderer = go.GetComponent<SpriteRenderer>();
        entity.spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask; // TODO: this is really only for the teaser trailer
        entity.transform.parent = parent;
        return entity;
    }

    public static Entity Create(Transform parent, Material material, Tileset tileset, char c, TileMap map, int posX, int posY, Color color)
    {
        Entity entity = Create(parent);
        entity.SetMaterial(material);
        entity.SetTile(tileset, c);
        entity.SetPosition(map, posX, posY);
        entity.SetColor(color);
        return entity;
    }

    /*
    SpriteRenderer CreateSprite(char chr, Color color)
    {
        int tileIndex = CharacterMapper.GetIndex(chr);
        var rect = tileset.GetRectForTileIndex(tileIndex).ToRect();
        Sprite sprite = Sprite.Create(scaledTileAtlas, rect, Vector2.zero, pixelsPerUnit);
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaterial(Material material)
    {
        spriteRenderer.material = material;
    }

    public void SetTile(Tileset tileset, char c)
    {
        int tileIndex = CharacterMapper.GetIndex(c);
        var rect = tileset.GetRectForTileIndex(tileIndex).ToRect();
        spriteRenderer.sprite = Sprite.Create(tileset.atlas, rect, Vector2.zero, 1);
    }

    public void SetPosition(TileMap map, int x, int y)
    {
        position.x = x;
        position.y = y;
        transform.position = map.TileCoordsToSpritePosition(position.x, position.y);
    }

    public void SetGlow(float glow)
    {
        spriteRenderer.material.SetFloat("_Glow", glow);
    }

    public void SetColor(Color c)
    {
        spriteRenderer.material.SetColor("_Color", c);
    }

    public Color GetColor()
    {
        return spriteRenderer.material.GetColor("_Color");
    }

}
