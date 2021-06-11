using System.Drawing;

namespace ProjectManager
{
    // Wrap reference of graph in a separate object, context. This makes it simpler to change the same reference in multiple classes.
    public class Context
    {
        public Graphics graphics;
        // add more objects if needed. for now, just want to try this with graph

        public Context(Graphics graph)
        { this.graphics = graph; }
    }
}
