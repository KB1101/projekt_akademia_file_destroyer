/* Author: Kornel Kopko
 * Mail: kornelgcc@gmail.com
 * 10.06.2016
 */

using System;
using System.Collections.Generic;
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
using System.IO;
using System.Threading;


namespace projekt_akademia_file_destroyer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /* Definicje */
        public static bool BooleanTrue = true;
        public static bool BooleanFalse = false;
        /* Ta zmienna daje dostęp z poziomu innych klas do składników tej klasy */
        public static MainWindow main;
        /* Za pomocą tego będę pobierał wartości zaznaczonych radio buttonów */
        private RadioButtonsOptions options = null;
        /* Kolekcja plików do usunięcia */
        private List<File> _files;
        /* Licznik wczytanych plików */
        private int counter { get; set; }
        /* Uchwyt do okna dialogowego */
        private OpenFileDialog openFileDialog;

        public MainWindow()
        {
            // Inicjalizacja
            InitializeComponent();
            this._files = new List<File>();
            this.options = new RadioButtonsOptions();
            this.options.BooleanProperty = false;
            this.options.EnumProperty = MethodEnum.Random;

            this.DataContext = this.options;
            this.counter = 0;
            main = this;
        }

        /* Przycisk dodawania nowych plików kliknięty */
        private void SE_button_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true; // pozwala na wybranie wielu plików
            if (openFileDialog.ShowDialog() == true) InsertSelectedFilesIntoList();
        }

        /* Dodaje do listy plików nowe gdy te różnią się od pozostałych */
        private void InsertSelectedFilesIntoList()
        {
            string filename; // tak naprawdę to pełna ścieżka do pliku
            bool fileAlreadyExist = false; // czy plik już istnieje na liście ?


            for (int i = 0; i < openFileDialog.FileNames.Length; i++)
            {
                fileAlreadyExist = false;
                filename = openFileDialog.FileNames[i]; // pobranie ścieżki

                for (int j = 0; j < _files.Count; j++)
                {
                    // Sprawdza czy wybrany plik istnieje już w kolekcji plików
                    if ( _files[j].Equals(filename))
                    {
                        fileAlreadyExist = true;
                        break;
                    }
                }
                if (fileAlreadyExist) continue;

                //Dodaj plik do kolekcji
                _files.Add(new File(counter, openFileDialog.FileNames[i]));
                counter++;
            } // for end

            //Odśwież data grid'a
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = _files;
        }

        /* Przycisk pozwala wykluczyć plik zaznaczony na gridzie z kolekcji plików */
        private void Exclude_File_Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedFileInGrid = this.dataGrid.SelectedItem as File;
            // Jeśli nie zaznaczono żadnej pozycji na gridzie
            if (selectedFileInGrid == null) 
            {
                MessageBox.Show("No file was selected for exclude.", "Oopps!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Usuwanie pliku z listy
            int index = selectedFileInGrid.id - 1;
            _files.RemoveAt(index);
            this.counter = 0;

            // Odświeżenie id plików w liście
            for (int i = 0; i < _files.Count; i++)
            {
                _files[i].id = counter + 1;
                counter++;
            }
            // Odświeżenie data grid'a
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = _files;
        }
 
        /* Przycisk uruchamia funkcje kasujące wybrane pliki z dysku */
        private void Delete_Files_Button_Click(object sender, RoutedEventArgs e)
        {
            // Tworzymy nową instancje klasy BackgroundEaser, która pozwala czyścić pliki na dysku współbieżnie w tle nie blokując interfejsu
            BackgroundEraser backgroundEraser = new BackgroundEraser(this.options.BooleanProperty, this.options.EnumProperty);
            if (_files.Count == 0) /* Jeśli nie wybrano plików */
            {
                MessageBox.Show("There are no files to delete.", "Oopps!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else if (MessageBox.Show("Are you sure that you want to permanently delete the selected file(s).","Delete?",
                                      MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            { /*Jeśli użytkownik potwierdził chęc wymazania plików */
                backgroundEraser.run(ref _files);
            }

        }

        /* Wywoływana gdy proces usuwania dobiegnie końca */ 
        public void BackgroundEaserWorkerEnd()
        {
            MessageBox.Show("Erase process completed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _files.Clear();
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = _files;
        }

        /* Reakcja na zmiany progress bar'a */
        private void pBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = _files;
        }

        /* Wyświetla okno z informacjami o programie */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            About aboutWindow = new About();
            aboutWindow.Show();
        }

    } // class end


    public class RadioButtonsOptions
    {
        public MethodEnum EnumProperty { get; set; }
        public bool BooleanProperty { get; set; }
    }

    /* Enum do wskazywania która metoda usuwania została wybrana */
    public enum MethodEnum
    {
        Random,
        Zeros,
        Ones,
        ZerosOnes
    }

    /* Konwerter pozwalający pobrać wartości klikniętych radio buttonów i przerabia je na wartości z MethotEnum */
    public class RadioButtonCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }

}
