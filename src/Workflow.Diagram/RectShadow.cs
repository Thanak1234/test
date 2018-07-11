using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Workflow.Diagram {
    public class RectShadow {
        public RectShadow() {
        }

        private static void AlphaBlend(Graphics g, Bitmap b, Rectangle dst) {
            float[][] newColorMatrix1 = new float[5][];
            float[][] numArray1 = newColorMatrix1;
            int index1 = 0;
            float[] numArray2 = new float[5];
            numArray2[0] = 1f;
            float[] numArray3 = numArray2;
            numArray1[index1] = numArray3;
            float[][] numArray4 = newColorMatrix1;
            int index2 = 1;
            float[] numArray5 = new float[5];
            numArray5[1] = 1f;
            float[] numArray6 = numArray5;
            numArray4[index2] = numArray6;
            float[][] numArray7 = newColorMatrix1;
            int index3 = 2;
            float[] numArray8 = new float[5];
            numArray8[2] = 1f;
            float[] numArray9 = numArray8;
            numArray7[index3] = numArray9;
            float[][] numArray10 = newColorMatrix1;
            int index4 = 3;
            float[] numArray11 = new float[5];
            numArray11[3] = 0.5f;
            float[] numArray12 = numArray11;
            numArray10[index4] = numArray12;
            float[][] numArray13 = newColorMatrix1;
            int index5 = 4;
            float[] numArray14 = new float[5];
            numArray14[4] = 1f;
            float[] numArray15 = numArray14;
            numArray13[index5] = numArray15;
            ColorMatrix newColorMatrix2 = new ColorMatrix(newColorMatrix1);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(newColorMatrix2, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            g.DrawImage((Image)b, dst, 0, 0, b.Width, b.Height, GraphicsUnit.Pixel, imageAttr);
        }

        private static void AlphaBlend2(Graphics g, Bitmap b, Rectangle dst) {
            float[][] newColorMatrix1 = new float[5][];
            float[][] numArray1 = newColorMatrix1;
            int index1 = 0;
            float[] numArray2 = new float[5];
            numArray2[0] = 1f;
            float[] numArray3 = numArray2;
            numArray1[index1] = numArray3;
            float[][] numArray4 = newColorMatrix1;
            int index2 = 1;
            float[] numArray5 = new float[5];
            numArray5[1] = 1f;
            float[] numArray6 = numArray5;
            numArray4[index2] = numArray6;
            float[][] numArray7 = newColorMatrix1;
            int index3 = 2;
            float[] numArray8 = new float[5];
            numArray8[2] = 1f;
            float[] numArray9 = numArray8;
            numArray7[index3] = numArray9;
            float[][] numArray10 = newColorMatrix1;
            int index4 = 3;
            float[] numArray11 = new float[5];
            numArray11[3] = 0.3f;
            float[] numArray12 = numArray11;
            numArray10[index4] = numArray12;
            float[][] numArray13 = newColorMatrix1;
            int index5 = 4;
            float[] numArray14 = new float[5];
            numArray14[4] = 1f;
            float[] numArray15 = numArray14;
            numArray13[index5] = numArray15;
            ColorMatrix newColorMatrix2 = new ColorMatrix(newColorMatrix1);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(newColorMatrix2, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            g.DrawImage((Image)b, dst, 0, 0, b.Width, b.Height, GraphicsUnit.Pixel, imageAttr);
        }

        private static void DrawHorShadeLine(Graphics g, Point point, int len) {
            Bitmap b = new Bitmap(len, 4);
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, len, 4);
            path.AddRectangle(rect);
            Graphics.FromImage((Image)b).FillPath((Brush)new PathGradientBrush(path) {
                CenterPoint = (PointF)new Point(0, 0),
                CenterColor = Color.FromArgb(128, 128, 128),
                FocusScales = new PointF(1f, 0.1f),
                SurroundColors = new Color[1]
              {
          Color.Transparent
              }
            }, path);
            RectShadow.AlphaBlend(g, b, new Rectangle(point.X, point.Y, len, 4));
        }

        private static void DrawVerShadeLine(Graphics g, Point point, int len) {
            Bitmap b = new Bitmap(4, len);
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, 4, len);
            path.AddRectangle(rect);
            Graphics.FromImage((Image)b).FillPath((Brush)new PathGradientBrush(path) {
                CenterPoint = (PointF)new Point(0, 0),
                CenterColor = Color.FromArgb(128, 128, 128),
                FocusScales = new PointF(0.1f, 1f),
                SurroundColors = new Color[1]
              {
          Color.Transparent
              }
            }, path);
            RectShadow.AlphaBlend(g, b, new Rectangle(point.X, point.Y, 4, len));
        }

        private static void DrawShadeCorner(Graphics g, Point point) {
            Bitmap b = new Bitmap(4, 4);
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, 4, 4);
            path.AddRectangle(rect);
            Graphics.FromImage((Image)b).FillPath((Brush)new PathGradientBrush(path) {
                CenterPoint = (PointF)new Point(0, 0),
                CenterColor = Color.FromArgb(128, 128, 128),
                FocusScales = new PointF(0.1f, 0.1f),
                SurroundColors = new Color[1]
              {
          Color.Transparent
              }
            }, path);
            RectShadow.AlphaBlend(g, b, new Rectangle(point.X, point.Y, 4, 4));
        }

        public static void DrawShade(Graphics g, Rectangle rect) {
        }
    }
}
