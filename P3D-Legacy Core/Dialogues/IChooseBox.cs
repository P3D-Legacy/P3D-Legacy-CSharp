using System;

using Microsoft.Xna.Framework;

using P3D.Legacy.Core.Data;
using P3D.Legacy.Core.Entities;

namespace P3D.Legacy.Core.Dialogues
{
    public interface IChooseBox
    {
        bool Showing { get; set; }

        FontContainer TextFont { get; set; }

        int Index { get; set; }

        string[] Options { get; set; }

        bool ReadyForResult { get; set; }

        int Result { get; set; }


        void Update();
        void Update(bool raiseClickEvent);

        void Draw(Vector2 position);
        void Draw();

        void Show(string[] options, Action<int> doSubs);
        void Show(string[] options, int id, bool actionScript);
        void Show(string[] options, int id, Entity[] updateEntities);

        int GetResult(int id);
    }
}