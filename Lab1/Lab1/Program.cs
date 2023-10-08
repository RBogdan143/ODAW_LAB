using Lab1;
using static Lab1.Student;

List<Student> Studenti = new List<Student>();
int n;

Console.WriteLine("Introduceti nr. de studenti pe care vreti sa-i adaugati: ");
string a = Console.ReadLine();
int.TryParse(a, out n);

for (int i = 0; i < n; i++)
{
    Studenti.Add(new Student());
}

Console.WriteLine("\n\n\nStudentii pe care i-ai introdus:");
for (int i = 0; i < n; i++)
{
    Studenti[i].afisare();
}