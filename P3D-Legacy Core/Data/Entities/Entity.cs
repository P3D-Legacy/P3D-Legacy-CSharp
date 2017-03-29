using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Battle.BattleSystemV2;
using P3D.Legacy.Core.Debug;
using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Models;
using P3D.Legacy.Core.Screens;
using P3D.Legacy.Core.World;

namespace P3D.Legacy.Core.Entities
{
    public class Entity
    {
        public abstract class EntityManager
        {
            public abstract Entity GetNewEntity(string EntityID, Vector3 Position, Texture2D[] Textures,
                int[] TextureIndex, bool Collision, Vector3 Rotation, Vector3 Scale, BaseModel Model, int ActionValue,
                string AdditionalValue,
                bool Visible, Vector3 Shader, int ID, string MapOrigin, string SeasonColorTexture, Vector3 Offset,
                object[] Params = null, float Opacity = 1f);
        }

        private static EntityManager _em;
        protected static EntityManager EM
        {
            get
            {
                if (_em == null)
                {
                    var assembly = Assembly.GetEntryAssembly();
                    var type = assembly.GetTypes().SingleOrDefault(t => t.IsSubclassOf(typeof(EntityManager)));
                    if (type != null)
                        _em = Activator.CreateInstance(type) as EntityManager;
                }

                return _em;
            }
            set { _em = value; }
        }
        public static Entity GetNewEntity(string EntityID, Vector3 Position, Texture2D[] Textures, int[] TextureIndex, bool Collision, Vector3 Rotation, Vector3 Scale, BaseModel Model, int ActionValue, string AdditionalValue,
            bool Visible, Vector3 Shader, int ID, string MapOrigin, string SeasonColorTexture, Vector3 Offset, object[] Params = null, float Opacity = 1f)
        {
            return EM.GetNewEntity(EntityID, Position, Textures, TextureIndex, Collision, Rotation, Scale, Model, ActionValue, AdditionalValue, Visible, Shader, ID, MapOrigin, SeasonColorTexture, Offset, Params, Opacity);
        }



        public static bool MakeShake = false;
        public static bool DrawViewBox = false;

        private static RasterizerState _newRasterizerState;
        private static RasterizerState _oldRasterizerState;

        public int ID = -1;
        public string EntityID = "";
        public string MapOrigin = "";
        public bool IsOffsetMapContent = false;
        public Vector3 Offset = new Vector3(0);
        public Vector3 Position;
        public Vector3 Rotation = new Vector3(0);
        public Vector3 Scale = new Vector3(1);
        public Texture2D[] Textures;
        public int[] TextureIndex;
        public int ActionValue;
        public string AdditionalValue;
        public BaseModel Model;
        public bool Visible = true;
        public Vector3 Shader = new Vector3(1f);

        public List<Vector3> Shaders = new List<Vector3>();

        public string SeasonColorTexture = "";
        public int FaceDirection = 0;
        public float Moved = 0f;
        public float Speed = 0.04f;

        public bool CanMove = false;

        public float Opacity = 1f;
        private float _normalOpactity = 1f;
        public float NormalOpacity
        {
            get { return _normalOpactity; }
            set
            {
                Opacity = value;
                _normalOpactity = value;
            }
        }

        public Vector3 BoundingBoxScale = new Vector3(1.25f);

        public BoundingBox BoundingBox;
        public BoundingBox ViewBox;

        public Vector3 ViewBoxScale = new Vector3(1f);
        public float CameraDistance;
        public Matrix World;
        public bool CreatedWorld = false;

        public bool CreateWorldEveryFrame = false;

        public bool Collision = true;
        public bool CanBeRemoved = false;

        public bool NeedsUpdate = false;

        private Vector3 _boundingPositionCreated = new Vector3(1110);
        private Vector3 _boundingRotationCreated = new Vector3(-1);

        public int HasEqualTextures = -1;
        private bool _drawnLastFrame = true;

        protected bool DropUpdateUnlessDrawn = true;

        public Entity() { }

        public Entity(float X, float Y, float Z, string EntityID, Texture2D[] Textures, int[] TextureIndex, bool Collision, int Rotation, Vector3 Scale, BaseModel Model, int ActionValue, string AdditionalValue, Vector3 Shader)
        {

            Position = new Vector3(X, Y, Z);
            this.EntityID = EntityID;
            this.Textures = Textures;
            this.TextureIndex = TextureIndex;
            this.Collision = Collision;
            this.Rotation = GetRotationFromInteger(Rotation);
            this.Scale = Scale;
            this.Model = Model;
            this.ActionValue = ActionValue;
            this.AdditionalValue = AdditionalValue;
            this.Shader = Shader;

            Initialize();
        }

        public virtual void Initialize()
        {
            if (GetRotationFromVector(Rotation) % 2 == 1)
            {
                ViewBox = new BoundingBox(Vector3.Transform(new Vector3(-(Scale.Z / 2), -(Scale.Y / 2), -(Scale.X / 2)), Matrix.CreateScale(ViewBoxScale) * Matrix.CreateTranslation(Position)), Vector3.Transform(new Vector3((Scale.Z / 2), (Scale.Y / 2), (Scale.X / 2)), Matrix.CreateScale(ViewBoxScale) * Matrix.CreateTranslation(Position)));
            }
            else
            {
                ViewBox = new BoundingBox(Vector3.Transform(new Vector3(-(Scale.X / 2), -(Scale.Y / 2), -(Scale.Z / 2)), Matrix.CreateScale(ViewBoxScale) * Matrix.CreateTranslation(Position)), Vector3.Transform(new Vector3((Scale.X / 2), (Scale.Y / 2), (Scale.Z / 2)), Matrix.CreateScale(ViewBoxScale) * Matrix.CreateTranslation(Position)));
            }

            BoundingBox = new BoundingBox(Vector3.Transform(new Vector3(-0.5f), Matrix.CreateScale(BoundingBoxScale) * Matrix.CreateTranslation(Position)), Vector3.Transform(new Vector3(0.5f), Matrix.CreateScale(BoundingBoxScale) * Matrix.CreateTranslation(Position)));

            _boundingPositionCreated = Position;
            _boundingRotationCreated = Rotation;

            if (_newRasterizerState == null)
            {
                _newRasterizerState = new RasterizerState();
                _oldRasterizerState = new RasterizerState();

                _newRasterizerState.CullMode = CullMode.None;
                _oldRasterizerState.CullMode = CullMode.CullCounterClockwiseFace;
            }

            LoadSeasonTextures();

            UpdateEntity();
        }

        public static void SetProperties(ref Entity newEnt, Entity PropertiesEnt)
        {
            newEnt.EntityID = PropertiesEnt.EntityID;
            newEnt.Position = PropertiesEnt.Position;
            newEnt.Textures = PropertiesEnt.Textures;
            newEnt.TextureIndex = PropertiesEnt.TextureIndex;
            newEnt.Collision = PropertiesEnt.Collision;
            newEnt.Rotation = PropertiesEnt.Rotation;
            newEnt.Scale = PropertiesEnt.Scale;
            newEnt.Model = PropertiesEnt.Model;
            newEnt.ActionValue = PropertiesEnt.ActionValue;
            newEnt.AdditionalValue = PropertiesEnt.AdditionalValue;
            newEnt.Visible = PropertiesEnt.Visible;
            newEnt.Shader = PropertiesEnt.Shader;
            newEnt.ID = PropertiesEnt.ID;
            newEnt.MapOrigin = PropertiesEnt.MapOrigin;
            newEnt.SeasonColorTexture = PropertiesEnt.SeasonColorTexture;
            newEnt.Offset = PropertiesEnt.Offset;
            newEnt.NormalOpacity = PropertiesEnt.Opacity;
        }

        public static Vector3 GetRotationFromInteger(int i)
        {
            switch (i)
            {
                case 0:
                    return new Vector3(0, 0, 0);
                case 1:
                    return new Vector3(0, MathHelper.PiOver2, 0);
                case 2:
                    return new Vector3(0, MathHelper.Pi, 0);
                case 3:
                    return new Vector3(0, MathHelper.Pi * 1.5f, 0);
            }

            return Vector3.Zero;
        }

        public static int GetRotationFromVector(Vector3 v)
        {
            if (v.Y == 0)
                return 0;
            if (v.Y == MathHelper.PiOver2)
                return 1;
            if (v.Y == MathHelper.Pi)
                return 2;
            if (v.Y == MathHelper.Pi * 1.5f)
                return 3;

            return 0;
        }

        public void LoadSeasonTextures()
        {
            if (!string.IsNullOrEmpty(SeasonColorTexture))
                Textures = Textures.Select(t => BaseWorld.GetSeasonTexture(TextureManager.GetTexture(Path.Combine("Textures", "Seasons", SeasonColorTexture)), t)).ToArray();
        }


        public virtual void Update()
        {
        }

        public virtual void OpacityCheck()
        {

            if (CameraDistance > 10f || (Screen.Level.OwnPlayer != null && CameraDistance > Screen.Level.OwnPlayer.CameraDistance))
            {
                Opacity = _normalOpactity;
                return;
            }

            string[] notNames =
            {
                "Floor",
                "OwnPlayer",
                "Water",
                "Whirlpool",
                "Particle",
                "OverworldPokemon",
                "ItemObject",
                "NetworkPokemon",
                "NetworkPlayer"
            };

            if (Screen.Camera.Name == "Overworld" && !notNames.Contains(EntityID))
            {
                Opacity = _normalOpactity;
                if (((BaseOverworldCamera)Screen.Camera).ThirdPerson)
                {
                    Ray Ray = Screen.Camera.Ray;
                    float? result = Ray.Intersects(BoundingBox);
                    if (result.HasValue == true)
                    {
                        if (result.Value < 0.3f + (((BaseOverworldCamera)Screen.Camera).ThirdPersonOffset.Z - 1.5f))
                        {
                            Opacity = _normalOpactity - 0.5f;
                            if (Opacity < 0.3f)
                            {
                                Opacity = 0.3f;
                            }
                        }
                    }
                }
            }
        }

        protected virtual Vector3 GetCameraDistanceCenterPoint() => Position + GetCenter();

        protected virtual float CalculateCameraDistance(Vector3 CPosition) => Vector3.Distance(GetCameraDistanceCenterPoint(), CPosition);

        public virtual void UpdateEntity()
        {
            Vector3 CPosition = Screen.Camera.Position;
            bool ActionScriptActive = false;

            //lock (Screen.Camera)
            {
                if (Core.CurrentScreen != null)
                {
                    if (Screen.Camera.Name == "Overworld")
                    {
                        CPosition = ((BaseOverworldCamera)Screen.Camera).CPosition;
                    }
                    if (Screen.Camera.Name == "BattleV2")
                    {
                        CPosition = ((BattleCamera)Screen.Camera).CPosition;
                    }
                    if (Core.CurrentScreen.Identification == Screen.Identifications.OverworldScreen)
                    {
                        // TODO
                        //ActionScriptActive = !((BaseOverworldScreen)Core.CurrentScreen).ActionScript.IsReady;
                        ActionScriptActive = !((BaseOverworldScreen)Core.CurrentScreen).ActionScriptIsReady;
                    }
                }
            }


            CameraDistance = CalculateCameraDistance(CPosition);

            if (DropUpdateUnlessDrawn == true && _drawnLastFrame == false && Visible == true && ActionScriptActive == false)
            {
                return;
            }


            if (Moved > 0f && CanMove == true)
            {
                Moved -= Speed;

                Vector3 movement = Vector3.Zero;
                switch (FaceDirection)
                {
                    case 0:
                        movement = new Vector3(0, 0, -1);
                        break;
                    case 1:
                        movement = new Vector3(-1, 0, 0);
                        break;
                    case 2:
                        movement = new Vector3(0, 0, 1);
                        break;
                    case 3:
                        movement = new Vector3(1, 0, 0);
                        break;
                }

                movement *= Speed;
                Position += movement;
                CreatedWorld = false;

                if (Moved <= 0f)
                {
                    Moved = 0f;

                    Position.X = Convert.ToInt32(Position.X);
                    Position.Z = Convert.ToInt32(Position.Z);
                }
            }

            if (IsOffsetMapContent == false)
            {
                OpacityCheck();
            }

            if (CreatedWorld == false || CreateWorldEveryFrame == true)
            {
                World = Matrix.CreateScale(Scale) * Matrix.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z) * Matrix.CreateTranslation(Position);
                CreatedWorld = true;
            }

            if (CameraDistance < Screen.Camera.FarPlane * 2)
            {
                if (Position != _boundingPositionCreated)
                {
                    List<float> diff = new List<float>();
                    diff.AddRange(new [] { _boundingPositionCreated.X - Position.X, _boundingPositionCreated.Y - Position.Y, _boundingPositionCreated.Z - Position.Z });

                    ViewBox.Min.X -= diff[0];
                    ViewBox.Min.Y -= diff[1];
                    ViewBox.Min.Z -= diff[2];

                    ViewBox.Max.X -= diff[0];
                    ViewBox.Max.Y -= diff[1];
                    ViewBox.Max.Z -= diff[2];

                    BoundingBox.Min.X -= diff[0];
                    BoundingBox.Min.Y -= diff[1];
                    BoundingBox.Min.Z -= diff[2];

                    BoundingBox.Max.X -= diff[0];
                    BoundingBox.Max.Y -= diff[1];
                    BoundingBox.Max.Z -= diff[2];

                    _boundingPositionCreated = Position;
                }
            }

            if (MakeShake == true)
            {
                if (Core.Random.Next(0, 1) == 0)
                {
                    Rotation.X += Convert.ToSingle((Core.Random.Next(1, 6) - 3) / 100);
                    Rotation.Z += Convert.ToSingle((Core.Random.Next(1, 6) - 3) / 100);
                    Rotation.Y += Convert.ToSingle((Core.Random.Next(1, 6) - 3) / 100);

                    Position.X += Convert.ToSingle((Core.Random.Next(1, 6) - 3) / 100);
                    Position.Z += Convert.ToSingle((Core.Random.Next(1, 6) - 3) / 100);
                    Position.Y += Convert.ToSingle((Core.Random.Next(1, 6) - 3) / 100);

                    Scale.X += Convert.ToSingle((Core.Random.Next(1, 6) - 3) / 100);
                    Scale.Z += Convert.ToSingle((Core.Random.Next(1, 6) - 3) / 100);
                    Scale.Y += Convert.ToSingle((Core.Random.Next(1, 6) - 3) / 100);

                    CreatedWorld = false;
                }
            }

            if ((Screen.Level.World != null))
            {
                switch (Screen.Level.World.EnvironmentType)
                {
                    case EnvironmentTypeEnum.Outside:
                        // TODO
                        //Shader = SkyDome.GetDaytimeColor(true).ToVector3();
                        break;
                    case EnvironmentTypeEnum.Dark:
                        Shader = new Vector3(0.5f, 0.5f, 0.6f);
                        break;
                    default:
                        Shader = new Vector3(1f);
                        break;
                }
            }

            foreach (Vector3 s in Shaders)
            {
                Shader *= s;
            }
        }

        Vector3 tempCenterVector = Vector3.Zero;
        /// <summary>
        /// Returns the offset from the 0,0,0 center of the position of the entity.
        /// </summary>
        private Vector3 GetCenter()
        {
            if (CreatedWorld == false || CreateWorldEveryFrame == true)
            {
                Vector3 v = Vector3.Zero;
                //(Me.ViewBox.Min - Me.Position) + (Me.ViewBox.Max - Me.Position)

                if ((Model != null))
                {
                    switch (Model.ID)
                    {
                        case 0:
                        case 9:
                        case 10:
                        case 11:
                            v.Y -= 0.5f;
                            break;
                    }
                }
                tempCenterVector = v;
            }

            return tempCenterVector;
        }

        public void Draw(BasicEffect effect, BaseModel Model, Texture2D[] Textures, bool setRasterizerState)
        {
            if (Visible)
            {
                if (IsInFieldOfView())
                {
                    if (setRasterizerState)
                    {
                        Core.GraphicsDevice.RasterizerState = _newRasterizerState;
                    }

                    Model.Draw(effect, this, Textures);

                    if (setRasterizerState)
                    {
                        Core.GraphicsDevice.RasterizerState = _oldRasterizerState;
                    }

                    _drawnLastFrame = true;

                    //if (EntityID != "Floor" && EntityID != "Water")
                    //{
                    //    if (DrawViewBox == true)
                    //    {
                    //        BoundingBoxRenderer.Render(ViewBox, Core.GraphicsDevice, Screen.Camera.View, Screen.Camera.Projection, Color.LightCoral);
                    //    }
                    //}
                }
                else
                {
                    _drawnLastFrame = false;
                }
            }
            else
            {
                _drawnLastFrame = false;
            }
        }


        public virtual void Render(BasicEffect effect)
        {
        }


        public virtual void ClickFunction()
        {
        }

        public virtual bool WalkAgainstFunction()
        {
            return true;
        }

        public virtual bool WalkIntoFunction()
        {
            return false;
        }


        public virtual void WalkOntoFunction()
        {
        }


        public virtual void ResultFunction(int Result)
        {
        }

        public virtual bool LetPlayerMove()
        {
            return true;
        }

        public bool _visibleLastFrame = false;

        public bool _occluded = false;
        public bool IsInFieldOfView()
        {
            if (Screen.Camera.BoundingFrustum.FastIntersect(ViewBox))
            {
                _visibleLastFrame = true;
                return true;
            }
            else
            {
                _visibleLastFrame = false;
                return false;
            }
            return false;

            // TODO
            //if (Screen.Camera.BoundingFrustum.Contains(ViewBox) != ContainmentType.Disjoint)
            //{
            //    _visibleLastFrame = true;
            //    return true;
            //}
            //else
            //{
            //    _visibleLastFrame = false;
            //    return false;
            //}
        }

        //Stores the vertex count so it doesnt need to be recalculated.
        int _cachedVertexCount = -1;

        public int VertexCount
        {
            get
            {
                if (_cachedVertexCount == -1)
                {
                    if ((Model != null))
                    {
                        int c = Convert.ToInt32(Model.VertexBuffer.VertexCount / 3);
                        int min = 0;

                        for (var i = 0; i <= TextureIndex.Length - 1; i++)
                        {
                            if (i <= c - 1)
                            {
                                if (TextureIndex[i] > -1)
                                {
                                    min += 1;
                                }
                            }
                        }

                        _cachedVertexCount = min;
                    }
                    else
                    {
                        _cachedVertexCount = 0;
                    }
                }
                return _cachedVertexCount;
            }
        }

        protected Entity GetEntity(List<Entity> List, Vector3 Position, bool IntComparison, Type[] validEntitytypes)
        {
            foreach (var e in List.Where(selEnt => validEntitytypes.Contains(selEnt.GetType()))) {
                if (IntComparison)
                {
                    if ((int) e.Position.X == (int) Position.X && (int)e.Position.Y == (int)Position.Y && (int)e.Position.Z == (int)Position.Z)
                    {
                        return e;
                    }
                }
                else
                {
                    if (e.Position.X == Position.X && e.Position.Y == Position.Y && e.Position.Z == Position.Z)
                    {
                        return e;
                    }
                }
            }
            return null;
        }
    }
}
