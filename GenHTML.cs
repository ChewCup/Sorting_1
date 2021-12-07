using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting_1
{
    class GenHTML
    {
        public class Scale
        {
            public double X, Y, xoff, yoff;
            public double x(double X) => this.X * X + xoff;
            public double y(double Y) => 400 - yoff - this.Y * Y;
        }
        public static void Head(StreamWriter writer, string title)
        {
            string[] head = {
                "<!DOCTYPE html>",
                "<html>",
                "  <head>",
                $"    <title>{title}</title>", // Ugly!
                "    <style type=\"text/css\">",
                "       body { background: #FFDDDD; }",
                "    </style>",
                "  </head>",
                "  <body>"
            };
            foreach (string s in head)
            {
                writer.WriteLine(s);
            }
        }
        public static void Foot(StreamWriter writer)
        {
            string[] foot =
            {
                "  </body>",
                "</html>"
            };
            foreach (string s in foot)
            {
                writer.WriteLine(s);
            }
        }
        public static void Write(StreamWriter writer, string str)
        {
            writer.WriteLine(str);
        }
        public static void PointAt(StreamWriter writer, Scale scale, string color, double x, double y)
        {
            string X = scale.x(x).ToString("0.##").Replace(',', '.');
            string Y = scale.y(y).ToString("0.##").Replace(',', '.');
            string[] point =
            {
                "    <ellipse",
                $"       style='opacity:1;fill:{color};fill-opacity:1;stroke:none;stroke-width:2;stroke-linecap:butt;stroke-linejoin:bevel;stroke-miterlimit:4.80000019;stroke-dasharray:none;stroke-dashoffset:0;stroke-opacity:1'",
                $"       cx='{X}'",
                $"       cy='{Y}'",
                "       rx='4'",
                "       ry='4' />",
            };
            foreach (string s in point)
            {
                writer.WriteLine(s);
            }
        }
        public static void SVGhead(StreamWriter writer)
        {
            string[] SVG_head =
            {
                "    <svg width='400' height='400'>",
                "        <rect",
                "           y='0' x='0'",
                "           height='400' width='400'",
                "           style='opacity:1; fill:#ffffff;fill-opacity:1;stroke:none;stroke-width:2;stroke-linecap:butt;stroke-linejoin:bevel;stroke-miterlimit:4;stroke-dasharray:none;stroke-dashoffset:0;stroke-opacity:1'/>",
                "        <path",
                "           d='m 30,30 v 330 l 330,0'",
                "           style='fill:none;stroke:#000000;stroke-width:2;stroke-linecap:butt;stroke-linejoin:miter;stroke-opacity:1;stroke-miterlimit:4;stroke-dasharray:none;'/>",
                "        <path",
                "           style='fill:#000000;stroke:none;stroke-width:0.26458332px;stroke-linecap:butt;stroke-linejoin:miter;stroke-opacity:1'",
                "           d='m 30,20 -5,13.5 10,0 z'",
                "           id='path1333'",
                "           inkscape:connector-curvature='0'",
                "           sodipodi:nodetypes='cccc' />",
                "        <path",
                "           style='fill:#000000;stroke:none;stroke-width:0.26458332px;stroke-linecap:butt;stroke-linejoin:miter;stroke-opacity:1'",
                "           d='M 370.12,359.6 356.58,354.75 V 364.51 Z'",
                "           id='path1333-4'",
                "           inkscape:connector-curvature='0'",
                "           sodipodi:nodetypes='cccc' />",
            };
            foreach (string s in SVG_head)
            {
                writer.WriteLine(s);
            }
            GenHTML.SVGtextAt(writer, "y", 20, 30, 15);
            GenHTML.SVGtextAt(writer, "x", 367, 375, 15);
        }
        public static void SVGfoot(StreamWriter writer)
        {
            string[] SVG_foot = {
                "    </svg>"
            };
            foreach (string s in SVG_foot)
            {
                writer.WriteLine(s);
            }
        }
        public static void SVGtextAt(StreamWriter writer, string text, double x, double y, int size = 10)
        {
            writer.WriteLine("    <text");
            writer.WriteLine("       xml:space='preserve'");
            writer.WriteLine($"       style='font-style:normal;font-weight:normal;font-size:{size}px;line-height:1.25;font-family:sans-serif;letter-spacing:0px;word-spacing:0px;fill:#000000;fill-opacity:1;stroke:none;stroke-width:0.26458332;font-stretch:normal;font-variant:normal;text-anchor:middle;text-align:center;writing-mode:lr;font-variant-ligatures:normal;font-variant-caps:normal;font-variant-numeric:normal;font-feature-settings:normal;'");
            writer.WriteLine($"       x='{x}' y='{y}'><tspan");
            writer.WriteLine($"         x='{x}' y='{y}'");
            writer.WriteLine($"         style='font-family:sans-serif;font-weight:normal;font-style:normal;font-stretch:normal;font-variant:normal;font-size:{size}px;text-anchor:middle;text-align:center;writing-mode:lr;font-variant-ligatures:normal;font-variant-caps:normal;font-variant-numeric:normal;font-feature-settings:normal;'>{text}</tspan></text>");
        }
        public static void SVGrotTextAt(StreamWriter writer, string text, double x, double y, int size = 10)
        {
            double X = y; double Y = -x;
            writer.WriteLine("    <text");
            writer.WriteLine("       xml:space='preserve'");
            writer.WriteLine($"       style='font-style:normal;font-weight:normal;font-size:{size}px;line-height:1.25;font-family:sans-serif;letter-spacing:0px;word-spacing:0px;fill:#000000;fill-opacity:1;stroke:none;stroke-width:0.26458332;font-stretch:normal;font-variant:normal;text-anchor:start;text-align:left;writing-mode:lr;font-variant-ligatures:normal;font-variant-caps:normal;font-variant-numeric:normal;font-feature-settings:normal;'");
            writer.WriteLine($"       x='{X}' y='{Y}' transform='rotate(90)'><tspan");
            writer.WriteLine($"         x='{X}' y='{Y}'");
            writer.WriteLine($"         style='font-family:sans-serif;font-weight:normal;font-style:normal;font-stretch:normal;font-variant:normal;font-size:{size}px;text-anchor:start;text-align:left;writing-mode:lr;font-variant-ligatures:normal;font-variant-caps:normal;font-variant-numeric:normal;font-feature-settings:normal;'>{text}</tspan></text>");
        }
    }
}
