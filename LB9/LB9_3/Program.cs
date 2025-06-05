using System.Text.Json;
namespace LB9_3
{
    class Program
    {
        static List<Song> songs = new List<Song>();

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== МЕНЮ ===");
                Console.WriteLine("1. Додати пісню");
                Console.WriteLine("2. Видалити пісню");
                Console.WriteLine("3. Змінити пісню");
                Console.WriteLine("4. Пошук пісні");
                Console.WriteLine("5. Показати всі пісні виконавця");
                Console.WriteLine("6. Зберегти у файл");
                Console.WriteLine("7. Завантажити з файлу");
                Console.WriteLine("0. Вихід");
                Console.Write("Ваш вибір: ");

                switch (Console.ReadLine())
                {
                    case "1": AddSong(); break;
                    case "2": DeleteSong(); break;
                    case "3": EditSong(); break;
                    case "4": SearchSong(); break;
                    case "5": SongsByPerformer(); break;
                    case "6": SaveToFile(); break;
                    case "7": LoadFromFile(); break;
                    case "0": running = false; break;
                    default: Console.WriteLine("Невірний вибір!"); break;
                }
            }
        }

        static void AddSong()
        {
            var song = new Song();
            Console.Write("Назва пісні: ");
            song.Title = Console.ReadLine();
            Console.Write("П.І.Б. автора: ");
            song.Author = Console.ReadLine();
            Console.Write("Композитор: ");
            song.Composer = Console.ReadLine();
            Console.Write("Рік написання: ");
            song.Year = int.Parse(Console.ReadLine());
            Console.Write("Текст пісні:\n");
            song.Lyrics = Console.ReadLine();
            Console.Write("Виконавці (через кому): ");
            song.Performers = new List<string>(Console.ReadLine().Split(','));

            songs.Add(song);
            Console.WriteLine("Пісню додано.");
        }

        static void DeleteSong()
        {
            Console.Write("Введіть назву пісні для видалення: ");
            string title = Console.ReadLine();

            var song = songs.Find(s => s.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (song != null)
            {
                songs.Remove(song);
                Console.WriteLine("Пісню видалено.");
            }
            else
                Console.WriteLine("Пісню не знайдено.");
        }

        static void EditSong()
        {
            Console.Write("Назва пісні для редагування: ");
            string title = Console.ReadLine();

            var song = songs.Find(s => s.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (song != null)
            {
                Console.Write("Нова назва (залишити пусто для пропуску): ");
                string newTitle = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newTitle)) song.Title = newTitle;

                Console.Write("Новий автор: ");
                string newAuthor = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newAuthor)) song.Author = newAuthor;

                Console.Write("Новий композитор: ");
                string newComposer = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newComposer)) song.Composer = newComposer;

                Console.Write("Новий рік: ");
                string newYear = Console.ReadLine();
                if (int.TryParse(newYear, out int y)) song.Year = y;

                Console.Write("Новий текст: ");
                string newLyrics = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newLyrics)) song.Lyrics = newLyrics;

                Console.Write("Нові виконавці (через кому): ");
                string newPerformers = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newPerformers))
                    song.Performers = new List<string>(newPerformers.Split(','));

                Console.WriteLine("Інформацію оновлено.");
            }
            else
                Console.WriteLine("Пісню не знайдено.");
        }

        static void SearchSong()
        {
            Console.Write("Введіть ключове слово (назва / автор / рік / композитор): ");
            string query = Console.ReadLine().ToLower();

            var results = songs.FindAll(s =>
                s.Title.ToLower().Contains(query) ||
                s.Author.ToLower().Contains(query) ||
                s.Composer.ToLower().Contains(query) ||
                s.Year.ToString().Contains(query));

            if (results.Count > 0)
                foreach (var s in results)
                    Console.WriteLine("\n" + s);
            else
                Console.WriteLine("Пісень не знайдено.");
        }

        static void SongsByPerformer()
        {
            Console.Write("Введіть ім’я виконавця: ");
            string performer = Console.ReadLine().ToLower();

            var results = songs.FindAll(s => s.Performers.Exists(p => p.Trim().ToLower() == performer));

            if (results.Count > 0)
                foreach (var s in results)
                    Console.WriteLine("\n" + s);
            else
                Console.WriteLine("Пісень не знайдено.");
        }

        static void SaveToFile()
        {
            Console.Write("Введіть шлях до файлу для збереження: ");
            string path = Console.ReadLine();
            string json = JsonSerializer.Serialize(songs, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
            Console.WriteLine("Колекцію збережено.");
        }

        static void LoadFromFile()
        {
            Console.Write("Введіть шлях до файлу для завантаження: ");
            string path = Console.ReadLine();
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                songs = JsonSerializer.Deserialize<List<Song>>(json);
                Console.WriteLine("Колекцію завантажено.");
            }
            else
                Console.WriteLine("Файл не знайдено.");
        }
    }
}