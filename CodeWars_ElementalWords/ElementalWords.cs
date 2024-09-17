using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars_ElementalWords
{
    public class ElementalWords
    {
        static Dictionary<string, string> ELEMENTS = new Dictionary<string, string>(
            StringComparer.InvariantCultureIgnoreCase)
        {
            {"Ac","Actinium"},
            {"Ag","Silver"},
            {"Al","Aluminium"},
            {"Am","Americium"},
            {"Ar","Argon"},
            {"As","Arsenic"},
            {"At","Astatine"},
            {"Au","Gold"},
            {"B","Boron"},
            {"Ba","Barium"},
            {"Be","Beryllium"},
            {"Bh","Bohrium"},
            {"Bi","Bismuth"},
            {"Bk","Berkelium"},
            {"Br","Bromine"},
            {"C","Carbon"},
            {"Ca","Calcium"},
            {"Cd","Cadmium"},
            {"Ce","Cerium"},
            {"Cf","Californium"},
            {"Cl","Chlorine"},
            {"Cm","Curium"},
            {"Cn","Copernicium"},
            {"Co","Cobalt"},
            {"Cr","Chromium"},
            {"Cs","Cesium"},
            {"Cu","Copper"},
            {"Db","Dubnium"},
            {"Ds","Darmstadtium"},
            {"Dy","Dysprosium"},
            {"Er","Erbium"},
            {"Es","Einsteinium"},
            {"Eu","Europium"},
            {"F","Fluorine"},
            {"Fe","Iron"},
            {"Fl","Flerovium"},
            {"Fm","Fermium"},
            {"Fr","Francium"},
            {"Ga","Gallium"},
            {"Gd","Gadolinium"},
            {"Ge","Germanium"},
            {"H","Hydrogen"},
            {"He","Helium"},
            {"Hf","Hafnium"},
            {"Hg","Mercury"},
            {"Ho","Holmium"},
            {"Hs","Hassium"},
            {"I","Iodine"},
            {"In","Indium"},
            {"Ir","Iridium"},
            {"K","Potassium"},
            {"Kr","Krypton"},
            {"La","Lanthanum"},
            {"Li","Lithium"},
            {"Lr","Lawrencium"},
            {"Lu","Lutetium"},
            {"Lv","Livermorium"},
            {"Mc","Moscovium"},
            {"Md","Mendelevium"},
            {"Mg","Magnesium"},
            {"Mn","Manganese"},
            {"Mo","Molybdenum"},
            {"Mt","Meitnerium"},
            {"N","Nitrogen"},
            {"Na","Sodium"},
            {"Nb","Niobium"},
            {"Nd","Neodymium"},
            {"Ne","Neon"},
            {"Nh","Nihonium"},
            {"Ni","Nickel"},
            {"No","Nobelium"},
            {"Np","Neptunium"},
            {"O","Oxygen"},
            {"Og","Oganesson"},
            {"Os","Osmium"},
            {"P","Phosphorus"},
            {"Pa","Protactinium"},
            {"Pb","Lead"},
            {"Pd","Palladium"},
            {"Pm","Promethium"},
            {"Po","Polonium"},
            {"Pr","Praseodymium"},
            {"Pt","Platinum"},
            {"Pu","Plutonium"},
            {"Ra","Radium"},
            {"Rb","Rubidium"},
            {"Re","Rhenium"},
            {"Rf","Rutherfordium"},
            {"Rg","Roentgenium"},
            {"Rh","Rhodium"},
            {"Rn","Radon"},
            {"Ru","Ruthenium"},
            {"S","Sulfur"},
            {"Sb","Antimony"},
            {"Sc","Scandium"},
            {"Se","Selenium"},
            {"Sg","Seaborgium"},
            {"Si","Silicon"},
            {"Sm","Samarium"},
            {"Sn","Tin"},
            {"Sr","Strontium"},
            {"Ta","Tantalum"},
            {"Tb","Terbium"},
            {"Tc","Technetium"},
            {"Te","Tellurium"},
            {"Th","Thorium"},
            {"Ti","Titanium"},
            {"Tl","Thallium"},
            {"Tm","Thulium"},
            {"Ts","Tennessine"},
            {"U","Uranium"},
            {"V","Vanadium"},
            {"W","Tungsten"},
            {"Xe","Xenon"},
            {"Y","Yttrium"},
            {"Yb","Ytterbium"},
            {"Zn","Zinc"},
            {"Zr","Zirconium"}
        };

        public static string[][] ElementalForms(string word)
        {
            List<ElementItem> elementList = new List<ElementItem>();
            List<ElementItem> elementListWithRepeats = new List<ElementItem>();

            BuildArray(word, 0, elementList);

            elementListWithRepeats = RepeatLowerLevelItems(elementList);

            string[][] returnArray = BuildArrayOfStringArraysFromElementList(elementListWithRepeats);
            
            return returnArray;
        }

        public static List<ElementItem> RepeatLowerLevelItems(List<ElementItem> elementList)
        {
            int previousLevel = 0;
            for (int i = 0; i < elementList.Count(); i++)
            {
                if (elementList[i].Level < previousLevel)
                {
                    if (elementList[i].Level > 0)
                    {
                        int parentItemIndex = FindParentItemIndex(elementList[i].Level, i, elementList);
                        ElementItem repeatItem = elementList[parentItemIndex];
                        elementList.Insert(i, repeatItem);

                    }
                }
                previousLevel = elementList[i].Level;
            }

            return elementList;
        }

        public static int FindParentItemIndex(int level, int index, List<ElementItem> elementList)
        {
            int i = 0, lowestLevel = 0, lowestLevelIndex = 0;
            foreach(ElementItem item in elementList)
            {
                if (i >= index) break;
                if (item.Level == lowestLevel)
                {
                    lowestLevelIndex = i;
                    lowestLevel = item.Level;
                }
                i++;
            }
            return lowestLevelIndex;
        }

        public static string[][] BuildArrayOfStringArraysFromElementList(List<ElementItem> elementList)
        {
            List<List<String>> listOfLists = new List<List<String>>();
            List<String> innerList = new List<String>();
            List<String> newInnerList = new List<String>();

            int i = 0; // needed because can't rely on IndexOf - first item is repeated.
            foreach (ElementItem item in elementList)
            {
                if (i > 0 && item.Level == 0)
                {
                    newInnerList = innerList.Select(a => a.ToString()).ToList();
                    listOfLists.Add(newInnerList);
                    innerList.Clear();
                }
                innerList.Add(String.IsNullOrEmpty(item.Text) ? "" : item.Text);
                i++;
            }
            listOfLists.Add(innerList);

            return listOfLists.Select(a => a.ToArray()).ToArray();
        }

        public static void BuildArray(string word, int wordIndex, List<ElementItem> elementList)
        {
            bool matchFound = false;

            if (wordIndex == (word.Length - 1)) // last character
            {
                if (ELEMENTS.ContainsKey(word.Substring(wordIndex, 1)))
                {
                    matchFound = true;
                    ElementItem elementItem = new ElementItem
                    {
                        Level = wordIndex,
                        Text = String.Concat(ELEMENTS[word.Substring(wordIndex, 1)], " (", word.Substring(wordIndex, 1), ")")
                    };

                    elementList.Add(elementItem);
                }
                if (!matchFound)
                {
                    elementList.Clear();
                }
            }
            else
            {
                for (int i = 1; i < 3; i++)
                {
                    if (ELEMENTS.ContainsKey(word.Substring(wordIndex, i)))
                    {
                        matchFound = true;
                        ElementItem elementItem = new ElementItem
                        {
                            Level = wordIndex,
                            Text = String.Concat(ELEMENTS[word.Substring(wordIndex, i)], " (", word.Substring(wordIndex, i), ")")
                        };

                        elementList.Add(elementItem);

                        BuildArray(word, wordIndex + i, elementList);
                    }
                }
                if (!matchFound)
                {
                    elementList.Clear();
                }
            }
        }
    }
}