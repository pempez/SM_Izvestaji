using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace izvestaji
{
    public partial class formPracenje : Form
    {
        string format = "yyyy-MM-dd HH:mm:ss";

        public formPracenje()
        {
            InitializeComponent();
            ucitaj();

            cbRadnik.SelectedValue = "==SVI==";
            cbNalog.SelectedValue = "==SVI==";
        }

        private void ucitaj()
        {
            ucitajRadnike();
            ucitajNaloge();
        }
        private void ucitajRadnike()
        {
            cbRadnik.DataSource = metode.DB.baza_upit("select '==SVI==' as name union select name from dbo.[Stirg Produkcija$Resource] order by name  ");
            cbRadnik.DisplayMember = "Name";
            cbRadnik.ValueMember = "Name";
          

        }
        private void ucitajNaloge()
        {
            cbNalog.DataSource = metode.DB.baza_upit("select '==SVI==' as nalog  union select [Prod_ Order No_] as nalog from dbo.[Stirg Produkcija$Output Journal Data]   order by nalog ");
            cbNalog.DisplayMember = "nalog";
            cbNalog.ValueMember = "nalog";
        }
        private void btnPronadji_Click(object sender, EventArgs e)
        {
            DateTime datumOd = new DateTime(dtpDatum.Value.Year, dtpDatum.Value.Month, dtpDatum.Value.Day);
            DateTime datumDo = new DateTime(dtpDatumDo.Value.Year, dtpDatumDo.Value.Month, dtpDatumDo.Value.Day);


            //ovako treba
            //            SELECT DISTINCT  
            //                         TOP (100) PERCENT dbo.[Stirg Produkcija$Resource].Name, dbo.[Stirg Produkcija$Capacity Ledger Entry].[Posting Date], 
            //                         (dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Setup Time] + dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Wait Time] + dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Move Time])
            //                          + dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Run Time] * dbo.[Stirg Produkcija$Prod_ Order Line].Quantity AS predvidjenVreme, 
            //                         dbo.[Stirg Produkcija$Capacity Ledger Entry].Description, dbo.[Stirg Produkcija$Prod_ Order Line].[Item No_], dbo.[Stirg Produkcija$Prod_ Order Line].Quantity, 
            //                         dbo.[Stirg Produkcija$Prod_ Order Line].[Variant Code], dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Setup Time], 
            //                         dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Run Time], dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Wait Time], 
            //                         dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Move Time], dbo.[Stirg Produkcija$Capacity Ledger Entry].[Routing No_], 
            //                         dbo.[Stirg Produkcija$Prod_ Order Line].[Line No_], dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Routing Reference No_], 
            //                         dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Prod_ Order No_], dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Operation No_], 
            //                         dbo.[Stirg Produkcija$Resource].No_, dbo.[Stirg Produkcija$Capacity Ledger Entry].[Output Quantity]
            //FROM            dbo.[Stirg Produkcija$Prod_ Order Line] INNER JOIN
            //                         dbo.[Stirg Produkcija$Prod_ Order Routing Line] ON 
            //                         dbo.[Stirg Produkcija$Prod_ Order Line].[Routing No_] = dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Routing No_] AND 
            //                         dbo.[Stirg Produkcija$Prod_ Order Line].[Prod_ Order No_] = dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Prod_ Order No_] AND 
            //                         dbo.[Stirg Produkcija$Prod_ Order Line].[Line No_] = dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Routing Reference No_] INNER JOIN
            //                         dbo.[Stirg Produkcija$Capacity Ledger Entry] ON 
            //                         dbo.[Stirg Produkcija$Prod_ Order Line].[Prod_ Order No_] = dbo.[Stirg Produkcija$Capacity Ledger Entry].[Order No_] AND 
            //                         dbo.[Stirg Produkcija$Prod_ Order Line].[Line No_] = dbo.[Stirg Produkcija$Capacity Ledger Entry].[Order Line No_] AND 
            //                         dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Routing No_] = dbo.[Stirg Produkcija$Capacity Ledger Entry].[Routing No_] AND 
            //                         dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Operation No_] = dbo.[Stirg Produkcija$Capacity Ledger Entry].[Operation No_] INNER JOIN
            //                         dbo.[Stirg Produkcija$Resource] ON dbo.[Stirg Produkcija$Capacity Ledger Entry].[Global Dimension 2 Code] = dbo.[Stirg Produkcija$Resource].No_
            //WHERE        (1 = 1) AND (dbo.[Stirg Produkcija$Resource].Name = N'Jović Miloš') AND (dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Prod_ Order No_] = N'RN18-01483') AND 
            //                         (dbo.[Stirg Produkcija$Capacity Ledger Entry].[Posting Date] = CONVERT(DATETIME, '2018-5-18 00:00:00', 102))
            //ORDER BY dbo.[Stirg Produkcija$Prod_ Order Line].[Item No_]

            //string qSelect = "SELECT  DISTINCT       TOP (100) PERCENT dbo.[Stirg Produkcija$Output Journal Data].[Operation No_], dbo.[Stirg Produkcija$Output Journal Data].[Resource No_], dbo.[Stirg Produkcija$Resource].Name, dbo.[Stirg Produkcija$Output Journal Data].[Prod_ Order No_], " +
            //           "  dbo.[Stirg Produkcija$Capacity Ledger Entry].[Posting Date], dbo.[Stirg Produkcija$Output Journal Data].[Item No_],  " +
            //    //dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Setup Time], dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Run Time],  " +
            //    //" dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Wait Time], dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Move Time], dbo.[Stirg Produkcija$Production Order].Quantity,  " +
            //            "  (dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Setup Time] + dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Wait Time] + dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Move Time])  " +
            //            "  + dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Run Time] * dbo.[Stirg Produkcija$Production Order].Quantity AS predvidjenVreme, " +
            //            "  dbo.[Stirg Produkcija$Capacity Ledger Entry].Description" +//, dbo.[Stirg Produkcija$Capacity Ledger Entry].[Posting Date] AS Expr1, dbo.[Stirg Produkcija$Capacity Ledger Entry].[Run Time] AS ostvarenoVreme_ZAKASNIJE,  " +
            //            " FROM            dbo.[Stirg Produkcija$Output Journal Data] INNER JOIN " +
            //             "  dbo.[Stirg Produkcija$Resource] ON dbo.[Stirg Produkcija$Output Journal Data].[Resource No_] = dbo.[Stirg Produkcija$Resource].No_ INNER JOIN " +
            //            "   dbo.[Stirg Produkcija$Prod_ Order Routing Line] ON dbo.[Stirg Produkcija$Output Journal Data].[Prod_ Order No_] = dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Prod_ Order No_] AND  " +
            //             "  dbo.[Stirg Produkcija$Output Journal Data].[Operation No_] = dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Operation No_] INNER JOIN " +
            //            "   dbo.[Stirg Produkcija$Production Order] ON dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Routing No_] = dbo.[Stirg Produkcija$Production Order].[Routing No_] AND  " +
            //            "   dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Prod_ Order No_] = dbo.[Stirg Produkcija$Production Order].No_ INNER JOIN " +
            //            "   dbo.[Stirg Produkcija$Capacity Ledger Entry] ON dbo.[Stirg Produkcija$Output Journal Data].[Document No_] = dbo.[Stirg Produkcija$Capacity Ledger Entry].[Document No_] AND  " +
            //           "    dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Operation No_] = dbo.[Stirg Produkcija$Capacity Ledger Entry].[Operation No_] AND  " +
            //           "    dbo.[Stirg Produkcija$Output Journal Data].[Item No_] = dbo.[Stirg Produkcija$Capacity Ledger Entry].[Item No_] AND  " +
            //            "   dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Prod_ Order No_] = dbo.[Stirg Produkcija$Capacity Ledger Entry].[Order No_] " +

            string qSelect = " SELECT DISTINCT  " +
                // "    TOP (100) PERCENT dbo.[Stirg Produkcija$Resource].Name, dbo.[Stirg Produkcija$Capacity Ledger Entry].[Posting Date],  "+
                // "    (dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Setup Time] + dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Wait Time] + dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Move Time]) "+
                //"      + dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Run Time] * dbo.[Stirg Produkcija$Prod_ Order Line].Quantity AS predvidjenVreme,  "+
                //"     dbo.[Stirg Produkcija$Capacity Ledger Entry].Description, dbo.[Stirg Produkcija$Prod_ Order Line].[Item No_], dbo.[Stirg Produkcija$Prod_ Order Line].Quantity, "+
                //"     dbo.[Stirg Produkcija$Prod_ Order Line].[Variant Code], dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Setup Time],  "+
                //"     dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Run Time], dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Wait Time],  "+
                //"     dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Move Time], dbo.[Stirg Produkcija$Capacity Ledger Entry].[Routing No_],  "+
                // "    dbo.[Stirg Produkcija$Prod_ Order Line].[Line No_], dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Routing Reference No_],  "+
                // "    dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Prod_ Order No_], dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Operation No_],  "+
                // "    dbo.[Stirg Produkcija$Resource].No_, dbo.[Stirg Produkcija$Capacity Ledger Entry].[Output Quantity] "+
                         "      TOP (100) PERCENT dbo.[Stirg Produkcija$Resource].No_, dbo.[Stirg Produkcija$Resource].Name, dbo.[Stirg Produkcija$Capacity Ledger Entry].[Posting Date], dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Prod_ Order No_],  " +
                        "    dbo.[Stirg Produkcija$Prod_ Order Line].[Item No_],  " +
                        "    (dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Setup Time] + dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Wait Time] + dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Move Time])  " +
                        "    + dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Run Time] * dbo.[Stirg Produkcija$Prod_ Order Line].Quantity AS predvidjenoVreme, dbo.[Stirg Produkcija$Capacity Ledger Entry].Description,  " +
                        "    dbo.[Stirg Produkcija$Prod_ Order Line].Quantity, dbo.[Stirg Produkcija$Capacity Ledger Entry].[Output Quantity], dbo.[Stirg Produkcija$Capacity Ledger Entry].No_ AS masinaNo " +
                      "  FROM            dbo.[Stirg Produkcija$Prod_ Order Line] INNER JOIN " +
                        "     dbo.[Stirg Produkcija$Prod_ Order Routing Line] ON  " +
                        "     dbo.[Stirg Produkcija$Prod_ Order Line].[Routing No_] = dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Routing No_] AND  " +
                       "      dbo.[Stirg Produkcija$Prod_ Order Line].[Prod_ Order No_] = dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Prod_ Order No_] AND  " +
                       "      dbo.[Stirg Produkcija$Prod_ Order Line].[Line No_] = dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Routing Reference No_] INNER JOIN " +
                        "     dbo.[Stirg Produkcija$Capacity Ledger Entry] ON  " +
                        "     dbo.[Stirg Produkcija$Prod_ Order Line].[Prod_ Order No_] = dbo.[Stirg Produkcija$Capacity Ledger Entry].[Order No_] AND  " +
                        "     dbo.[Stirg Produkcija$Prod_ Order Line].[Line No_] = dbo.[Stirg Produkcija$Capacity Ledger Entry].[Order Line No_] AND  " +
                        "     dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Routing No_] = dbo.[Stirg Produkcija$Capacity Ledger Entry].[Routing No_] AND  " +
                         "    dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Operation No_] = dbo.[Stirg Produkcija$Capacity Ledger Entry].[Operation No_] INNER JOIN " +
                         "    dbo.[Stirg Produkcija$Resource] ON dbo.[Stirg Produkcija$Capacity Ledger Entry].[Global Dimension 2 Code] = dbo.[Stirg Produkcija$Resource].No_ " +

                         "  WHERE      1=1 ";


            if (cbRadnik.Text != "==SVI==")
            {
                qSelect += " AND (dbo.[Stirg Produkcija$Resource].Name = N'" + cbRadnik.SelectedValue.ToString() + "')";
            }
            if (cbNalog.Text != "==SVI==")
            {
                qSelect += "  AND (dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Prod_ Order No_] = N'" + cbNalog.SelectedValue.ToString() + "')"; ;
            }
            if (cbDatum.Checked)
            {
                qSelect += " and (dbo.[Stirg Produkcija$Capacity Ledger Entry].[Posting Date] >= CONVERT(DATETIME, '" + datumOd.Year + "-" + datumOd.Month + "-" + datumOd.Day + " 00:00:00', 102))" +
                    "and (dbo.[Stirg Produkcija$Capacity Ledger Entry].[Posting Date] <= CONVERT(DATETIME, '" + datumDo.Year + "-" + datumDo.Month + "-" + datumDo.Day + " 23:59:59', 102)) ";
            }
            qSelect += " ORDER BY dbo.[Stirg Produkcija$Capacity Ledger Entry].[Posting Date], dbo.[Stirg Produkcija$Prod_ Order Routing Line].[Prod_ Order No_]";

            DataTable dt = metode.DB.baza_upit(qSelect);

            if (dt.Rows.Count > 0)
            {
                dgvRez.DataSource = dt;
            }
            else
            {
                dgvRez.DataSource = null;
                MessageBox.Show("Nema podataka za zadati kriterijum");
            }

        }

        private void btnUnesi_Click(object sender, EventArgs e)
        {
            //  dbo.[Stirg Produkcija$Output Journal Data].[Prod_ Order No_], dbo.[Stirg Produkcija$Capacity Ledger Entry].[Posting Date], 
            //dbo.[Stirg Produkcija$Output Journal Data].[Item No_], dbo.[Stirg Produkcija$Output Journal Data].[Resource No_]
            string Operation = "";
            string Prod_Order = "";
            DateTime datum;
            string Item = "";
            string Resource = "";
            string masinaNo = "";

            DateTime radnoVremeOd;
            DateTime radnoVremeDo;
            double pauza = 0;
            string problemi = "";
            double izbubljenoVreme = 0;
            double predvidjenoVreme = 0;

            if (dgvRez.SelectedRows.Count > 0)
            {
                //Operation = dgvRez.CurrentRow.Cells["Operation No_"].Value.ToString();
                //Prod_Order = dgvRez.CurrentRow.Cells["Prod_ Order No_"].Value.ToString();
                datum = DateTime.Parse(dgvRez.CurrentRow.Cells["Posting Date"].Value.ToString());
                // Item = dgvRez.CurrentRow.Cells["Item No_"].Value.ToString();
                Resource = dgvRez.CurrentRow.Cells["No_"].Value.ToString();
                masinaNo = dgvRez.CurrentRow.Cells["masinaNo"].Value.ToString();
                problemi = rtbProblemi.Text;

                radnoVremeOd = dtpOd.Value;
                radnoVremeDo = dtpDo.Value;

                if (tbPauza.Text == "") tbPauza.Text = "0";
                try
                {
                    pauza = double.Parse(tbPauza.Text);
                }
                catch
                {
                    MessageBox.Show("Vreme za pauzu i manipulaciju mora biti uneto u minutima!");
                    tbPauza.Focus();
                    return;
                }

                if (tbIzgubljenoVreme.Text == "") tbIzgubljenoVreme.Text = "0";
                try
                {
                    izbubljenoVreme = double.Parse(tbIzgubljenoVreme.Text);
                }
                catch
                {
                    MessageBox.Show("Vreme za izgubljeno vreme mora biti uneto u minutima!");
                    tbIzgubljenoVreme.Focus();
                    return;
                }

                //predvidjeno vreme racun
                foreach (DataGridViewRow r in dgvRez.Rows)
                {
                    predvidjenoVreme += double.Parse(r.Cells["predvidjenoVreme"].Value.ToString());
                }


                string qInsert = "INSERT INTO pracenjeRadnika(predvidjenoVreme,izgubljenoVreme,MasinaNo, [Resource No_], [Posting Date], radnoVremeOd, radnoVremeDo, pauzaManipulacija, problemi) " + // [Prod_ Order No_] N'" + Prod_Order + "',,[Item No_] N'" + Item + "',, [Operation No_]N'" + Operation + "',, 
                                 " VALUES        ('" + predvidjenoVreme.ToString().Replace(",", ".") + "','" + izbubljenoVreme.ToString().Replace(",", ".") + "', N'" + masinaNo + "', N'" + Resource + "','" + datum.ToString(format) + "','" + radnoVremeOd.ToString(format) + "','" + radnoVremeDo.ToString(format) + "','" + pauza.ToString().Replace(",", ".") + "',N'" + problemi + "')";
                metode.DB.pristup_bazi(qInsert);

                dgvRez_Click(null, null);
            }
            else
            {
                MessageBox.Show("niste odabrali radnika!");
                return;
            }
        }

        private void dtpDatum_ValueChanged(object sender, EventArgs e)
        {
            dtpOd.Value = dtpDatum.Value;
            dtpDo.Value = dtpDatum.Value;
        }

        private void ucitajRadnika()
        {
            string resource = "";
            DateTime datum;
            datum = dtpDatum.Value;

            string qUpit = "SELECT        TOP (200) [Resource No_], [Prod_ Order No_], [Posting Date], [Item No_], [Operation No_], radnoVremeOd, radnoVremeDo, pauzaManipulacija, problemi " +
                            " FROM            pracenjeRadnika WHERE        ([Resource No_] = N'" + resource + "') AND ([Posting Date] = '" + datum.ToString(format) + "')";
            DataTable dt = metode.DB.baza_upit(qUpit);
        }

        private void dgvRez_Click(object sender, EventArgs e)
        {
            if (dgvRez.SelectedRows.Count > 0)
            {
                string idRAdnik = dgvRez.CurrentRow.Cells["No_"].Value.ToString();
                string masniNo = dgvRez.CurrentRow.Cells["masinaNo"].Value.ToString();
                DateTime datum = DateTime.Parse(dgvRez.CurrentRow.Cells["Posting Date"].Value.ToString());

                string qUpit = " SELECT  predvidjenoVreme,  DATEDIFF(minute, radnoVremeOd, radnoVremeDo) AS ostvarenoVreme,     pauzaManipulacija + izgubljenoVreme + predvidjenoVreme AS razlika, "+
                        "   FORMAT(radnoVremeOd, 'HH:mm') radnoVremeOd,  FORMAT(radnoVremeDo, 'HH:mm') AS radnoVremeDo, pauzaManipulacija,izgubljenoVreme, problemi " +
                        " FROM  [Stirg_local].dbo.pracenjeRadnika " +
                        " WHERE        ([Resource No_] = N'" + idRAdnik + "') AND ([Posting Date] = '" + datum.ToString(format) + "') AND (masinaNo = N'" + masniNo + "')";
                DataTable dt = metode.DB.baza_upit(qUpit);
                if (dt.Rows.Count > 0)
                {
                    dgvPracenje.DataSource = dt;
                }
                else
                {
                    dgvPracenje.DataSource = null;
                }
            }
        }
    }
}
