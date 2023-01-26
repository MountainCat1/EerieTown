using System;
using System.Collections.Generic;
using UnityEngine;

namespace Population
{
    public abstract class Occupation : ScriptableObject
    {
        public string Name => GetType().Name;
        public List<Population> Populations { get; } = new List<Population>();
        
        public void ActAll()
        {
            foreach (var population in Populations)
            {
                Act(population);
            }
        }
        protected abstract void Act(Population population);
    }
}