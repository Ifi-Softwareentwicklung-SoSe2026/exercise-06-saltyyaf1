using System;
using System.Collections.Generic;

namespace Baufflaechenverwaltung
{
    public class Antragsteller
    {
        public string Name { get; set; } = string.Empty;
        public string Kontaktdaten { get; set; } = string.Empty;
        public string Firma { get; set; } = string.Empty;
    }

    public class Zeitplan
    {
        public DateTime Beginn { get; set; }
        public DateTime Fertigstellung { get; set; }
    }

    public enum Status
    {
        AntragEingereicht, Genehmigt, Abgelehnt, InBearbeitung, Abgeschlossen
    }

    public class GeplanteNutzung
    {
        public string Beschreibung { get; set; } = string.Empty;
    }

    public class Bauvorhaben
    {
        public string Titel { get; set; } = string.Empty;
        public Antragsteller Antragsteller { get; set; } = new Antragsteller();
        public GeplanteNutzung Nutzung { get; set; } = new GeplanteNutzung();
        public Zeitplan Zeitplan { get; set; } = new Zeitplan();
        public Status Status { get; set; }
        public List<Bauflaeche> ZugeordneteFlaechen { get; set; } = new List<Bauflaeche>();

        public void InformationenAusgeben()
        {
            Console.WriteLine($"--- Bauvorhaben: {Titel} ---");
            Console.WriteLine($"Antragsteller: {Antragsteller.Name} ({Antragsteller.Firma})");
            Console.WriteLine($"Nutzung: {Nutzung.Beschreibung}");
            Console.WriteLine($"Zeitraum: {Zeitplan.Beginn.ToShortDateString()} bis {Zeitplan.Fertigstellung.ToShortDateString()}");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine($"Anzahl Flächen: {ZugeordneteFlaechen.Count}");
            Console.WriteLine("----------------------------");
        }
    }

    public class Grundstueck
    {
        public string FlurstueckNummer { get; set; } = string.Empty;
        public double Groesse { get; set; }
        public string Lage { get; set; } = string.Empty;
        public string AktuelleNutzung { get; set; } = string.Empty;
        public string Bebaubarkeit { get; set; } = string.Empty;
        public List<Bauflaeche> Bauflaechen { get; set; } = new List<Bauflaeche>();
    }

    public class Bauflaeche
    {
        public string FlurstueckNummer { get; set; } = string.Empty;
        public double Groesse { get; set; }
        public string Lage { get; set; } = string.Empty;
        public string AktuelleNutzung { get; set; } = string.Empty;
        public string Bebaubarkeit { get; set; } = string.Empty;
        public string BPlanNummer { get; set; } = string.Empty;
        public decimal Bodenrichtwert { get; set; }
        public string Eigentuemer { get; set; } = string.Empty;
        public string Status { get; set; } = "frei"; // frei, reserviert, bebaut
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Beispiel-Daten
            var flaeche1 = new Bauflaeche 
            {
                FlurstueckNummer = "0015 00012 001/002", 
                Groesse = 500.0, 
                Lage = "Leipzig-Nord", 
                Status = "reserviert"
            };

            var vorhaben = new Bauvorhaben
            {
                Titel = "Neubau Wohnanlage Nord",
                Antragsteller = new Antragsteller { Name = "Max Mustermann", Firma = "BauAG GmbH" },
                Nutzung = new GeplanteNutzung { Beschreibung = "Wohngebäude" },
                Zeitplan = new Zeitplan { Beginn = DateTime.Now, Fertigstellung = DateTime.Now.AddYears(2) },
                Status = Status.InBearbeitung
            };
            vorhaben.ZugeordneteFlaechen.Add(flaeche1);

            // Demonstration der Funktionen
            Console.WriteLine("Bauflächenverwaltung Demonstration");
            vorhaben.InformationenAusgeben();
        }
    }
}