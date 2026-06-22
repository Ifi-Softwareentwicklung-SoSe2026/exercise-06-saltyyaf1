using System;
using System.Collections.Generic;

namespace Baufflaechenverwaltung
{
    public enum FlaechenStatus
    {
        Frei,
        Reserviert,
        Bebaut
    }

    public enum BauvorhabenStatus
    {
        AntragEingereicht,
        Genehmigt,
        Abgelehnt,
        InBearbeitung,
        Abgeschlossen
    }

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
        public BauvorhabenStatus Status { get; set; }
        public List<Bauflaeche> ZugeordneteFlaechen { get; set; } = new List<Bauflaeche>();

        public void StatusAktualisieren(BauvorhabenStatus neuerStatus)
        {
            Status = neuerStatus;
        }
    }

    public class Bauflaeche
    {
        public string FlaechenId { get; set; } = string.Empty;
        public double Groesse { get; set; }
        public string Lage { get; set; } = string.Empty;
        public string AktuelleNutzung { get; set; } = string.Empty;
        public bool Bebaubarkeit { get; set; } // true = ja, false = nein
        public string BPlanNummer { get; set; } = string.Empty;
        public decimal Bodenrichtwert { get; set; }
        public string Eigentuemer { get; set; } = string.Empty;
        public FlaechenStatus Status { get; set; }

        public void FlaecheReservieren()
        {
            if (Status == FlaechenStatus.Frei)
            {
                Status = FlaechenStatus.Reserviert;
            }
        }
    }

    public class Grundstueck
    {
        public string FlurstueckNummer { get; set; } = string.Empty;
        public List<Bauflaeche> Bauflaechen { get; set; } = new List<Bauflaeche>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Demonstration der Funktionalität
            var grundstueck = new Grundstueck { FlurstueckNummer = "0015 00012 001/002" };
            var flaeche = new Bauflaeche
            {
                FlaechenId = "F1",
                Groesse = 500.0,
                Lage = "Nordseite",
                AktuelleNutzung = "Brachfläche",
                Bebaubarkeit = true,
                BPlanNummer = "BP-2022-089",
                Bodenrichtwert = 500m,
                Eigentuemer = "Max Mustermann",
                Status = FlaechenStatus.Frei
            };
            grundstueck.Bauflaechen.Add(flaeche);

            var vorhaben = new Bauvorhaben
            {
                Titel = "Neubau Wohnhaus",
                Antragsteller = new Antragsteller { Name = "Erika Musterfrau", Firma = "Bau GmbH" },
                Nutzung = new GeplanteNutzung { Beschreibung = "Wohngebäude" },
                Zeitplan = new Zeitplan { Beginn = DateTime.Now, Fertigstellung = DateTime.Now.AddYears(1) },
                Status = BauvorhabenStatus.AntragEingereicht
            };

            flaeche.FlaecheReservieren();
            vorhaben.ZugeordneteFlaechen.Add(flaeche);

            Console.WriteLine($"Bauvorhaben '{vorhaben.Titel}' für Fläche {flaeche.FlaechenId} angelegt.");
            Console.WriteLine($"Status der Fläche: {flaeche.Status}");
            Console.WriteLine($"Status des Vorhabens: {vorhaben.Status}");
        }
    }
}