using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace GhostHunterCorp_SaveEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Player player;
        public List<Item> items;
        public List<Location> locations;

        public Dictionary<double, string> MyDictionary { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            this.player = new Player();

            this.items = Item.GetBaseList();

            this.locations = Location.GetBaseList();

            itemsList.DataContext = this.items;

            ownedLocationsList.DataContext = this.locations;

            txtExp.TextChanged += experienceTextChange;

            this.updateTextFields();
        }

        private void experienceTextChange(object sender, TextChangedEventArgs e)
        {
            if (txtExp.Text.Equals(""))
                return;
            this.player.Exp = txtExp.Text;
            //txtLvl.Text = this.player.GetLevel().ToString();
        }

        public void updateObjects()
        {
            this.player.Name = txtName.Text;
            this.player.Exp = txtExp.Text;
            this.player.Money = txtMoney.Text;

            this.player.OwnedItems = Item.ToString(this.items);
            this.player.OwnedLocation = Location.ToString(this.locations);
        }

        public void updateTextFields()
        {
            txtName.Text = this.player.Name;
            txtExp.Text = this.player.Exp;
            txtMoney.Text = this.player.Money;
            //txtExp.Text = this.player.Exp.ToString();
            //txtMoney.Text = this.player.Money.ToString();
            //txtColor.Text = this.player.Color;

            itemsList.Items.Refresh();
            ownedLocationsList.Items.Refresh();
        }

        private void MenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            string defaultSavePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).ToString() + @"\AppData\LocalLow\StudioGoupil\Ghost Exorcism Inc";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = "Ghost Hunter Save files (*.save)|*.save|All files (*.*)|*.*";
            if (Directory.Exists(defaultSavePath))
            {
                openFileDialog.InitialDirectory = defaultSavePath;
            }

            if (openFileDialog.ShowDialog() == true)
            {
                SaveData? saveData = SaveManager.LoadSaveFromFile(openFileDialog.FileName);
                if (!saveData.HasValue)
                {
                    MessageBox.Show("Invalid file type or damaged!", "Cannot Open Save File", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                File.Copy(openFileDialog.FileName, openFileDialog.FileName + ".bak", true);

                this.player = saveData.Value.Player;
                Item.UpdateList(this.items, Item.Parse(player.OwnedItems));

                this.locations = Location.Parse(player.OwnedLocation, this.locations);

                this.updateTextFields();
            }
        }

        private void MenuItem_Save_Click(object sender, RoutedEventArgs e)
        {
            this.updateObjects();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Ghost Hunter Save files (*.save)|*.save|All files (*.*)|*.*";
            saveFileDialog.FileName = "Gex0000.save";
            if (saveFileDialog.ShowDialog() == true)
            {
                if (SaveManager.SaveFile(saveFileDialog.FileName, this.player))
                    MessageBox.Show("File was saved successfully!", "Saved!", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                else
                    MessageBox.Show("Cannot save to file. Maybe this program just f_cked up XD", "Cannot Save To File",
                        MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Macro_Max_Items_Click(object sender, RoutedEventArgs e)
        {
            foreach (Item item in this.items)
            {
                item.Amount = 99;
            }
            foreach (Location location in this.locations)
            {
                location.IsOwned = true;
            }

            this.updateTextFields();
        }
    }
}