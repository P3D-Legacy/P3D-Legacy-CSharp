using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Resources.Models;

public class BillModel : BaseModel
{
    public BillModel()
    {
        vertexData.Clear();

        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0), Vector3.Backward, new Vector2(0, 1)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0), Vector3.Backward, new Vector2(0, 0)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0), Vector3.Backward, new Vector2(1, 0)));

        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0), Vector3.Backward, new Vector2(1, 0)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0), Vector3.Backward, new Vector2(1, 1)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0), Vector3.Backward, new Vector2(0, 1)));

        ID = 3;

        Setup();
    }
}