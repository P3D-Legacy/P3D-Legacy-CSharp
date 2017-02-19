using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using P3D.Legacy.Core.Resources.Models;

public class StepModel : BaseModel
{

    public StepModel()
    {
        vertexData.Clear();

        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0, 0, 1), new Vector2(0.0f, 1.0f)));
        //h
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.25f, 0.5f), new Vector3(0, 0, 1), new Vector2(0.0f, 0.0f)));
        //e
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0, 0, 1), new Vector2(1.0f, 1.0f)));
        //c

        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0, 0, 1), new Vector2(1.0f, 1.0f)));
        //c
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.25f, 0.5f), new Vector3(0, 0, 1), new Vector2(0.0f, 0.0f)));
        //e
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.25f, 0.5f), new Vector3(0, 0, 1), new Vector2(1.0f, 0.0f)));
        //d

        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(1, 0, 0), new Vector2(0.0f, 1.0f)));
        //c
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.25f, 0.5f), new Vector3(1, 0, 0), new Vector2(0.0f, 0.0f)));
        //d
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(1, 0, 0), new Vector2(1.0f, 1.0f)));
        //b

        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.25f, 0.5f), new Vector3(-1, 0, 0), new Vector2(1.0f, 0.0f)));
        //e
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(-1, 0, 0), new Vector2(1.0f, 1.0f)));
        //h
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-1, 0, 0), new Vector2(0.0f, 1.0f)));
        //a

        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.25f, 0.5f), new Vector3(0, 0, -1), new Vector2(1, 0)));
        //e
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(1, 1)));
        //a
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(0, 1)));
        //b

        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(0, 1)));
        //b
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.25f, 0.5f), new Vector3(0, 0, -1), new Vector2(0, 0)));
        //d
        vertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.25f, 0.5f), new Vector3(0, 0, -1), new Vector2(1, 0)));
        //e

        ID = 7;

        Setup();
    }

}