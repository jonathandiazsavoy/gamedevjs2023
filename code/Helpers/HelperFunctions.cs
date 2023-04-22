using Godot;
using System;

namespace code.Helpers
{
    // Just keep misc usful functions in here for later
    public static class HelperFunctions
    {
        public static void PrintChildrenNames(Node node)
        {
            foreach (Node child in node.GetChildren())
            {
                GD.Print(node.Name + " children: " + child.Name);
            }
        }

        public static Node GetChildByType(Node node, Type nodeType)
        {
            foreach (Node child in node.GetChildren())
            {
                if (child.GetType() == nodeType) return child;
            }
            return null;
        }
    }
}
