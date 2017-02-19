using System;
using System.Linq;
using System.Reflection;

namespace P3D.Legacy.Core.Pokemon
{
    public abstract class BaseAbility
    {
        public abstract class AbilityManager
        {
            public abstract BaseAbility GetAbilityByID(int ID);
        }

        private static AbilityManager _am;
        protected static AbilityManager AM
        {
            get
            {
                if (_am == null)
                {
                    var assembly = Assembly.GetEntryAssembly();
                    var type = assembly.GetTypes().SingleOrDefault(t => t.IsSubclassOf(typeof(AbilityManager)));
                    if (type != null)
                        _am = Activator.CreateInstance(type) as AbilityManager;
                }

                return _am;
            }
            set { _am = value; }
        }
        public static BaseAbility GetAbilityByID(int ID) => AM.GetAbilityByID(ID);



        public int Id { get; }
        public string Name { get; }
        public string Description { get; }

        public BaseAbility(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public virtual void EndBattle(BasePokemon parentPokemon) { }
        public virtual void SwitchOut(BasePokemon parentPokemon) { }
    }
}
