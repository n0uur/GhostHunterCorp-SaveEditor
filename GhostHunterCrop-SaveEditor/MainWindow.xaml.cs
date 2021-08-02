using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace GhostHunterCrop_SaveEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Player player;
        public List<Item> items;

        public Dictionary<double, string> MyDictionary { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            this.player = new Player();

            this.items = Item.GetBaseList();

            itemsList.DataContext = this.items;

            txtExp.TextChanged += experienceTextChange;

            this.updateTextFields();
        }

        private void experienceTextChange(object sender, TextChangedEventArgs e)
        {
            if (txtExp.Text.Equals(""))
                return;
            this.player.Exp = UInt32.Parse(txtExp.Text);
            txtLvl.Text = this.player.GetLevel().ToString();
        }

        public void updateObject()
        {
            this.player.Name = txtName.Text;
            this.player.Exp = UInt32.Parse(txtExp.Text);
            this.player.Money = UInt32.Parse(txtMoney.Text);
        }

        public void updateTextFields()
        {
            txtName.Text = this.player.Name;
            txtExp.Text = this.player.Exp.ToString();
            txtMoney.Text = this.player.Money.ToString();
            txtSkin.Text = this.player.Skin.ToString();
            txtColor.Text = this.player.Color;

            itemsList.Items.Refresh();
        }

        private void MenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Ghost Hunter Save files (*.sav)|*.sav|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                SaveData? saveData = SaveManager.LoadSaveFromFile(openFileDialog.FileName);
                if (!saveData.HasValue)
                {
                    MessageBox.Show("Invalid file type or damaged!", "Cannot Open Save File", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                this.player = saveData.Value.Player;
                Item.UpdateList(this.items, saveData.Value.Items);

                this.updateTextFields();
            }
        }

        private void MenuItem_Save_Click(object sender, RoutedEventArgs e)
        {
            this.updateObject();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Ghost Hunter Save files (*.sav)|*.sav|All files (*.*)|*.*";
            saveFileDialog.FileName = "save.sav";
            if (saveFileDialog.ShowDialog() == true)
            {
                if (SaveManager.SaveFile(saveFileDialog.FileName, this.player, this.items))
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
                item.Amount = 999;
            }

            this.updateTextFields();
        }
    }
}