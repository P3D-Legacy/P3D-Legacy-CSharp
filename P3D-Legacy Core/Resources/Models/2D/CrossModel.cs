using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Resources.Models;

public class CrossModel : BaseModel<CrossModel>
{
    public override int ID => 13;

    static CrossModel()
    {
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0), Vector3.Backward, new Vector2(0, 1)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0), Vector3.Backward, new Vector2(0, 0)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0), Vector3.Backward, new Vector2(1, 0)));

        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0), Vector3.Backward, new Vector2(1, 0)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0), Vector3.Backward, new Vector2(1, 1)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0), Vector3.Backward, new Vector2(0, 1)));

        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0, 0.5f, -0.5f), Vector3.Backward, new Vector2(0, 0)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0, -0.5f, -0.5f), Vector3.Backward, new Vector2(0, 1)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0, -0.5f, 0), Vector3.Backward, new Vector2(1, 1)));

        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0, -0.5f, 0), Vector3.Backward, new Vector2(1, 1)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0, 0.5f, 0), Vector3.Backward, new Vector2(1, 0)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0, 0.5f, -0.5f), Vector3.Backward, new Vector2(0, 0)));

        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0, 0.5f, 0), Vector3.Backward, new Vector2(0, 0)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0, -0.5f, 0), Vector3.Backward, new Vector2(0, 1)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0, -0.5f, 0.5f), Vector3.Backward, new Vector2(1, 1)));

        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0, -0.5f, 0.5f), Vector3.Backward, new Vector2(1, 1)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0, 0.5f, 0.5f), Vector3.Backward, new Vector2(1, 0)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0, 0.5f, 0), Vector3.Backward, new Vector2(0, 0)));

        SetupVb();
    }

}