using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.IO;

namespace projekt_akademia_file_destroyer
{
    class BackgroundEraser
    {
        
        private BackgroundWorker bw;
        private Boolean fastErase;
        private MethodEnum mode;
        

        public BackgroundEraser(Boolean fe, MethodEnum m)
        {
           // Przypisanie ustawień 
            this.fastErase = fe;
            this.mode = m;

            bw = new BackgroundWorker();

            // Ciało nowe wątku który zostanie uruchomiony w tle
            bw.DoWork += ((sender, args) =>
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                // Pobranie kopii listy plików
                List<File> _files = (List<File>)args.Argument;
                // Liczniki
                int filesCounter = _files.Count;
                int filesEraseCounter = 0;

                /* Tworzymy nową tablicę zadań do wykonania współbieżnego - efektywniejsze niż uruchamianie kilku wątków - komputer sam dobiera liczbę wątków do wykonania tych zadań */
                Task[] tasks = new Task[filesCounter];
                for (int i = filesCounter; i > 0; i--)
                {
                    int index = i - 1;
                    string filePath = (string)_files[index].fileFullPath;

                    // Definiowanie zadań - jeden plik do wmazania na jedno zadanie
                    tasks[index] = new Task(() =>
                    {
                        try
                        {
                            _files[index].StatusUpdate("Processing...");
                            // Odświeża progress bar'a i data grid'a
                            if (worker != null) worker.ReportProgress(filesEraseCounter * 100 / filesCounter);
                            // Wymazuje dany plik
                            Boolean result = FileEraserFunction(filePath, fastErase, mode);
                            if(result) _files[index].StatusUpdate("Deleted!");
                            else _files[index].StatusUpdate("Internal Error.");
                        }
                        catch (OwnException ex) // Własny wyjątek rzucany przez FileEraserFunction gdy plik nie istnieje
                        {
                            System.Windows.MessageBox.Show("File: " + ex.Message + " not found!\nExceptoion source: " + ex.Source, "Oopps!");
                            _files[index].StatusUpdate("File not found.");

                        }
                        finally
                        {
                            filesEraseCounter++;
                            if (worker != null) worker.ReportProgress(filesEraseCounter * 100 / filesCounter);
                        }
                    }); // tasks end

                } // for end

                try
                {
                    // Uruchomienie wykonywania wszystkich zadań w tle
                    for (int a = 0; a < filesCounter; a++) tasks[a].Start();
                    // Czekaj na zakończenie wszystkich zadań
                    Task.WaitAll(tasks);
                }
                catch (AggregateException ae) // Jeśli coś poszło nie tak -- przydatne przy debuggowaniu 
                {
                    Console.WriteLine("One or more exceptions occurred: ");
                    foreach (var ex in ae.Flatten().InnerExceptions) Console.WriteLine("{0}", ex.Message);
                }

                Console.WriteLine("Status of completed tasks:");
                foreach (var t in tasks)
                {
                    Console.WriteLine(">Task #{0}: {1}", t.Id, t.Status);
                }

                
            }); //bw.doWork end

            /* Reakcja na ReportProgress */
            bw.ProgressChanged += ((sender, args) =>
            {
                MainWindow.main.pBar.Minimum = 0;
                MainWindow.main.pBar.Maximum = 100;
                MainWindow.main.pBar.Value = args.ProgressPercentage;
            });

            /* Gdy background worker zakończy swe działanie */
            bw.RunWorkerCompleted += ((sender, args) =>
            {
                Console.WriteLine("Process ended.");
                MainWindow.main.BackgroundEaserWorkerEnd();
            });


        } // constructor end

        /* Uruchomienie background worker'a */
        public void run(ref List<File> _files)
        {
            bw.WorkerReportsProgress = true;
            bw.RunWorkerAsync( _files);
        }

        /* Funkcja wymazuje pliki z dysku zadanymi metodami */
        private bool FileEraserFunction(string filename, Boolean fastErase, MethodEnum mode)
        {
            bool ret = true; 
            const int BufferSize = 1024000; // Wielkość bufora 1MB
            int eraseLoops; // Ilość pętli wymazujących 

            if (fastErase == false) eraseLoops = 100;
            else eraseLoops = 32;

            // Rzucanie własnego wyjątku jeśli plik nie istnieje na dysku
            if (!System.IO.File.Exists(filename)) throw new OwnException(filename);
        
            try
            {
                byte[] DataBuffer;

                if (mode != MethodEnum.ZerosOnes) // Jeśli nie wybrano metody napisania samymi zerami i jedynkami 
                {
                    using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
                    {

                        DataBuffer = new byte[BufferSize];
                        int readDataNumber = -1; // liczba przeczytanych bajtów

                        FileInfo fileInfo = new FileInfo(filename); // Pobranie informacji o pliku
                        long count = fileInfo.Length; // Licznik danych do pobrania
                        long offset = 0;

                        for (int i = 0; i < eraseLoops; i++)
                        {
                            while (count >= 0) /* Dopóki nie odczytano całego pliku */
                            {
                                readDataNumber = stream.Read(DataBuffer, 0, BufferSize);
                                if (readDataNumber == 0) break;

                                switch (mode)  /* Wybór metody wymazywania */
                                {
                                    case MethodEnum.Ones:
                                        for (int j = 0; j < readDataNumber; j++) DataBuffer[j] = 0xFF;
                                        break;
                                    case MethodEnum.Zeros:
                                        for (int j = 0; j < readDataNumber; j++) DataBuffer[j] = 0x00;
                                        break;
                                    case MethodEnum.Random :
                                    default :
                                        Random randomBytes = new Random();
                                        randomBytes.NextBytes(DataBuffer);
                                        break;
                                } // switch end

                                // Nadpisanie pobranych bajtów jedną z wybranych wartości 
                                stream.Seek(offset, SeekOrigin.Begin);
                                stream.Write(DataBuffer, 0, readDataNumber);

                                // Obliczenie przesunięcia w pliku i pomniejszenie licznika bajtów do pobrania
                                offset += readDataNumber; 
                                count -= readDataNumber;
                            } //while end
                        } // for end
                    }// using end
                } // if end

                /* To samo co wyżej tylko powoduje zniszczenie śladów po pliku na dysku magnetycznym */
                using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
                {
                    FileInfo fileInfo = new FileInfo(filename);
                    long count = fileInfo.Length;
                    long offset = 0;
                    DataBuffer = new byte[BufferSize];
                    int readDataNumber = -1;
                    for (int i = 0; i < 12; i++)
                    {
                        fileStream.Seek(0, SeekOrigin.Begin);
                        while (count >= 0)
                        {
                            readDataNumber = fileStream.Read(DataBuffer, 0, BufferSize);
                            if (readDataNumber == 0) break;
                            for (int j = 0; j < readDataNumber; j++) DataBuffer[j] = 0x00;
                            fileStream.Seek(offset, SeekOrigin.Begin);
                            fileStream.Write(DataBuffer, 0, readDataNumber);
                            for (int j = 0; j < readDataNumber; j++) DataBuffer[j] = 0xFF;
                            fileStream.Seek(offset, SeekOrigin.Begin);
                            fileStream.Write(DataBuffer, 0, readDataNumber);
                            offset += readDataNumber;
                            count -= readDataNumber;
                        } //while end
                    } //for end 
                }// using end

                /* Dla utrudnienia ewentualnego odzyskiwania danych zmienia nazwę pliku na losowy ciąg znaków */
                string newFileName = "";
                Random random = new Random();
                string prevName = System.IO.Path.GetFileName(filename);
                string dirName = System.IO.Path.GetDirectoryName(filename);
                do
                {
                    int getRandomLetters = random.Next(9);
                    for (int i = 0; i < prevName.Length + getRandomLetters; i++)
                    {
                        newFileName += random.Next(9).ToString();
                    }
                    newFileName = dirName + "\\" + newFileName;
                } while (System.IO.File.Exists(newFileName));

                System.IO.File.Move(filename, newFileName); // zmiana nazwy pliku
                System.IO.File.Delete(newFileName); // Usunięcie pliku
            }
            catch
            {
                ret = false;
            }
            return ret;
        } //file eraser end

    } // class end



    /* Klasa własnego wyjątku */
    class OwnException : Exception
    {
        public OwnException(string message) 
            : base(message)
        {
            this.Source = "eraseFile() -> System.IO.File.Exists(String)";
        }
    }

}
