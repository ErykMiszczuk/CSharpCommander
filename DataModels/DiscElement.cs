using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCommander.DataModels
{
    public abstract class DiscElement : IComparable<DiscElement>
    {
        string path;
        public DiscElement(string path)
        {
            this.path = path;
        }

        public string Path
        {
            get
            {
                return path;
            }
        }

        public abstract DateTime CreationTime
        {
            get;
        }

        public abstract string Name
        {
            get;
        }

        public abstract string GetDescription
        {
            get;
        }

        public int CompareTo(DiscElement other)
        {
            return string.Compare(System.IO.Path.GetFileNameWithoutExtension(this.Path), System.IO.Path.GetFileNameWithoutExtension(other.Path));
        }
    }
}
