using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Workflow.Diagram.views {
    public class PointView {
        public double X { get; set; }
        public double Y { get; set; }
        public static PointView From(Point p) {
            return new PointView() { X = p.X, Y = p.Y };
        }
     }
}
