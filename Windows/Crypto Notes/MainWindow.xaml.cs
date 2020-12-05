using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Crypto_Notes
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			bt_encrypt.Click += EncryptClick;
			bt_encryptsave.Click += EncryptSaveClick;
			bt_decrypt.Click += DecryptClick;
			bt_decryptfromfile.Click += DecryptFromFileClick;
			tb_encloc.Text = "encrypted";
		}

		private void DecryptFromFileClick(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
			ofd.InitialDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CryptoNotes");
			ofd.Filter = "CryptoNotes (*.cn)|*.cn|All files (*.*)|*.*";
			if (ofd.ShowDialog(this) == true)
			{
				rtb_decrypt.Document.Blocks.Clear();
				string content = System.IO.File.ReadAllText(ofd.FileName);
				rtb_decrypt.Document.Blocks.Add(new Paragraph(new Run(content)));
			}
		}

		private void EncryptSaveClick(object sender, RoutedEventArgs e)
		{
			EncryptClick(null, e);
			var dir = System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CryptoNotes"));
			string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CryptoNotes",tb_encloc.Text + ".cn");
			System.IO.File.WriteAllText(path, new TextRange(rtb_encrypt.Document.ContentStart, rtb_encrypt.Document.ContentEnd).Text);
			Process.Start(dir.FullName);
		}

		private void DecryptClick(object sender, RoutedEventArgs e)
		{
			try
			{
				string text = new TextRange(rtb_decrypt.Document.ContentStart, rtb_decrypt.Document.ContentEnd).Text;
				string key = tb_dekey.Text;
				string decrypted_text = new Crypta().AESDecryption(text, key);
				rtb_decrypt.Document.Blocks.Clear();
				rtb_decrypt.Document.Blocks.Add(new Paragraph(new Run(decrypted_text)));
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			
		}

		private void EncryptClick(object sender, RoutedEventArgs e)
		{
			string text = new TextRange(rtb_encrypt.Document.ContentStart, rtb_encrypt.Document.ContentEnd).Text;
			string key = new Crypta().RandomKey();
			string cipher = new Crypta().AESEncryption(text, key);
			tb_enckey.Text = key;
			rtb_encrypt.Document.Blocks.Clear();
			rtb_encrypt.Document.Blocks.Add(new Paragraph(new Run(cipher)));
		}
	}
}
