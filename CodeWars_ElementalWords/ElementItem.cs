using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars_ElementalWords
{
    public class ElementItem : IEquatable<ElementItem>
    {
        public string Name { get; set; }
        public int Code { get; set; }

        public bool Equals(ElementItem other)
        {
            if (other is null)
                return false;

            return this.Name == other.Name && this.Code == other.Code;
        }

        public override bool Equals(object obj) => Equals(obj as ElementItem);
        public override int GetHashCode() => (Name, Code).GetHashCode();

        public int Level;
        public string? Text;

    }
}