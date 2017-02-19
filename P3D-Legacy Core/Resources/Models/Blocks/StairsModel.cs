using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using P3D.Legacy.Core.Resources.Models;

public class StairsModel : BaseModel
{

    public StairsModel()
    {
        vertexData.Clear();

        //Lower stair, front:
        //left vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0.5f), new Vector3(0, 0, 1), new Vector2(0.0f, 0.5f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0.5f), new Vector3(0, 0, 1), new Vector2(1.0f, 0.5f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0, 0, 1), new Vector2(0.0f, 1.0f)));
        //right vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0.5f), new Vector3(0, 0, 1), new Vector2(1.0f, 0.5f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0, 0, 1), new Vector2(1.0f, 1.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0, 0, 1), new Vector2(0.0f, 1.0f)));

        //Lower stair, top:
        //Left vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0), new Vector3(0, 1, 0), new Vector2(0.0f, 0.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0), new Vector3(0, 1, 0), new Vector2(1.0f, 0.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0.5f), new Vector3(0, 1, 0), new Vector2(0.0f, 0.5f)));
        //Right vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0), new Vector3(0, 1, 0), new Vector2(1.0f, 0.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0.5f), new Vector3(0, 1, 0), new Vector2(1.0f, 0.5f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0.5f), new Vector3(0, 1, 0), new Vector2(0.0f, 0.5f)));

        //Upper stair, front:
        //left vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0), new Vector3(0, 0, 1), new Vector2(0.0f, 0.5f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0), new Vector3(0, 0, 1), new Vector2(1.0f, 0.5f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0), new Vector3(0, 0, 1), new Vector2(0.0f, 1.0f)));
        //right vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0), new Vector3(0, 0, 1), new Vector2(1.0f, 0.5f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0), new Vector3(0, 0, 1), new Vector2(1.0f, 1.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0), new Vector3(0, 0, 1), new Vector2(0.0f, 1.0f)));

        //Upper stair, top:
        //Left vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0, 1, 0), new Vector2(0.0f, 0.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(0, 1, 0), new Vector2(1.0f, 0.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0), new Vector3(0, 1, 0), new Vector2(0.0f, 0.5f)));
        //Right vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(0, 1, 0), new Vector2(1.0f, 0.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0), new Vector3(0, 1, 0), new Vector2(1.0f, 0.5f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0), new Vector3(0, 1, 0), new Vector2(0.0f, 0.5f)));

        //Back:
        //Left vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(0.0f, 0.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(1.0f, 0.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(0.0f, 1.0f)));
        //Right vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(1.0f, 0.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(1.0f, 1.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(0.0f, 1.0f)));

        //Left side, lower:
        //Left vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, -0.5f), new Vector3(-1, 0, 0), new Vector2(0.0f, 0.5f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0.5f), new Vector3(-1, 0, 0), new Vector2(1.0f, 0.5f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-1, 0, 0), new Vector2(0.0f, 1.0f)));
        //Right vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0.5f), new Vector3(-1, 0, 0), new Vector2(1.0f, 0.5f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(-1, 0, 0), new Vector2(1.0f, 1.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-1, 0, 0), new Vector2(0.0f, 1.0f)));

        //Left side, upper:
        //Left vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(-1, 0, 0), new Vector2(0.0f, 0.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0), new Vector3(-1, 0, 0), new Vector2(0.5f, 0.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, -0.5f), new Vector3(-1, 0, 0), new Vector2(0.0f, 0.5f)));
        //Right vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0), new Vector3(-1, 0, 0), new Vector2(0.5f, 0.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0), new Vector3(-1, 0, 0), new Vector2(0.5f, 0.5f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, -0.5f), new Vector3(-1, 0, 0), new Vector2(0.0f, 0.5f)));

        //Right side, lower:
        //Left vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0.5f), new Vector3(1, 0, 0), new Vector2(0.0f, 0.5f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, -0.5f), new Vector3(1, 0, 0), new Vector2(1.0f, 0.5f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(1, 0, 0), new Vector2(0.0f, 1.0f)));
        //Right vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, -0.5f), new Vector3(1, 0, 0), new Vector2(1.0f, 0.5f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(1, 0, 0), new Vector2(1.0f, 1.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(1, 0, 0), new Vector2(0.0f, 1.0f)));

        //Right side, upper:
        //Left vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0), new Vector3(1, 0, 0), new Vector2(0.5f, 0.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(1, 0, 0), new Vector2(1.0f, 0.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0), new Vector3(1, 0, 0), new Vector2(0.5f, 0.5f)));
        //Right vertex:
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(1, 0, 0), new Vector2(1.0f, 0.0f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, -0.5f), new Vector3(1, 0, 0), new Vector2(1.0f, 0.5f)));
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0), new Vector3(1, 0, 0), new Vector2(0.5f, 0.5f)));

        ID = 16;

        Setup();
    }

}