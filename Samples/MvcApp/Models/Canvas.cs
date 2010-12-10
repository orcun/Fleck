using System;

namespace MvcApp.Models
{
    public class Canvas
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}