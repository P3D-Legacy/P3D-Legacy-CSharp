using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Resources.Models;

public class SlideModel : BaseModel<SlideModel>
{
    public override int ID => 2;

    static SlideModel()
    {
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0, 0, 1), new Vector2(0.0f, 1.0f))); //h
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0, 0, 1), new Vector2(0.0f, 0.0f))); //e
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0, 0, 1), new Vector2(1.0f, 1.0f))); //c

        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0, 0, 1), new Vector2(1.0f, 1.0f))); //c
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0, 0, 1), new Vector2(0.0f, 0.0f))); //e
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0, 0, 1), new Vector2(1.0f, 0.0f))); //d

        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(1, 0, 0), new Vector2(0.0f, 1.0f))); //c
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1, 0, 0), new Vector2(0.0f, 0.0f))); //d
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(1, 0, 0), new Vector2(1.0f, 1.0f))); //b

        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(-1, 0, 0), new Vector2(1.0f, 0.0f))); //e
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(-1, 0, 0), new Vector2(1.0f, 1.0f))); //h
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-1, 0, 0), new Vector2(0.0f, 1.0f))); //a

        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0, 0, -1), new Vector2(1, 0)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(1, 1)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(0, 1)));

        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(0, 1)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0, 0, -1), new Vector2(0, 0)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0, 0, -1), new Vector2(1, 0)));

        SetupVb();
    }

}
