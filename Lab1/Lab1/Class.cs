using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Student
    {
        string nume, prenume, nr_matricol, forma_finatare;
        bool a_trecut_la_Vladoiu = false;
        int nr;
        public List<Materie> Materii = new List<Materie>();
        public class Materie
        {
            string titlu, durata;
            double nota_minima;
            public Materie()
            {
                Console.WriteLine("Introduceti numele materiei: ");
                titlu = Console.ReadLine();

                Console.WriteLine("Introduceti durata materiei: ");
                durata = Console.ReadLine();

                Console.WriteLine("Introduceti nota minima de obtinut la aceasta materie: ");
                string a = Console.ReadLine();
                double.TryParse(a, out nota_minima);
            }

            public void write()
            {
                Console.WriteLine("Numele materiei: " + titlu);

                Console.WriteLine("Durata cursului: " + durata);

                Console.WriteLine("Nota minima de obtinut pentru promovare: " + nota_minima);
            }
        }

        public Student()
        {
            Console.WriteLine("\nIntroduceti numele studentului: ");
            nume = Console.ReadLine();

            Console.WriteLine("Introduceti prenumele studentului: ");
            prenume = Console.ReadLine();

            Console.WriteLine("Introduceti nr. matricol al studentului: ");
            nr_matricol = Console.ReadLine();

            Console.WriteLine("Introduceti forma de finantare a studentului: ");
            forma_finatare = Console.ReadLine();

            Console.WriteLine("Are studentul o restanta la Algebra anul 1?");
            Console.WriteLine("Press y/n");
            string a = Console.ReadLine();
            if (a == "y")
                a_trecut_la_Vladoiu = true;

            Console.WriteLine("Introduceti nr. de materii pe care vreti sa le adaugati: ");
            a = Console.ReadLine();
            int.TryParse(a, out nr);

            for (int i = 0; i < nr; i++)
            {
                Materii.Add(new Materie());
            }
        }

        public void afisare()
        {
            Console.WriteLine("\n\nNumele studentului: " + nume);

            Console.WriteLine("Prenumele studentului: " + prenume);

            Console.WriteLine("Nr. matricol student: " + nr_matricol);

            Console.WriteLine("Forma de finantare a studentului: " + forma_finatare);

            if (a_trecut_la_Vladoiu)
                Console.WriteLine("Studentul a scapat de Vladoiu!");
            else
                Console.WriteLine("Studentul nu a scapat de Vladoiu! :(");

            Console.WriteLine("Fiind inscris la urmatoarele cursuri:\n");
            for (int i = 0; i < nr; i++)
            {
                Materii[i].write();
            }
        }

    }
}
