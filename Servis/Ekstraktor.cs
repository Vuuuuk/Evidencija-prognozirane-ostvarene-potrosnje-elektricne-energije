﻿using Common.Interface;
using Common.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Servis
{
    public class Ekstraktor : IEkstraktor
    {
        public string CuvanjePodatakaCSV(List<RelativnoOdstupanje> relodstupanje)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV (*.csv) | *.csv";
            sfd.FileName = "Evidencija relativnog odstupanja.csv";
            bool fileError = false;
            if(sfd.ShowDialog().Equals(true))
            {
                if(File.Exists(sfd.FileName))
                {
                    try
                    {
                        File.Delete(sfd.FileName);
                    }
                    catch (IOException e)
                    {
                        fileError = true;
                        MessageBox.Show("Neuspšno kreiranje fajla, molimo pokušajte ponovo!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                if(!fileError)
                {
                    File.AppendAllLines(sfd.FileName, relodstupanje.Select(x => string.Join(",", x)));
                    return sfd.SafeFileName + "_" + sfd.FileName;
                }
            }
            return string.Empty;
        }
    }
}
