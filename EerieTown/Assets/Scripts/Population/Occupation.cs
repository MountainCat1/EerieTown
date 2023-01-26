using System;
using System.Collections.Generic;
using UnityEngine;

namespace Population
{
    public abstract class Occupation : ScriptableObject
    {
        public string Name => GetType().Name;

        public void ActAll(IEnumerable<Population> populations)
        {
            foreach (var population in populations)
            {
                Act(population);
            }
        }
        protected abstract void Act(Population population);
        public virtual void Initialize(){}
    }
}