using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    internal class File
    {
        private string name, path;

        public string Name { get { return name; } }
        public string Path { get { return path; } }

        public File(string name, string path)
        {
            this.name = name;
            this.path = path;
        }

        public override string ToString() { return name; }
    }
}
