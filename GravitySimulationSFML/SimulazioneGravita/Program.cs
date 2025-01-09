// See https://aka.ms/new-console-template for more information
using SimulazioneGravita;

Console.WriteLine("Hello, World!");


Universo universo = new Universo();
Pianeta p1 = new Pianeta(new Vettore(1, 2), 10);
Pianeta p2 = new Pianeta(new Vettore(3, 3), 100);
universo.pianeti.Add(p1);
universo.pianeti.Add(p2);

//inizializza tre pianeti e avvia simulazione