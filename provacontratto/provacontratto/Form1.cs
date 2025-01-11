using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace provacontratto
{
    public class Contratto
    {
        public string Squadra { get; set; }
        public int Stipendio { get; set; }
        public int DurataContratto { get; set; }
        public int Bonus { get; set; }
        public string Sponsor { get; set; }
        public string Clausola { get; set; }
        public string ObiettivoSquadra { get; set; }
        public int BonusObiettivo { get; set; }

        public override string ToString()
        {
            return $"Squadra: {Squadra}\n" +
                   $"Stipendio: {Stipendio}\n" +
                   $"Durata: {DurataContratto} anni\n" +
                   $"Bonus: {Bonus}\n" +
                   $"Sponsor: {Sponsor}\n" +
                   $"Clausola: {Clausola}\n" +
                   $"Obiettivo Squadra: {ObiettivoSquadra}\n" +
                   $"Bonus Obiettivo: {BonusObiettivo}";
        }
    }

    public partial class Form1 : Form
    {
        private List<Contratto> contratti;
        private int contrattoAttuale;
        private List<Contratto> inAttesa;
        private ListBox lstContratto;
        private Button btnAccetta;
        private Button btnRifiuta;
        private Button btnAttesa;

        public Form1()
        {
            contratti = GeneraContratti();
            contrattoAttuale = 0;
            inAttesa = new List<Contratto>();

            lstContratto = new ListBox { Location = new Point(10, 10), Size = new Size(360, 160) };
            btnAccetta = new Button { Text = "Accetta", Location = new Point(10, 180), Size = new Size(100, 30) };
            btnRifiuta = new Button { Text = "Rifiuta", Location = new Point(120, 180), Size = new Size(100, 30) };
            btnAttesa = new Button { Text = "Attesa", Location = new Point(230, 180), Size = new Size(100, 30) };

            btnAccetta.Click += BtnAccetta_Click;
            btnRifiuta.Click += BtnRifiuta_Click;
            btnAttesa.Click += BtnAttesa_Click;

            Controls.Add(lstContratto);
            Controls.Add(btnAccetta);
            Controls.Add(btnRifiuta);
            Controls.Add(btnAttesa);

            VisualizzaContratto();
        }

        private void VisualizzaContratto()
        {
            lstContratto.Items.Clear();
            if (contrattoAttuale < contratti.Count)
            {
                lstContratto.Items.Add(contratti[contrattoAttuale].ToString());
            }
            else if (contratti.Count > 0)
            {
                lstContratto.Items.Add("Non ci sono più contratti disponibili.");
                btnAccetta.Enabled = false;
                btnRifiuta.Enabled = false;
                btnAttesa.Enabled = false;
            }
        }

        private void BtnAccetta_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hai accettato il seguente contratto:\n" + contratti[contrattoAttuale]);
            Application.Exit();
        }

        private void BtnRifiuta_Click(object sender, EventArgs e)
        {
            contratti.RemoveAt(contrattoAttuale);
            if (contrattoAttuale >= contratti.Count && inAttesa.Count > 0)
            {
                contratti.AddRange(inAttesa);
                inAttesa.Clear();
                contrattoAttuale = 0;
            }
            VisualizzaContratto();
        }

        private void BtnAttesa_Click(object sender, EventArgs e)
        {
            inAttesa.Add(contratti[contrattoAttuale]);
            contrattoAttuale++;
            if (contrattoAttuale >= contratti.Count)
            {
                contratti.AddRange(inAttesa);
                inAttesa.Clear();
                contrattoAttuale = 0;
            }
            VisualizzaContratto();
        }

        private List<Contratto> GeneraContratti()
        {
            Random random = new Random();
            List<string> squadre = new List<string> { "Squadra A", "Squadra B", "Squadra C" };
            List<string> sponsor = new List<string> { "Sponsor X", "Sponsor Y", "Sponsor Z" };
            List<string> clausole = new List<string> { "Nessuna rescissione", "Rescissione a 50M", "Rescissione a 100M" };
            List<string> obiettivi = new List<string> { "Vincere Champions", "Vincere Campionato", "Arrivare in Champions", "Triplete", "Vincere Coppa Italia" };

            List<Contratto> contratti = new List<Contratto>();

            for (int i = 0; i < 3; i++)
            {
                Contratto contratto = new Contratto
                {
                    Squadra = squadre[random.Next(squadre.Count)],
                    Stipendio = random.Next(50000, 100000),
                    DurataContratto = random.Next(1, 5),
                    Bonus = random.Next(10000, 20000),
                    Sponsor = sponsor[random.Next(sponsor.Count)],
                    Clausola = clausole[random.Next(clausole.Count)],
                    ObiettivoSquadra = obiettivi[random.Next(obiettivi.Count)],
                    BonusObiettivo = random.Next(5000, 15000)
                };
                contratti.Add(contratto);
            }

            return contratti;
        }
    }

    
}
