// See https://aka.ms/new-console-template for more information
using CodeWars_ElementalWords;

string[][] returnArray = ElementalWords.ElementalForms("snack");

foreach (var array in returnArray)
{
    Console.WriteLine(String.Join(", ", array));
}




