﻿using System.Collections.Generic;

namespace Composite
{
    public class CompositeGraphic : IGraphic
    {
        //Collection of Graphics.
        private readonly List<IGraphic> graphics;

        //Constructor 
        public CompositeGraphic()
        {
            //initialize generic Collection(Composition)
            graphics = new List<IGraphic>();
        }

        //Adds the graphic to the composition
        public void Add(IGraphic graphic)
        {
            graphics.Add(graphic);
        }

        //Adds multiple graphics to the composition
        public void AddRange(params IGraphic[] graphic)
        {
            graphics.AddRange(graphic);
        }

        //Removes the graphic from the composition
        public void Delete(IGraphic graphic)
        {
            graphics.Remove(graphic);
        }

        //Prints the graphic.
        public void Print()
        {
            foreach (var childGraphic in graphics)
            {
                childGraphic.Print();
            }
        }
    }
}
