using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Resources.Models;

public class CliffCornerModel : BaseModel<CliffCornerModel>
{
    public override int ID => 11;

    static CliffCornerModel()
    {
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), Vector3.Up, new Vector2(0, 1)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), Vector3.Up, new Vector2(0, 0)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), Vector3.Up, new Vector2(1, 0)));

        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), Vector3.Up, new Vector2(1, 0)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), Vector3.Up, new Vector2(1, 1)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), Vector3.Up, new Vector2(0, 1)));

        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.25f, 0.5f), new Vector3(-1, 0, 0), new Vector2(1.0f, 0.0f))); //e
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(-1, 0, 0), new Vector2(1.0f, 1.0f))); //h
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.25f), new Vector3(-1, 0, 0), new Vector2(0.0f, 1.0f))); //a

        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0, 0, 1), new Vector2(0.0f, 1.0f))); //h
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.25f, 0.5f), new Vector3(0, 0, 1), new Vector2(0.0f, 0.0f))); //e
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.25f, -0.5f, 0.5f), new Vector3(0, 0, 1), new Vector2(1.0f, 1.0f))); //c

        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.25f, -0.5f, 0.25f), new Vector3(1, 0, 0), new Vector2(0.0f, 1.0f))); //b
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.25f, -0.5f, 0.5f), new Vector3(1, 0, 0), new Vector2(1.0f, 1.0f))); //c
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.25f, 0.5f), new Vector3(1, 0, 0), new Vector2(1.0f, 0.0f))); //e

        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.25f, 0.5f), new Vector3(0, 0, -1), new Vector2(1.0f, 0.0f))); //e
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.25f), new Vector3(0, 0, -1), new Vector2(1.0f, 1.0f))); //a
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.25f, -0.5f, 0.25f), new Vector3(0, 0, -1), new Vector2(0.0f, 1.0f))); //b

        SetupVb();
    }

}